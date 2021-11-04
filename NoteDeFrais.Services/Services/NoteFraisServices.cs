using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NoteDeFrais.Data;
using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Domain.Enum;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.Services
{
    public class NoteFraisServices : INoteFraisServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public NoteFraisServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
        }

        public List<NotesFrais> GetNotesFraisByEmployeeID(int employeeId)
        {
            return utOfWork.NoteFraisRepository.GetMany(d => d.EmployeeIDConsumed == employeeId).ToList();
        }

        public NotesFrais GetNotesFraisByID(int id)
        {
            return utOfWork.NoteFraisRepository.GetMany(d => d.Id == id).Include(n => n.Depenses).Include(n => n.FraisKilometriques).Include(n => n.DocumentsNoteFrais).First();
        }

        public NotesFrais GetNotesFraisById(int id)
        {
            return utOfWork.NoteFraisRepository.Get(a => a.Id == id);
        }

        public List<NotesFrais> GetAllNotesFrais()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.NoteFraisRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }


        public NotesFrais Create(NotesFrais notesFrais)
        {
            try
            {
                utOfWork.NoteFraisRepository.Add(notesFrais);
                utOfWork.Commit();
                return notesFrais;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public NotesFrais Edit(NotesFrais notesFrais)
        {
            try
            {
                utOfWork.NoteFraisRepository.Update(notesFrais);
                utOfWork.Commit();
                return notesFrais;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        public NotesFrais Delete(int notesFraisId)
        {
            try
            {
                var notesFrais = GetNotesFraisById(notesFraisId);
                if (notesFrais != null)
                {
                    utOfWork.NoteFraisRepository.Delete(notesFrais);
                    utOfWork.Commit();
                    return notesFrais;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<NotesFrais> GetNotesFraisOthers(string username, int employeeId, int companyId)
        {
            try
            {
                return ((employeeId > 0) ?
                   utOfWork.NoteFraisRepository.GetMany(d => d.UserCreat == username && d.EmployeeIDConsumed != employeeId && d.companyID == companyId)
                    : utOfWork.NoteFraisRepository.GetMany(d => d.UserCreat == username && d.companyID == companyId))
                    .ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<NotesFrais> GetNotesFraisByOrdreMissionId(int ordreMissionId)
        {
            return utOfWork.NoteFraisRepository.GetMany(d => d.OrdreMissionId == ordreMissionId).ToList();
        }


        public bool checkPeriodUnicity(NotesFrais notesFrais)
        {
            var EmployeeNotes = GetNotesFraisByEmployeeID(notesFrais.EmployeeIDConsumed);
            EmployeeNotes = EmployeeNotes.Where(x => x.Statut != StatusDocument.abondonner && x.Statut != StatusDocument.refuser && x.Statut != StatusDocument.annuler).ToList();
            if (EmployeeNotes.Any())
            {
                foreach (var nfrais in EmployeeNotes)
                {
                    if (nfrais.DateFin.Date >= notesFrais.DateDebut.Date && nfrais.DateDebut <= notesFrais.DateFin.Date)
                        return false;
                }

            }

            return true;
        }

        public string checkNotesFraisAndLeaveDates(int employeeId, DateTime dateDebutConge, DateTime dateRepriseConge)
        {
            var validatedNotesFrais = getValidatedNotesFraisByEmployeeId(employeeId);
            if (validatedNotesFrais.Any())
            {
                foreach (var note in validatedNotesFrais)
                {
                    if ((dateDebutConge >= note.DateDebut.Date && dateDebutConge <= note.DateFin.Date) ||
                        (dateRepriseConge > note.DateDebut.Date && dateRepriseConge <= note.DateFin.Date) ||
                        (dateDebutConge <= note.DateDebut.Date && dateRepriseConge >= note.DateFin.Date))
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                        return note.DateDebut.GetDateTimeFormats(culture)[0] + " " + note.DateFin.GetDateTimeFormats(culture)[0];
                    }
                }
            }
            return "";
        }

        public List<NotesFrais> getValidatedNotesFraisByEmployeeId(int employeeId)
        {
            return utOfWork.NoteFraisRepository.GetMany(nf => nf.Statut == StatusDocument.valider && nf.EmployeeIDConsumed == employeeId && nf.OrdreMissionId == null)
                                                      .Select(nf => new NotesFrais
                                                      {
                                                          Id = nf.Id,
                                                          DateDebut = nf.DateDebut,
                                                          DateFin = nf.DateFin
                                                      }).ToList();
        }

        public NotesFrais GetNotesFraisByDepense(Depenses depense)
        {
            return utOfWork.NoteFraisRepository.GetMany(nf => nf.Id == depense.NotesFraisID).First();
        }

        public NotesFrais GetNotesFraisByFraisKilometriques(FraisKilometriques fraisKilometriques)
        {
            return utOfWork.NoteFraisRepository.GetMany(nf => nf.Id == fraisKilometriques.NotesFraisId).First();
        }

        public int getNumeroNoteFrais()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.NoteFraisRepository.GetMany(t => t.companyID == currentCompanyId).AsEnumerable().Select(p => Convert.ToInt32(p.NumeroNote)).DefaultIfEmpty(0).Max();
        }

        public NotesFrais getNoteFraisByNumeroNote(string numero)
        {
            return utOfWork.NoteFraisRepository.GetMany(d => d.NumeroNote == numero).Include(x => x.DocumentsNoteFrais).FirstOrDefault();
        }
    }
}
