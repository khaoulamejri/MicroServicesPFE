using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteDeFrais.API.Common;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDeFrais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeGroupeController : ControllerBase
    {
        private readonly IEmployeeGroupeServices _employeeGroupeServices;
        private readonly INoteFraisServices _noteFraisServices;
        private readonly IGroupeFraisServices _groupeFraisServices;
        private readonly IEmployeeServices _employeeServices;


        public EmployeeGroupeController(IEmployeeGroupeServices employeeGroupeServices, INoteFraisServices noteFraisServices, IGroupeFraisServices groupeFraisServices, IEmployeeServices employeeServices)
        {
            _employeeGroupeServices = employeeGroupeServices;
            _noteFraisServices = noteFraisServices;
            _groupeFraisServices = groupeFraisServices;
            _employeeServices = employeeServices;
        }

        [ProducesResponseType(200)]
    //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Read_NoteEmployee)]
        [HttpGet, Route("GetAllEmployeeGroupe")]
        public IActionResult GetAllEmployeeGroupe()
        {
            return StatusCode(200, _employeeGroupeServices.GetAllEmployeeGroupe());
        }

        [ProducesResponseType(200)]
    //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Read_NoteEmployee)]
        [HttpGet, Route("GetEmployeeGroupeByID")]
        public IActionResult GetEmployeeGroupeByID(int id)
        {
            //var employeeGroupe = _employeeGroupeServices.GetEmployeeGroupeByIdIncluded(id);
            //if (employeeGroupe == null)
            //    return NotFound();
            //else
            //    return StatusCode(200, employeeGroupe);
            var employeeGroupe = _employeeGroupeServices.GetEmployeeGroupeById(id);
            var employes = _employeeServices.GetAllEmployees();
            var empl = employes.Single(empl => empl.Id == employeeGroupe.EmployeeIDConsumed);
            var empgroup = employeeGroupe.AsEmpGroup(empl.UserCreat, empl.UserModif,
                empl.DateCreat, empl.DateModif, empl.companyID,
                 empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne);
            return StatusCode(200, empgroup);
        }

        [ProducesResponseType(200)]
  //    [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_NoteEmployee)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] EmployeeGroupe employeeGroupe)
        {
            try
            {
                if (employeeGroupe == null) throw new Exception("FailEmployeeGroupeObject");
                employeeGroupe.DateAffectation = employeeGroupe.DateAffectation.AddHours(1);
                employeeGroupe.DateFinAffectation = employeeGroupe.DateFinAffectation.AddHours(1);
                if (!_employeeGroupeServices.checkEmployeeGroup(employeeGroupe))
                {
                    return StatusCode(400, "EmployeeGroupPeriodWarning");
                }
                else
                {

                    var c = Convert.ToInt32(employeeGroupe.GroupeFraisID);
                    var group = _groupeFraisServices.GetGroupeFraisByID(c);
                    employeeGroupe.GroupeFrais = group;
                    employeeGroupe = _employeeGroupeServices.Create(employeeGroupe);

                    if (employeeGroupe == null)
                    {
                        return StatusCode(400, "FailCreateEmployeeGroupe");
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, employeeGroupe);
        }

   //   [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_NoteEmployee)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] EmployeeGroupe employeeGroupe)
        {
            try
            {
                if (employeeGroupe == null) throw new Exception("FailEmployeeGroupeObject");
                var employeeGroupeModified = _employeeGroupeServices.GetEmployeeGroupeById(id);
                if (employeeGroupeModified == null) throw new Exception("FailEmployeeGroupeObject");
                employeeGroupeModified.DateAffectation = employeeGroupe.DateAffectation.AddHours(1);
                employeeGroupeModified.DateFinAffectation = employeeGroupe.DateFinAffectation.AddHours(1);
                employeeGroupeModified.EmployeeIDConsumed = employeeGroupe.EmployeeIDConsumed;
                employeeGroupeModified.GroupeFraisID = employeeGroupe.GroupeFraisID;
                if (!_employeeGroupeServices.checkEmployeeGroup(employeeGroupeModified))
                {
                    return StatusCode(400, "EmployeeGroupPeriodWarning");
                }
                else
                {
                    employeeGroupeModified = _employeeGroupeServices.Edit(employeeGroupeModified);
                    return StatusCode(200, employeeGroupeModified);
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

   //   [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_NoteEmployee)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employeeGroup = _employeeGroupeServices.Delete(id);
                if (employeeGroup == null)
                {
                    return StatusCode(400, "FailDeleteEmployeeGroupe");
                }
                return StatusCode(200, employeeGroup);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


        [ProducesResponseType(200)]
      //[ClaimRequirement("Privilege", ApiPrivileges.Employees_Read_NoteEmployee)]
        [HttpGet, Route("GetAllGroupeByEmployeeId")]
        public IActionResult GetAllGroupeByEmployeeId(int id)
        {

            return StatusCode(200, _employeeGroupeServices.GetAllEmployeeGroupeByEmployeeId(id));
        }

        [ProducesResponseType(200)]
     // [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetGroupeByEmployeeIdDateNote")]
        public IActionResult GetGroupeByEmployeeIdDateNote(int id, int idNote)
        {
            var note = _noteFraisServices.GetNotesFraisByID(idNote);
            if (note == null) throw new Exception("FailNoteFraisObject");
            return StatusCode(200, _employeeGroupeServices.GetGroupeByEmployeeIdDateNote(id, note.DateFin, note.DateFin));
        }

    }
}