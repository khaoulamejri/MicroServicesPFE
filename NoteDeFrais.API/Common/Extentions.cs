using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static NoteDeFrais.API.Common.DTOS;

namespace NoteDeFrais.API.Common
{
    public static class Extentions
    {

        public static ordremissionDto AsOrdreMisiion(this OrdreMission item, string Code, string Intitule, string DeviseCode, string UserCreatE, string UserModifE, DateTime DateCreatE, DateTime? DateModifE, int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne)
        {
            return new ordremissionDto(item.Id,
         item.UserCreat,
          item.UserModif,
         item.DateCreat,
        item.DateModif,
         item.companyID,
           item.Titre,

         item.DateDebut,

         item.DateFin,
          item.PaysIdConsumed,
         Code,
       Intitule,

    DeviseCode,

         item.EmployeeIDConsumed,
                 UserCreatE, UserModifE, DateCreatE, DateModifE, companyIDE, NumeroPersonne, Nom, Prenom, DateNaissance, CIN, DeliveryDateCin, PlaceCin, PassportNumber, ValidityDateRP, RecruitementDate, TitularizationDate, Tel, TelGSM, Mail, Langue, Adresse, Ville, CodePostal, User, PlanDroitCongeIDConsumed, RegimeTravailID, ConsultantExterne,
                  item.TypeMissionOrderId,
         item.NumeroOM,
         item.Description);
        }

        public static employegroupDto AsEmpGroup(this EmployeeGroupe item, string UserCreatE, string UserModifE, DateTime DateCreatE, DateTime? DateModifE, int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne)
        {
            return new employegroupDto(item.Id,
         item.UserCreat,
          item.UserModif,
         item.DateCreat,
        item.DateModif,
          item.companyID,
         item.DateAffectation,
          item.DateFinAffectation,
         item.GroupeFraisID,


       item.GroupeFrais,
        item.EmployeeIDConsumed,
                         UserCreatE, UserModifE, DateCreatE, DateModifE, companyIDE, NumeroPersonne, Nom, Prenom, DateNaissance, CIN, DeliveryDateCin, PlaceCin, PassportNumber, ValidityDateRP, RecruitementDate, TitularizationDate, Tel, TelGSM, Mail, Langue, Adresse, Ville, CodePostal, User, PlanDroitCongeIDConsumed, RegimeTravailID, ConsultantExterne);


    }
        public static notefraisDto AsNotefrais(this NotesFrais item, string UserCreatE, string UserModifE, DateTime DateCreatE, DateTime? DateModifE, int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne)
        {
            return new notefraisDto(item.Id,
         item.UserCreat,
          item.UserModif,
         item.DateCreat,
        item.DateModif,
          item.companyID,
              item.Code ,
           item.DateDebut ,
          item.DateFin ,
         item.DateNote,
         item.NumeroNote ,
          item.EmployeeIDConsumed ,
     UserCreatE, UserModifE, DateCreatE, DateModifE, companyIDE, NumeroPersonne, Nom, Prenom, DateNaissance, CIN, DeliveryDateCin, PlaceCin, PassportNumber, ValidityDateRP, RecruitementDate, TitularizationDate, Tel, TelGSM, Mail, Langue, Adresse, Ville, CodePostal, User, PlanDroitCongeIDConsumed, RegimeTravailID, ConsultantExterne , item.Validateur, item.OrdreMissionId, item.TotalKm, item.TotalTTC, item.TotalRembourser, item.Description, item.Commentaire);

    }
        public static depenseDto AsDepense ( this Depenses item, int Decimal, string Code, string Intitule, string DeviseCode)
        {
            return new depenseDto( 
                item.Id,
                 item.UserCreat,
          item.UserModif,
         item.DateCreat,
        item.DateModif,
          item.companyID,
               item.Titre,
item.DateDepense,
    item.Facturable,
   item.Libelle,
     item.Commentaire,
   item.Client,
    item.ReferenceCommande,
     item.Reference2,
    item.TVA,
    item.TTC,
     item.TotalRemboursable,
    item.NotesFraisID ,

     item.TypeDepenseID ,

    item.MoyenPaiementID ,

     item.DeviseIDConsumed ,
     Decimal,


    item.ExchangeRate,
    item.PaysIDConsumed ,
        Code,
        Intitule,

      DeviseCode,


   item.Warning
                );
        }

    }
}