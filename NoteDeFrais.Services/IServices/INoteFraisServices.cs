using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface INoteFraisServices
    {
        NotesFrais GetNotesFraisByID(int id);
        NotesFrais GetNotesFraisById(int id);
        NotesFrais GetNotesFraisByDepense(Depenses depense);
        NotesFrais GetNotesFraisByFraisKilometriques(FraisKilometriques fraisKilometriques);
        List<NotesFrais> GetAllNotesFrais();
        NotesFrais Create(NotesFrais notesFrais);
        NotesFrais Edit(NotesFrais notesFrais);
        NotesFrais Delete(int notesFraisId);
        List<NotesFrais> GetNotesFraisByEmployeeID(int employeeId);
        List<NotesFrais> GetNotesFraisOthers(string username, int employeeId, int companyId);
        List<NotesFrais> GetNotesFraisByOrdreMissionId(int ordreMissionId);
        bool checkPeriodUnicity(NotesFrais notesFrais);
        string checkNotesFraisAndLeaveDates(int employeeId, System.DateTime dateDebutConge, System.DateTime dateRepriseConge);
        List<NotesFrais> getValidatedNotesFraisByEmployeeId(int employeeId);
        int getNumeroNoteFrais();
        NotesFrais getNoteFraisByNumeroNote(string numero);
    }
}
