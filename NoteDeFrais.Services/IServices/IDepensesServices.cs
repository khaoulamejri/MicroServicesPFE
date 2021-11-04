using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IDepensesServices
    {
        Depenses GetDepensesByID(int id);
        List<Depenses> GetAllDepensesIncludedByNoteFrais(int noteFraisId);
        List<Depenses> GetAllDepensesByNoteFraisId(int noteFraisId);
        List<Depenses> GetAllDepenses();
        Depenses Create(Depenses depenses);
        Depenses Edit(Depenses depenses);
        Depenses Delete(int depensesId);
        bool checkDateInNotePeriod(Depenses depense, DateTime dateDebutNote, DateTime dateFinNote);
    }
}
