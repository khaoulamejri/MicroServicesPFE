using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class FraisKilometriquesController : ControllerBase
    {
        private readonly INoteFraisServices _noteFraisServices;
        private readonly IFraisKilometriquesServices _fraisKilometriquesServices;

        public FraisKilometriquesController(INoteFraisServices noteFraisServices, IFraisKilometriquesServices fraisKilometriquesServices)
        {
            _noteFraisServices = noteFraisServices;
            _fraisKilometriquesServices = fraisKilometriquesServices;
        }

        [ProducesResponseType(200)]
 //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList)]
        [HttpGet, Route("GetAllFraisKilometriques")]
        public IActionResult GetAllFraisKilometriques()
        {
            return StatusCode(200, _fraisKilometriquesServices.GetAllFraisKilometriques());
        }

        [ProducesResponseType(200)]
 //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetAllFraisKilometriquesByNoteFrais")]
        public IActionResult GetAllFraisKilometriquesByNoteFrais(int id)
        {
            return StatusCode(200, _fraisKilometriquesServices.GetAllFraisKilometriquesByNoteFrais(id));
        }

        [ProducesResponseType(200)]
  //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Read_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpGet, Route("GetFraisKilometriquesByID")]
        public IActionResult GetFraisKilometriquesByID(int id)
        {
            var FraisKilometriques = _fraisKilometriquesServices.GetFraisKilometriquesByID(id);
            if (FraisKilometriques == null)
                return NotFound();
            else
                return StatusCode(200, FraisKilometriques);
        }

  //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] FraisKilometriques fraisKilometriques, string dateDebut, string dateFin)
        {
            try
            {
                if (fraisKilometriques == null) throw new Exception("FailFraisKilometriquesObject");
                var note = _noteFraisServices.GetNotesFraisByFraisKilometriques(fraisKilometriques);
                if (note == null) throw new Exception("FailNoteFraisObject");
                DateTime dateD = DateTime.ParseExact(dateDebut, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime dateF = DateTime.ParseExact(dateFin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                fraisKilometriques.DateDebut = dateD;
                fraisKilometriques.DateFin = dateF;
                var c = Convert.ToInt32(fraisKilometriques.NotesFraisId);
                var notes = _noteFraisServices.GetNotesFraisByID(c);
                fraisKilometriques.NotesFrais = notes;
                fraisKilometriques = _fraisKilometriquesServices.Create(fraisKilometriques);
                if (fraisKilometriques == null)
                {
                    return StatusCode(400, "FailCreateFraisKilometriques");
                }
                note.TotalTTC = note.TotalTTC + fraisKilometriques.TotalTTC;
                note.TotalRembourser = note.TotalRembourser + fraisKilometriques.TotalRemboursable;
                note.TotalKm = note.TotalKm + (fraisKilometriques.DistanceParcourue * fraisKilometriques.NombreTrajets);
                _noteFraisServices.Edit(note);

            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, fraisKilometriques);
        }

  //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, string dateDebut, string dateFin, [FromBody] FraisKilometriques fraisKilometriques)
        {
            try
            {
                if (fraisKilometriques == null) throw new Exception("FailFraisKilometriquesObject");
                var note = _noteFraisServices.GetNotesFraisByFraisKilometriques(fraisKilometriques);
                if (note == null) throw new Exception("FailNoteFraisObject");
                DateTime dateD = DateTime.ParseExact(dateDebut, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime dateF = DateTime.ParseExact(dateFin, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                fraisKilometriques.DateDebut = dateD;
                fraisKilometriques.DateFin = dateF;
                if (fraisKilometriques.DateDebut.Date >= note.DateDebut.Date && fraisKilometriques.DateFin.Date <= note.DateFin.Date)
                {
                    var fraisKilometriquesModified = _fraisKilometriquesServices.GetFraisKilometriquesByID(id);
                    if (fraisKilometriquesModified == null) throw new Exception("FailFraisKilometriquesObject");
                    if (fraisKilometriques.DateDebut != fraisKilometriquesModified.DateDebut)
                    {
                        fraisKilometriquesModified.DateDebut = fraisKilometriques.DateDebut;
                    }
                    if (fraisKilometriques.DateFin != fraisKilometriquesModified.DateFin)
                    {
                        fraisKilometriquesModified.DateFin = fraisKilometriques.DateFin;
                    }
                    note.TotalTTC = (note.TotalTTC - fraisKilometriquesModified.TotalTTC) + fraisKilometriques.TotalTTC;
                    note.TotalRembourser = (note.TotalRembourser - fraisKilometriquesModified.TotalRemboursable) + fraisKilometriques.TotalRemboursable;
                    note.TotalKm = note.TotalKm + (fraisKilometriques.DistanceParcourue * fraisKilometriques.NombreTrajets) - (fraisKilometriquesModified.DistanceParcourue * fraisKilometriquesModified.NombreTrajets);
                    _noteFraisServices.Edit(note);
                    fraisKilometriquesModified.Depart = fraisKilometriques.Depart;
                    fraisKilometriquesModified.Arrivee = fraisKilometriques.Arrivee;
                    fraisKilometriquesModified.DepartMaps = fraisKilometriques.DepartMaps;
                    fraisKilometriquesModified.ArriveeMaps = fraisKilometriques.ArriveeMaps;
                    fraisKilometriquesModified.Commentaire = fraisKilometriques.Commentaire;
                    fraisKilometriquesModified.Titre = fraisKilometriques.Titre;
                    fraisKilometriquesModified.DistanceParcourue = fraisKilometriques.DistanceParcourue;
                    fraisKilometriquesModified.DistanceParcouruetotal = fraisKilometriques.DistanceParcouruetotal;
                    fraisKilometriquesModified.NombreTrajets = fraisKilometriques.NombreTrajets;
                    fraisKilometriquesModified.TotalTTC = fraisKilometriques.TotalTTC;
                    fraisKilometriquesModified.TotalRemboursable = fraisKilometriques.TotalRemboursable;
                    fraisKilometriquesModified.TypeVehicule = fraisKilometriques.TypeVehicule;
                    fraisKilometriquesModified = _fraisKilometriquesServices.Edit(fraisKilometriquesModified);
                    return StatusCode(200, fraisKilometriquesModified);
                }
                else
                {
                    return StatusCode(400, "Kilometerfeechargemustbeincludedinthesameperiod");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

   //   [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Trait_Edit_ExpenseReportList + ";" + ApiPrivileges.ExpenseReportModule_Trait_EquipeExpense_ExtraExpense)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var fraisKilometrique = _fraisKilometriquesServices.GetFraisKilometriquesByID(id);
                if (fraisKilometrique == null) throw new Exception("FailFraisKilometriquesObject");
                fraisKilometrique = _fraisKilometriquesServices.Delete(id);
                if (fraisKilometrique == null)
                {
                    return StatusCode(400, "FailDeleteFraisKilometriques");
                }
                var note = _noteFraisServices.GetNotesFraisByFraisKilometriques(fraisKilometrique);
                note.TotalTTC = note.TotalTTC - fraisKilometrique.TotalTTC;
                note.TotalRembourser = note.TotalRembourser - fraisKilometrique.TotalRemboursable;
                note.TotalKm = note.TotalKm - (fraisKilometrique.DistanceParcourue * fraisKilometrique.NombreTrajets);
                _noteFraisServices.Edit(note);
                return StatusCode(200, fraisKilometrique);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}