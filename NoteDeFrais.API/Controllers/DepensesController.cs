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
using System.Transactions;

namespace NoteDeFrais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepensesController : ControllerBase
    {
        private readonly IFileNServices _fileServices;
        private readonly INoteFraisServices _noteFraisServices;
        private readonly IDepensesServices _depensesServices;
        private readonly IEmployeeGroupeServices _employeeGroupeServices;
        private readonly ITypeDepenseServices _typeDepenseServices;
        private readonly IDeviseServices _deviseServices;
        private readonly IMoyenPaiementServices _moyenPaiementServices;
        private readonly IPaysServices paysServices;

        public DepensesController(INoteFraisServices noteFraisServices, IDepensesServices depensesServices, IEmployeeGroupeServices employeeGroupeServices,
             ITypeDepenseServices typeDepenseServices, IDeviseServices deviseServices, IFileNServices fileServices, IMoyenPaiementServices moyenPaiementServices, IPaysServices pays)
        {
            _noteFraisServices = noteFraisServices;
            _depensesServices = depensesServices;
            _deviseServices = deviseServices;
            _employeeGroupeServices = employeeGroupeServices;
            _typeDepenseServices = typeDepenseServices;
            _fileServices = fileServices;
            paysServices = pays;
            _moyenPaiementServices = moyenPaiementServices;
        }



        [ProducesResponseType(200)]
 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList)]
        [HttpGet, Route("GetAllDepenses")]
        public IActionResult GetAllDepenses()
        {
            return StatusCode(200, _depensesServices.GetAllDepenses());
        }


        [ProducesResponseType(200)]
 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetAllDepensesByNoteFrais")]
        public IActionResult GetAllDepensesByNoteFrais(int id)
        {
            return StatusCode(200, _depensesServices.GetAllDepensesIncludedByNoteFrais(id));
        }


        [ProducesResponseType(200)]
 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetDepensesByID")]
        public IActionResult GetDepensesByID(int id)
        {
            var depense = _depensesServices.GetDepensesByID(id);
            if (depense == null)
                return NotFound();
            else
                return StatusCode(200, depense);
        }



