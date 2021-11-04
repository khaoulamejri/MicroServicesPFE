using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
  //  [ApiController]
    public class AffectatioEmployeeController : ControllerBase
    {
        private readonly IAffectationEmployeeServices affectationEmployeeServices;
        private readonly IEmployeeServices employeeServices;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IPositionServices positionServices;
        public AffectatioEmployeeController(IAffectationEmployeeServices aff, IEmployeeServices emp, IPublishEndpoint publishEndpoint, IPositionServices pos)
        {
            affectationEmployeeServices = aff;
            employeeServices = emp;
            _publishEndpoint = publishEndpoint;
            positionServices = pos;
        }
        [ProducesResponseType(200)]
      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAffectationEmployees")]
        public IActionResult GetAffectationEmployees(int employeeId)
        {
            var listAffectationEmployee = affectationEmployeeServices.GetAffectationEmployeeByEmployeeId(employeeId);
            return StatusCode(200, listAffectationEmployee);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
      //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpPost, Route("Post")]
        public async Task<IActionResult> PostAsync(DateTime DateRecrutement, [FromBody] AffectationEmployee affectationEmployee)
        {
            try
            {
                var listaff = affectationEmployeeServices.GetAffectationEmployeeByEmployeeIdcmp(affectationEmployee.EmployeeID);
                if (affectationEmployeeServices.CheckUnicityPrincipalAffectationEmployee(affectationEmployee.Id, affectationEmployee.EmployeeID, affectationEmployee.companyID) && affectationEmployee.Principal)
                {
                    return StatusCode(400, "theemployeehasaprincipalposition");
                }

                else
                {
                    affectationEmployee.DateDebut = affectationEmployee.DateDebut.AddHours(1);
                    affectationEmployee.DateFin = affectationEmployee.DateFin.AddHours(1);
                    affectationEmployee.companyID = affectationEmployee.companyID;
                    var emp = employeeServices.GetEmployeeByID(affectationEmployee.EmployeeID);
                    emp.RecruitementDate = DateRecrutement;

                    if (listaff.Exists(a => a.DateDebut < emp.RecruitementDate))
                    {
                        return StatusCode(400, "RecrutementControl");
                    }
                    //if ((affectationEmployee.DateDebut.Date >= affectationEmployee.DateFin.Date) || (affectationEmployee.DateDebut.Date < emp.RecruitementDate.Value.Date))
                    //{
                    //    return StatusCode(400, "RecrutementControl");
                    //}

                    emp = employeeServices.Edit(emp);
                    await _publishEndpoint.Publish(new EmployeeUserUpdated(emp.Id, emp.UserCreat, emp.UserModif, emp.DateCreat, emp.DateModif, emp.companyID, emp.NumeroPersonne, emp.Nom, emp.Prenom, emp.DateNaissance, emp.CIN, emp.DeliveryDateCin, emp.PlaceCin, emp.PassportNumber, emp.ValidityDateRP, emp.RecruitementDate, emp.TitularizationDate, emp.Tel, emp.TelGSM, emp.Mail, emp.Langue, emp.Adresse, emp.Ville, emp.CodePostal, emp.User, emp.PlanDroitCongeIDConsumed, emp.RegimeTravailID, emp.ConsultantExterne));

                    var empl= employeeServices.GetEmployeeByID(affectationEmployee.EmployeeID);
                    affectationEmployee.Employee = empl;

                    var po = positionServices.GetPositionByID(affectationEmployee.PositionID);
                    affectationEmployee.Position = po;
                    var AffectationEmployee = affectationEmployeeServices.Create(affectationEmployee);
                    await _publishEndpoint.Publish(new AffectationEmployeeCreated(affectationEmployee.Id, affectationEmployee.UserCreat, affectationEmployee.UserModif, affectationEmployee.DateCreat, affectationEmployee.DateModif, affectationEmployee.companyID, affectationEmployee.EmployeeID, affectationEmployee.DateDebut, affectationEmployee.DateFin, affectationEmployee.PositionID, affectationEmployee.Principal));

            
                    if (AffectationEmployee != null)
                    {
                        return StatusCode(200, AffectationEmployee);
                    }
                    return StatusCode(400, "PositionExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
      //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpPut, Route("Put")]
        public async Task<IActionResult> PutAsync(int id, DateTime DateRecrutement, [FromBody] AffectationEmployee affectationEmployee)
        {
            try
            {
                var listaff = affectationEmployeeServices.GetAffectationEmployeeByEmployeeIdcmp(affectationEmployee.EmployeeID).Where(a => a.Id != id).ToList();
                if (affectationEmployeeServices.CheckUnicityPrincipalAffectationEmployee(affectationEmployee.Id, affectationEmployee.EmployeeID, affectationEmployee.companyID) && affectationEmployee.Principal)
                {
                    return StatusCode(400, "theemployeehasaprincipalposition");
                }
                else
                {
                    affectationEmployee.DateDebut = affectationEmployee.DateDebut.AddHours(1);
                    affectationEmployee.DateFin = affectationEmployee.DateFin.AddHours(1);
                    var affEmp = affectationEmployeeServices.GetAffectationEmployeeByID(id);
                    affEmp.EmployeeID = affectationEmployee.EmployeeID;
                    affEmp.DateDebut = affectationEmployee.DateDebut;
                    affEmp.DateFin = affectationEmployee.DateFin;
                    affEmp.PositionID = affectationEmployee.PositionID;
                    affEmp.Principal = affectationEmployee.Principal;
                    var employee = employeeServices.GetEmployeeByID(affectationEmployee.EmployeeID);
                    employee.RecruitementDate = DateRecrutement;
                    if ((affEmp.DateDebut.Date >= affEmp.DateFin.Date) || (affEmp.DateDebut.Date < employee.RecruitementDate.Value.Date))
                    {
                        return StatusCode(400, "RecrutementControl");
                    }
                    if (listaff.Exists(a => a.DateDebut < employee.RecruitementDate.Value.Date))
                    {
                        return StatusCode(400, "RecrutementControl");
                    }
                    if (affectationEmployeeServices.CheckAffectationExistingTimeInterval(affEmp.PositionID, affEmp.DateDebut, affEmp.DateFin, affEmp.EmployeeID))
                    {
                        var mess = affectationEmployeeServices.Edit(affEmp);
                        employee = employeeServices.Edit(employee);
                        await _publishEndpoint.Publish(new AffectationEmployeeUpdated(affEmp.Id, affEmp.UserCreat, affEmp.UserModif, affEmp.DateCreat, affEmp.DateModif, affEmp.companyID, affEmp.EmployeeID, affEmp.DateDebut, affEmp.DateFin, affEmp.PositionID, affEmp.Principal));
                        return StatusCode(200, affectationEmployee);
                    }
                    return StatusCode(200, affEmp);
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
     //   [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpDelete, Route("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                string message = affectationEmployeeServices.Delete(id);
                await _publishEndpoint.Publish(new AffectationEmployeeDeleted(id));
                if (message == null)
                {
                    return StatusCode(400, "FailEmployeeassignment");
                }
                return StatusCode(200, message);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
      //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_InformationEmployee)]
        [HttpGet, Route("GetVacantposition")]
        public IActionResult GetVacantposition(DateTime startDate, DateTime endDate)
        {
            try
            {
                var posList = affectationEmployeeServices.SelectListItemPositions(startDate, endDate);
                return StatusCode(200, posList);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
    
