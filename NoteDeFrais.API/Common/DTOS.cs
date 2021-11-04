using Conge.Domain.Enum;
using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDeFrais.API.Common
{
    public class DTOS
    {
        public record ordremissionDto(
              int Id ,
         string UserCreat ,
         string UserModif ,
         DateTime DateCreat ,
        DateTime? DateModif ,
         int companyID ,
           string Titre ,
      
        DateTime DateDebut ,
       
        DateTime DateFin ,
     int? PaysIdConsumed ,
        string Code ,
       string Intitule ,

      string DeviseCode ,

        int EmployeeIDConsumed ,
        string UserCreatE, string UserModifE, DateTime DateCreatE, DateTime? DateModifE, int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne
            ,  int TypeMissionOrderId ,
        string NumeroOM ,
           string Description );



        public record employegroupDto(
               int Id,
         string UserCreat,
         string UserModif,
         DateTime DateCreat,
        DateTime? DateModif,
         int companyID,
           DateTime DateAffectation ,
         DateTime DateFinAffectation ,
       int GroupeFraisID ,
     GroupeFrais GroupeFrais ,
      int EmployeeIDConsumed ,
              string UserCreatE, string UserModifE, DateTime DateCreatE, DateTime? DateModifE, int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne );

    }

    public record notefraisDto(
           int Id,
         string UserCreat,
         string UserModif,
         DateTime DateCreat,
        DateTime? DateModif,
         int companyID,
            string Code ,
     DateTime DateDebut ,
    DateTime DateFin ,
   DateTime DateNote ,
 string NumeroNote ,
    int EmployeeIDConsumed ,
                  string UserCreatE, string UserModifE, DateTime DateCreatE, DateTime? DateModifE, int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne,
    string Validateur ,
    int? OrdreMissionId ,
    float TotalKm ,
    float TotalTTC ,
   float TotalRembourser ,
     string Description ,
    string Commentaire 
  
        );
    public record depenseDto(
          int Id,
         string UserCreat,
         string UserModif,
         DateTime DateCreat,
        DateTime? DateModif,
         int companyID,
          string Titre,
    DateTime DateDepense,
    Boolean Facturable ,
    string Libelle ,
     string Commentaire ,
   string Client ,
    string ReferenceCommande ,
     string Reference2 ,
     float TVA ,
    float TTC ,
     float TotalRemboursable ,
    int? NotesFraisID ,
    
     int? TypeDepenseID ,
 
    int? MoyenPaiementID ,
  
     int? DeviseIDConsumed ,
     int Decimal,


    float ExchangeRate ,
    int? PaysIDConsumed ,
       string Code,
       string Intitule,

      string DeviseCode,


    bool Warning  );

}
