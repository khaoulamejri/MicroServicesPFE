using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IMoyenPaiementServices
    {
        MoyenPaiement GetMoyenPaiementByID(int id);
        List<MoyenPaiement> GetAllMoyenPaiement();
        MoyenPaiement Create(MoyenPaiement moyenPaiement);
        MoyenPaiement Edit(MoyenPaiement moyenPaiement);
        MoyenPaiement Delete(int moyenPaiementId);
        bool checkUnicity(MoyenPaiement moyenPaiement, bool create);



    }
}