//        [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPost, Route("POST"), DisableRequestSizeLimit]
        public IActionResult POST([FromBody] Depenses depenses, string dateDepense)
        {
            try
            {
                if (depenses == null) throw new Exception("FailDepensesObject");
                var note = _noteFraisServices.GetNotesFraisByDepense(depenses);
                if (note == null) return StatusCode(400, "FailCreateDepenses");
                DateTime dateD = DateTime.ParseExact(dateDepense, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                depenses.DateDepense = dateD;
                if (_depensesServices.checkDateInNotePeriod(depenses, note.DateDebut, note.DateFin))
                {
                    var c = Convert.ToInt32(depenses.NotesFraisID);
                    var notes = _noteFraisServices.GetNotesFraisByID(c);
                    depenses.NotesFrais = notes;
                    var a = Convert.ToInt32(depenses.MoyenPaiementID);
                    var money = _moyenPaiementServices.GetMoyenPaiementByID(a);
                    depenses.MoyenPaiement = money;
                    var b = Convert.ToInt32(depenses.TypeDepenseID);
                    var typedep = _typeDepenseServices.GetTypeDepenseById(b);
                    depenses.TypeDepense = typedep;
                    depenses = _depensesServices.Create(depenses);
                    if (depenses == null)
                    {
                        return StatusCode(400, "FailCreateDepenses");
                    }
                    note.TotalRembourser = note.TotalRembourser + depenses.TotalRemboursable;
                    note.TotalTTC = note.TotalTTC + depenses.TTC;
                    _noteFraisServices.Edit(note);
                }
                else
                {
                    return StatusCode(400, "Expensesmustbeincludedinthesameperiod");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
            return StatusCode(200, depenses);
        }



//        [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Depenses depenses, string dateDepense)
        {
            try
            {
                if (depenses == null) throw new Exception("FailDepensesObject");
                var note = _noteFraisServices.GetNotesFraisByDepense(depenses);
                if (note == null) return StatusCode(400, "FailNoteFraisObject");
                var depensesModified = _depensesServices.GetDepensesByID(id);
                if (depensesModified == null) return StatusCode(400, "FailUpdateDepenses");
                DateTime dateD = DateTime.ParseExact(dateDepense, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                depenses.DateDepense = dateD;
                depensesModified.Client = depenses.Client;
                depensesModified.Commentaire = depenses.Commentaire;
                int? idDevise = depenses.DeviseIDConsumed;
                var d = _deviseServices.GetDeviseByID(idDevise.Value);
                var dec = d.Decimal;
                note.TotalRembourser = (note.TotalRembourser - depensesModified.TotalRemboursable) + depenses.TotalRemboursable;
                note.TotalRembourser = (float)Math.Round(note.TotalRembourser, dec);
                note.TotalTTC = (note.TotalTTC - depensesModified.TTC) + depenses.TTC;
                note.TotalTTC = (float)Math.Round(note.TotalTTC, dec);
                _noteFraisServices.Edit(note);
                depensesModified.DeviseIDConsumed = depenses.DeviseIDConsumed;
                depensesModified.Reference2 = depenses.Reference2;
                depensesModified.ExchangeRate = depenses.ExchangeRate;
                depensesModified.Facturable = depenses.Facturable;
                depensesModified.Libelle = depenses.Libelle;
                depensesModified.MoyenPaiementID = depenses.MoyenPaiementID;
                depensesModified.PaysIDConsumed = depenses.PaysIDConsumed;
                depensesModified.ReferenceCommande = depenses.ReferenceCommande;
                depensesModified.Titre = depenses.Titre;
                depensesModified.TotalRemboursable = depenses.TotalRemboursable;
                depensesModified.TVA = depenses.TVA;
                depensesModified.TTC = depenses.TTC;
                depensesModified.DateDepense = depenses.DateDepense;
                depensesModified.Warning = depenses.Warning;
                depensesModified.TypeDepenseID = depenses.TypeDepenseID;
                depensesModified = _depensesServices.Edit(depensesModified);
                return StatusCode(200, depensesModified);


            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("WarningUpdate")]
        public IActionResult UpdateWarning(int id, bool warning)
        {
            try
            {
                var depensesModified = _depensesServices.GetDepensesByID(id);
                if (depensesModified == null)
                    return NotFound();
                depensesModified.Warning = warning;
                depensesModified = _depensesServices.Edit(depensesModified);
                return StatusCode(200, depensesModified);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


  //      [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var dep = _depensesServices.Delete(id);
                if (dep == null)
                {
                    return StatusCode(400, "FailDeleteDepenses");
                }
                else
                {
                    var documentList = _fileServices.GetDepenseDocumentsListByDepenseId(id);
                    if (documentList.Count() > 0)
                    {
                        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                        {
                            try
                            {
                                foreach (var doc in documentList)
                                {
                                    _fileServices.DeleteDepenseDocument(doc);
                                }
                                scope.Complete();

                            }
                            finally
                            {
                                scope.Dispose();
                            }
                        }
                    }
                    int? idDevise = dep.DeviseIDConsumed;
                    var devise = _deviseServices.GetDeviseByID(idDevise.Value);
                    if (devise == null)
                        return NotFound();
                    var dec = devise.Decimal;
                    var n = _noteFraisServices.GetNotesFraisByDepense(dep);
                    n.TotalRembourser = n.TotalRembourser - dep.TotalRemboursable;
                    n.TotalRembourser = (float)Math.Round(n.TotalRembourser, dec);
                    n.TotalTTC = n.TotalTTC - dep.TTC;
                    n.TotalTTC = (float)Math.Round(n.TotalTTC, dec);
                    _noteFraisServices.Edit(n);
                    return StatusCode(200, dep);
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPost, Route("DeleteDocDepense")]
        public IActionResult DeleteDocDepense([FromBody] List<int> ids)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    foreach (var item in ids)
                    {
                        var doc = _fileServices.GetDepenseDocumentById(item);
                        _fileServices.DeleteDepenseDocument(doc);
                    }
                    scope.Complete();

                }
                finally
                {
                    scope.Dispose();
                }
            }
            return StatusCode(200, "ok");
        }


        [ProducesResponseType(200)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetAllTypeDepenseByEmployee")]
        public IActionResult GetAllTypeDepenseByEmployee(int idEmployee, DateTime dateDepense)
        {
            var listEmployeeGroupe = _employeeGroupeServices.GetGroupeByEmployeeIdIncludeGroupeFraisDepense(idEmployee);
            var GetAllTypeDepenseByEmployee = new List<EmployeeGroupe>();
            var GetAllGroupeFrais = new List<GroupeFrais>();
            var GetAllGroupeFraisDepense = new List<GroupeFraisDepense>();
            var DepenseGroupeVMs = new List<DepenseGroupeVM>();
            var FinalDepenseGroupeVMs = new List<DepenseGroupeVM>();
            var GetAllTypeDepense = new List<TypeDepense>();
            var GetAllTypeDepenseId = new List<int>();

            foreach (var t in listEmployeeGroupe)
            {
                if (dateDepense >= t.DateAffectation && dateDepense <= t.DateFinAffectation)
                {
                    GetAllTypeDepenseByEmployee.Add(t);
                }
            }
            foreach (var tt in GetAllTypeDepenseByEmployee)
            {
                GetAllGroupeFrais.Add(tt.GroupeFrais);
            }
            foreach (var ttt in GetAllGroupeFrais)
            {
                foreach (var i in ttt.GroupeFraisDepense)
                {
                    var t = new TypeDepense();
                    t = _typeDepenseServices.GetTypeDepenseByIDIncluded(i.TypeDepenseID);
                    if (t == null)
                        return NotFound();
                    else
                    {
                        var depense = new DepenseGroupeVM();
                        depense.Intitule = t.Intitule;
                        depense.TypeDepenseID = t.Id;
                        depense.GroupeFraisID = i.GroupeFraisID;
                        depense.Plafond = i.Plafond;
                        depense.Forfait = i.Forfait;
                        DepenseGroupeVMs.Add(depense);
                    }

                }
                if (GetAllGroupeFrais.Count() > 1)
                {
                    foreach (var i in DepenseGroupeVMs)
                    {
                        var SelectedDepenseGroupeVMs = new List<DepenseGroupeVM>();
                    }

                    for (int i = 0; i < DepenseGroupeVMs.Count(); i++)
                    {
                        var dep = DepenseGroupeVMs.Where(x => x.TypeDepenseID == DepenseGroupeVMs[i].TypeDepenseID).OrderByDescending(t => t.Plafond + t.Forfait).FirstOrDefault();
                        FinalDepenseGroupeVMs.Add(dep);
                    }
                    FinalDepenseGroupeVMs = FinalDepenseGroupeVMs.Distinct().ToList();
                }
                else
                {
                    FinalDepenseGroupeVMs = DepenseGroupeVMs;
                }
            }
            return StatusCode(200, FinalDepenseGroupeVMs);
        }
                [ProducesResponseType(200)]
 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetDepensesdetailByID")]
        public IActionResult GetDepensesdetailByID(int id)
        {
            var dep = _depensesServices.GetDepensesByID(id);
            var pay = paysServices.GetPayByID(dep.PaysIDConsumed);
            var c = Convert.ToInt32(dep.DeviseIDConsumed);
            var dev = _deviseServices.GetDeviseByID(c);
            var depense = dep.AsDepense(dev.Decimal, pay.Code, pay.Intitule, pay.DeviseCode);
            return StatusCode(200, depense);
        }


    }
}