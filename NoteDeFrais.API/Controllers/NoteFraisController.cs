using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteDeFrais.API.Common;
using NoteDeFrais.Domain.Entities;
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
    public class NoteFraisController : ControllerBase
    {
        private readonly IEmployeeGroupeServices employeeGroupeServices;
        private readonly INoteFraisServices _noteFraisServices;
        private readonly IEmployeeServices _employeeServices;

        private readonly IOrdreMissionServices _ordreMissionServices;

   //     private readonly IDemandeCongeServices _demandeCongeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NoteFraisController(INoteFraisServices noteFraisServices, IHttpContextAccessor httpContextAccessor,
               IEmployeeServices employeeServices, IDepensesServices depensesServices, IFraisKilometriquesServices fraisKilometriquesServices,
               IOrdreMissionServices ordreMissionServices, IEmployeeGroupeServices EmGService
              )

        {
            _noteFraisServices = noteFraisServices;
            _employeeServices = employeeServices;
            employeeGroupeServices = EmGService;
            _ordreMissionServices = ordreMissionServices;
            _httpContextAccessor = httpContextAccessor;
      
        }

        // [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] NotesFrais notesFrais, string date1, string date2)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
          
            var note = new NotesFrais();
            DateTime dateD = DateTime.ParseExact(date1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateF = DateTime.ParseExact(date2, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int numero = 0;
            try
            {
                notesFrais.DateDebut = dateD;
                notesFrais.DateFin = dateF;

               // emplId = notesFrais.EmployeeIDConsumed;
                if (notesFrais == null) throw new Exception("FailNotesFraisObject");
                //if (!others)
                //{
                //    var emp = _employeeServices.GetEmployeeByUserNameCompany();
                //    emplId = emp.Id;
                //    notesFrais.EmployeeIDConsumed = emp.Id;
                //}
                //else
                //{
                //    emplId = notesFrais.EmployeeIDConsumed;
                //}
                if (_ordreMissionServices.checkNoteDates(notesFrais))
                {
                    if (_noteFraisServices.checkPeriodUnicity(notesFrais))
                    {
                        //if (employeeGroupeServices.GetGroupeByEmployeeIdDateNote(notesFrais.EmployeeIDConsumed, notesFrais.DateFin, notesFrais.DateFin) != null)
                        //{
                            //var LeaveDates = _demandeCongeServices.checkMiisonOrdersAndLeaveDates(notesFrais.EmployeeIDConsumed, notesFrais.DateDebut, notesFrais.DateFin);
                            //if (LeaveDates == "")
                            //{
                                numero = _noteFraisServices.getNumeroNoteFrais();
                                numero++;
                                notesFrais.NumeroNote = numero.ToString();
                             //   notesFrais.EmployeeIDConsumed = emplId;
                                notesFrais.companyID = currentCompanyId;
                            var c = Convert.ToInt32(notesFrais.OrdreMissionId);
                            var ordre = _ordreMissionServices.GetOrdreMissionById(c);
                            notesFrais.OrdreMission = ordre;
                            note = _noteFraisServices.Create(notesFrais);
                         //   }
                            //else
                            //{
                            //    return StatusCode(400, new { ExceptionMessage = "conflictwithValidatedLeaveDates", Value = LeaveDates });
                            //}
                        //}
                        //else
                        //{
                        //    return StatusCode(400, "NonAffectation");
                        //}
                    }
                    else
                    {
                        return StatusCode(400, "NotePeriodExist");
                    }
                }
                else { return StatusCode(400, "checkExpensePeriod"); }

                if (note == null)
                {
                    return StatusCode(400, "FailCreateNotesFrais");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, note);
        }
        [ProducesResponseType(200)]
      //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetNoteFraisByID")]
        public IActionResult GetNoteFraisByID(int id)
        {
            var notefrais = _noteFraisServices.GetNotesFraisByID(id);
           var employes = _employeeServices.GetAllEmployees();
           var empl = employes.Single(empl => empl.Id == notefrais.EmployeeIDConsumed);

            var note = notefrais.AsNotefrais(empl.UserCreat, empl.UserModif,
                empl.DateCreat, empl.DateModif, empl.companyID,
                 empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.PlanDroitCongeIDConsumed, empl.RegimeTravailID, empl.ConsultantExterne);

            return StatusCode(200, note);
        }




        // [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPost, Route("POSTT")]
        public IActionResult POSTT([FromBody] NotesFrais notesFrais, string date1, string date2)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);

            var note = new NotesFrais();
            DateTime dateD = DateTime.ParseExact(date1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime dateF = DateTime.ParseExact(date2, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            int numero = 0;
            try
            {
                notesFrais.DateDebut = dateD;
                notesFrais.DateFin = dateF;

                // emplId = notesFrais.EmployeeIDConsumed;
                if (notesFrais == null) throw new Exception("FailNotesFraisObject");
                //if (!others)
                //{
                //    var emp = _employeeServices.GetEmployeeByUserNameCompany();
                //    emplId = emp.Id;
                //    notesFrais.EmployeeIDConsumed = emp.Id;
                //}
                //else
                //{
                //    emplId = notesFrais.EmployeeIDConsumed;
                //}
                if (_ordreMissionServices.checkNoteDates(notesFrais))
                {
                    if (_noteFraisServices.checkPeriodUnicity(notesFrais))
                    {
                        if (employeeGroupeServices.GetGroupeByEmployeeIdDateNote(notesFrais.EmployeeIDConsumed, notesFrais.DateFin, notesFrais.DateFin) != null)
                        {
                            //var LeaveDates = _demandeCongeServices.checkMiisonOrdersAndLeaveDates(notesFrais.EmployeeIDConsumed, notesFrais.DateDebut, notesFrais.DateFin);
                            //if (LeaveDates == "")
                            //{
                            numero = _noteFraisServices.getNumeroNoteFrais();
                        numero++;
                        notesFrais.NumeroNote = numero.ToString();
                        //   notesFrais.EmployeeIDConsumed = emplId;
                        notesFrais.companyID = currentCompanyId;
                        var c = Convert.ToInt32(notesFrais.OrdreMissionId);
                        var ordre = _ordreMissionServices.GetOrdreMissionById(c);
                        notesFrais.OrdreMission = ordre;
                        note = _noteFraisServices.Create(notesFrais);
                          }
                        //else
                        //{
                        //    return StatusCode(400, new { ExceptionMessage = "conflictwithValidatedLeaveDates", Value = LeaveDates });
                        //}
                        //}
                            else
                            {
                                return StatusCode(400, "NonAffectation");
                            }
                        }
                    else
                    {
                        return StatusCode(400, "NotePeriodExist");
                    }
                }
                else { return StatusCode(400, "checkExpensePeriod"); }

                if (note == null)
                {
                    return StatusCode(400, "FailCreateNotesFrais");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, note);
        }
      
        //   [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] NotesFrais notesFrais)
        {
            try
            {
                var notesFraisModified = _noteFraisServices.GetNotesFraisById(id);
                if (notesFraisModified == null) throw new Exception("FailNotesFraisObject");
                notesFraisModified.Code = notesFrais.Code;
                notesFraisModified = _noteFraisServices.Edit(notesFraisModified);
                return StatusCode(200, notesFrais);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
        [ProducesResponseType(200)]
    //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetAllNoteFrais")]
        public IActionResult GetAllNoteFrais()
        {
            return StatusCode(200, _noteFraisServices.GetAllNotesFrais());
        }
        [ProducesResponseType(200)]
     // [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetNoteFraisListByMissionOrderId")]
        public IActionResult GetNoteFraisListByMissionOrderId(int missionOrderId)
        {
            return StatusCode(200, _noteFraisServices.GetNotesFraisByOrdreMissionId(missionOrderId));
        }
    }
}
