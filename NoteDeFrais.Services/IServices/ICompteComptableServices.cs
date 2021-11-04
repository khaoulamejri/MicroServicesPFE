using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface ICompteComptableServices
    {
        CompteComptable GetCompteComptableByID(int id);
        List<CompteComptable> GetAllCompteComptableByCompanyID();
        CompteComptable Create(CompteComptable compteComptable);
        CompteComptable Edit(CompteComptable compteComptable);
        CompteComptable Delete(int compteComptableId);
        bool checkUnicity(CompteComptable compteComptable);
    }
}
