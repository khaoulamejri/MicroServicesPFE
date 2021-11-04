using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteDeFrais.API.Common;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Domain.Enum;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDeFrais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdreMissionController : ControllerBase
    {
        private readonly IOrdreMissionServices _ordreMissionServices;
        private readonly INoteFraisServices _noteFraisServices;
        private readonly IEmployeeServices _employeeServices;
        private readonly ITypeOrdreMissionServices _typeOrdreMissionServices;
        private readonly IPaysServices paysServices;


        public OrdreMissionController(IOrdreMissionServices ordreMissionServices, INoteFraisServices noteFraisServices,
            IEmployeeServices employeeServices, ITypeOrdreMissionServices typeOrdreMissionServices, IPaysServices pays)
        {
            _ordreMissionServices = ordreMissionServices;
            _noteFraisServices = noteFraisServices;
            _employeeServices = employeeServices;
            paysServices = pays;
            _typeOrdreMissionServices = typeOrdreMissionServices;
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] OrdreMission ordreMission, string dateDebut, string dateFin, bool others = false)
        {
            DateTime dateD = DateTime.ParseExact(dateDebut, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateF = DateTime.ParseExact(dateFin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            try
            {
                if (ordreMission == null) throw new Exception("FailOrdreMissionObject");
                ordreMission.DateDebut = dateD;
                ordreMission.DateFin = dateF;
                if (_ordreMissionServices.checkTitleUnicity(ordreMission))
                {

                    if (_ordreMissionServices.checkPeriodUnicity(ordreMission))
                    {
                        var LeaveDates = _employeeServices.checkMiisonOrdersAndLeaveDates(ordreMission.EmployeeIDConsumed, ordreMission.DateDebut, ordreMission.DateFin);
                        if (LeaveDates == "")
                        {
                            int numero = 0;
                            numero = _ordreMissionServices.GetAllOrdreMission().Select(p => Convert.ToInt32(p.NumeroOM)).DefaultIfEmpty(0).Max();
                            numero++;
                            ordreMission.NumeroOM = numero.ToString();

                            var c = Convert.ToInt32(ordreMission.TypeMissionOrderId);
                            var typeordre = _typeOrdreMissionServices.GetTypeOrdreMissionById(c);
                            ordreMission.typeOrdreMission = typeordre;
                            ordreMission = _ordreMissionServices.Create(ordreMission);
                            if (ordreMission == null)
                            {
                                return StatusCode(400, "FailCreateordreMission");
                            }
                        }
                        else
                        {
                            return StatusCode(400, new { ExceptionMessage = "conflictwithValidatedLeaveDates", Value = LeaveDates });
                        }
                    }
                    else
                    {
                        return StatusCode(400, "OrderPeriodExist");
                    }
                }
                else
                {
                    return StatusCode(400, "NameOrderExist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200, ordreMission);
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, string dateDebut, string dateFin, [FromBody] OrdreMission ordreMission)
        {
            DateTime dateD = DateTime.ParseExact(dateDebut, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateF = DateTime.ParseExact(dateFin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            try
            {
                var ordreMissionModified = _ordreMissionServices.GetOrdreMissionById(id);
                if (ordreMissionModified == null) throw new Exception("FailOrdreMissionObject");
                ordreMission.DateDebut = dateD;
                ordreMission.DateFin = dateF;
                if (_ordreMissionServices.checkTitleUnicity(ordreMission))
                {
                    if (_ordreMissionServices.checkPeriodUnicity(ordreMission))
                    {
                        var LeaveDates = _employeeServices.checkMiisonOrdersAndLeaveDates(ordreMission.EmployeeIDConsumed, ordreMission.DateDebut, ordreMission.DateFin);
                        if (LeaveDates == "")
                        {
                            if (ordreMission.PaysIdConsumed != 0) { ordreMissionModified.PaysIdConsumed = ordreMission.PaysIdConsumed; }
                         ordreMissionModified.Statut = ordreMission.Statut;
                            ordreMissionModified.Titre = ordreMission.Titre;
                            ordreMissionModified.EmployeeIDConsumed = ordreMission.EmployeeIDConsumed;
                            ordreMissionModified.TypeMissionOrderId = ordreMission.TypeMissionOrderId;
                            ordreMissionModified.DateDebut = ordreMission.DateDebut;
                            ordreMissionModified.DateFin = ordreMission.DateFin;
                            ordreMissionModified.Description = ordreMission.Description;
                            ordreMissionModified = _ordreMissionServices.Edit(ordreMissionModified);
                            return StatusCode(200, ordreMissionModified);
                        }
                        else
                        {
                            return StatusCode(400, new { ExceptionMessage = "conflictwithValidatedLeaveDates", Value = LeaveDates });
                        }
                    }
                    else { return StatusCode(400, "OrderPeriodExist"); }
                }
                else
                {
                    return StatusCode(400, "NameOrderExist");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Delete_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ordreMission = _ordreMissionServices.GetOrdreMissionById(id);
                if (ordreMission != null)
                {
                    var listNoteFrais = _noteFraisServices.GetNotesFraisByOrdreMissionId(id);
                    if (listNoteFrais.Count > 0) { return StatusCode(400, "OrdreMissionFailDelete"); }
                    else ordreMission = _ordreMissionServices.Delete(id);
                }
                else { return StatusCode(404, "OrdreMissionNotFound"); }
                return StatusCode(200, ordreMission);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

  
       

        #region Mission orders to be Validated

        [ProducesResponseType(200)]
      //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpGet, Route("GetMissionOrdersToBeValidated")]
        public IActionResult GetMissionOrdersToBeValidated(int currentCompanyId, string userName)
        {
            var employee = _employeeServices.GetEmployeeByUserName(userName);
            if (employee == null)
            {
                return StatusCode(204, null);
            }
            var MissionOrdersToBeValidated = new List<OrdreMissionVM>();

            var wfDocToBeValidated = _employeeServices.GetDocumentsToBeValidatedByEmployee(employee.Id, TypeDemande.OrdreMission.ToString());
            foreach (var wfDoc in wfDocToBeValidated)
            {
                var ordreMissionEmployee = _ordreMissionServices.GetAllIncludedOrdreMissionById(wfDoc.DocumentId);
                if (ordreMissionEmployee != null && ordreMissionEmployee.Statut == StatusDocument.soumetre)
                {
                    var ordreMissionVM = _ordreMissionServices.ConvertToOrdreMissionVM(ordreMissionEmployee);
                    MissionOrdersToBeValidated.Add(ordreMissionVM);
                }
            }
            return StatusCode(200, MissionOrdersToBeValidated);
        }

        #endregion


        #region Update Status MO
        [ProducesResponseType(200)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpGet, Route("UpdateStatusOM")]
        public IActionResult UpdateStatus(int id, eWfAction status)
        {
            var ordreMission = _ordreMissionServices.GetOrdreMissionById(id);
            if (ordreMission == null) throw new Exception("FailOrdreMissionObject");
            if (status == eWfAction.Attente)
            {
                ordreMission.Statut = StatusDocument.soumetre;
            }
            if (status == eWfAction.Approbation)
            {
                ordreMission.Statut = StatusDocument.valider;
            }
            if (status == eWfAction.Refus)
            {
                ordreMission.Statut = StatusDocument.refuser;
            }
            if (status == eWfAction.Annulation)
            {
                ordreMission.Statut = StatusDocument.annuler;
            }
            ordreMission.DateModif = DateTime.Today;
            ordreMission = _ordreMissionServices.Edit(ordreMission);
            return StatusCode(200, ordreMission);
        }

        #endregion

        #region Abandonner Ordre de mision
        [ProducesResponseType(200)]
    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpGet, Route("AbandonnerOM")]
        public IActionResult AbandonneOM(int id)
        {
            var ordreMission = _ordreMissionServices.GetOrdreMissionById(id);
            if (ordreMission == null)
            {
                return NotFound();
            }
            ordreMission.Statut = StatusDocument.abondonner;
            ordreMission.DateModif = DateTime.Today;
            ordreMission = _ordreMissionServices.Edit(ordreMission);
            return StatusCode(200, ordreMission);
        }

        #endregion


        [ProducesResponseType(200)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpGet, Route("GetMissionOrderById")]
        public IActionResult GetMissionOrderById(int id)
        {
            var oredremission = _ordreMissionServices.GetOrdreMissionById(id);
            var employes = _employeeServices.GetAllEmployees();
            var empl = employes.Single(empl => empl.Id == oredremission.EmployeeIDConsumed);
            var pay = paysServices.GetPayByID(oredremission.PaysIdConsumed);
            var ordre = oredremission.AsOrdreMisiion(pay.Code, pay.Intitule, pay.DeviseCode,empl.UserCreat, empl.UserModif,
                empl.DateCreat, empl.DateModif, empl.companyID,
                 empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne);
          





            return StatusCode(200, ordre);
        }


        [ProducesResponseType(200)]
     //   [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_OrderMissionList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeOM_ExtraOM)]
        [HttpGet, Route("getValidatedMissionOrdersByEmployeeId")]
        public IActionResult getValidatedMissionOrdersByEmployeeId(int employeeId)
        {


            return StatusCode(200, _ordreMissionServices.getValidatedMissionOrdersByEmployeeId(employeeId));
        }
    }
}
