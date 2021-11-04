using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Conge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsDroitCongeController : ControllerBase
    {
        private readonly IDetailsDroitCongeServices _detailsDroitCongeServices;
        private readonly IDroitCongeServices _droitCongeServices;
        private readonly IEmployeeServices _employeeServices;
      
        private readonly IAncienteServices _ancienteServices;
        private readonly ITypeCongeServices _typeCongeServices;
        private readonly IMvtCongeServices _mvtCongeServices;

        public DetailsDroitCongeController(IDetailsDroitCongeServices detailsDroitCongeServices, IDroitCongeServices droitCongeServices,
            IEmployeeServices employeeServices, IAncienteServices ancienteServices,
            ITypeCongeServices typeCongeServices, IMvtCongeServices mvtCongeServices)
        {
            _detailsDroitCongeServices = detailsDroitCongeServices;
            _droitCongeServices = droitCongeServices;
            _employeeServices = employeeServices;
          
            _ancienteServices = ancienteServices;
            _typeCongeServices = typeCongeServices;
            _mvtCongeServices = mvtCongeServices;
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Read_LeaveRightList)]
        [HttpGet, Route("GetDetailsDroitCongeByDroitConge")]
        public IActionResult GetDetailsDroitCongeByDroitConge(int DroitCongeId)
        {
            var listDetailsDroit = _detailsDroitCongeServices.GetDetailsDroitVMCongeByDroitCongeId(DroitCongeId);
            return StatusCode(200, listDetailsDroit);
        }

  //      [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightList)]
        [HttpPut, Route("Edit")]
        public IActionResult Edit(int id, [FromBody] Details_DroitConge detailsdroitConge)
        {
            try
            {
                var detDroitCong = _detailsDroitCongeServices.GetDetailsDroitCongeByID(id);
                if (detDroitCong == null)
                    return NotFound();
                detDroitCong.Droit = detailsdroitConge.Droit;
                detDroitCong.DroitMisAJour = detailsdroitConge.DroitMisAJour;
                detDroitCong.IdEmployee = detailsdroitConge.IdEmployee;
                detDroitCong.Commentaire = detailsdroitConge.Commentaire;
                detDroitCong = _detailsDroitCongeServices.Edit(detDroitCong);
                return StatusCode(200, detDroitCong);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var detailsdroitConge = _detailsDroitCongeServices.Delete(id);
                if (detailsdroitConge == null)
                    return StatusCode(400, "FailDeleteDroitDetail");
                return StatusCode(200, detailsdroitConge);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightList)]
        [HttpGet, Route("GenerateDetailsDroit")]
        public IActionResult GenerateDetailsDroit(int droitId)
        {
            try
            {
                int countGenerate = 0;
                bool resDelete = _detailsDroitCongeServices.deleteAllDetailsDroitConge(droitId);
                if (resDelete)
                {
                    var droitConge = _droitCongeServices.GetDroitCongeByID(droitId);
                    if (droitConge == null)
                        return NotFound();
                    var detailsDroitSameMonth = _droitCongeServices.GetDroitCongeByDate(droitConge.MoisAffectation)
                                                                                            .Where(d => d.Id != droitConge.Id)
                                                                                          .SelectMany(d => d.Details_DroitConge).ToList();
                    var emplList = _employeeServices.GetAllEmployee();
                    var transactionOptions = new TransactionOptions();
                    transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                    {
                        try
                        {
                            foreach (var item in emplList)
                            {
                                var listDroitsEmployee = detailsDroitSameMonth.Where(d => d.IdEmployee == item.Id);
                                if (listDroitsEmployee.Count() <= 0)
                                {
                                    bool isActif = false;
                                    var ActifAffect = _employeeServices.GetAffectationActif(item.Id);
                                    if (ActifAffect != null) isActif = true;

                                    if (isActif && item.RegimeTravailID != null && item.ConsultantExterne == false)
                                    {
                                        GenerateEmployeeDetailsDroit(item, droitConge);
                                        countGenerate++;
                                    }
                                    else
                                    {
                                        countGenerate++;
                                    }

                                }
                            }
                            scope.Complete();


                        }
                        finally
                        {
                            scope.Dispose();
                        }
                    }
                }
                return StatusCode(200, countGenerate);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

  //      [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_ValidateRequest_LeaveRightList)]
        [HttpGet, Route("ValidateDetailsDroit")]
        public IActionResult ValidateDetailsDroit(int droitId)
        {
            DroitConge obj = new DroitConge();
            try
            {
                var droitConge = _droitCongeServices.GetDroitCongeByID(droitId);
                if (droitConge == null)
                    return NotFound();
                if (droitConge.Status == RhStatus.Valider)
                    throw new Exception("le droit de congé est déja validé");
                var CongeAnnuel = _typeCongeServices.GetTypeCongetAnnuel();
                var listDetailsDroit = _detailsDroitCongeServices.GetDetailsDroitCongeByDroitCongeId(droitId);
                if (droitConge != null && CongeAnnuel != null)
                {
                    int i = 0;
                    using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                    {
                        try
                        {
                            foreach (var Detail in listDetailsDroit)
                            {
                                var employee = _employeeServices.GetEmployeeByID(Detail.IdEmployee);
                                if (employee.ConsultantExterne == false)
                                {
                                    var mvtConge = _mvtCongeServices.ValidateMvtSoldeConge(Detail.IdEmployee, CongeAnnuel.Id, droitConge.MoisAffectation, Detail.companyID, Detail.DroitMisAJour, RhSens.Droit);
                                    if (mvtConge != null) i++;
                                }
                            }
                            scope.Complete();
                        }

                        finally
                        {
                            scope.Dispose();
                        }
                    }
                    if (i == listDetailsDroit.Count)
                    {
                        droitConge.Status = RhStatus.Valider;
                        obj = _droitCongeServices.Edit(droitConge);
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, obj);
        }

        private Details_DroitConge GenerateEmployeeDetailsDroit(Employee employee, DroitConge listDroitConge)
        {
            long diffdate = ((listDroitConge.MoisAffectation.Month - employee.RecruitementDate.Value.Month) + 12 * (listDroitConge.MoisAffectation.Year - employee.RecruitementDate.Value.Year));
            float droit = 0;
            if (employee.PlanDroitCongeIDConsumed != null)
            {
                int planId = (int)employee.PlanDroitCongeIDConsumed;
                List<Anciente> listAnciente = _ancienteServices.GetAncienteByPlanDroitCongeId(planId).OrderBy(d => d.ToAnc).ToList();
                long FromAnc = 0;
                foreach (var anc in listAnciente)
                {
                    if ((FromAnc <= diffdate) && (diffdate <= anc.ToAnc))
                    {
                        droit = anc.JourIncrimente;
                        break;
                    }
                    FromAnc = anc.ToAnc;
                }
            }
            Details_DroitConge detDroitCong = new Details_DroitConge();
            detDroitCong.DroitCongeId = listDroitConge.Id;
            detDroitCong.IdEmployee = employee.Id;
            detDroitCong.Droit = droit;
            detDroitCong.DroitMisAJour = droit;
            detDroitCong.companyID = listDroitConge.companyID;
            detDroitCong.Commentaire = "";
            return _detailsDroitCongeServices.Create(detDroitCong);
        }
    }
}