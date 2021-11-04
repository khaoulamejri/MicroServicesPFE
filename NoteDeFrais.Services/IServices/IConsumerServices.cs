using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
 public   interface IConsumerServices
    {
        DemandeConge CreateDemande(DemandeConge Conge);
        Devise CreateDevise(Devise devise);
        WFDocument CreateWFDocument(WFDocument wFDocument);
        Pays CreatePay(Pays Pays);
    }
}
