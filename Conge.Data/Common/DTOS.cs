using Conge.Domain.Entities;
using Conge.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data.Common
{
   public class DTOS
    {
        public record DetailsDroitCongeViewModel(
           int Id,
            DateTime? DateModif,
            DateTime DateCreat,
            string UserCreat,
            string UserModif,
            string Commentaire,
            int companyID,
            float Droit,
            int DroitCongeId,
            float DroitMisAJour,
            int IdEmployee,
            string Nom,
            string Prenom);
        /*************************/
        public record MvtCongeDto(
           int Id,
            DateTime? DateModif,
            DateTime DateCreat,
            string UserCreat,
            string UserModif,
            int companyID,
          DateTime Date,
        float SoldeApres ,
         float NbreJours,
       int TypeCongeID ,
        
        int IdEmployee,
         DateTime? DateModifE,
            DateTime DateCreatE,
            string UserCreatE,
            string UserModifE,
            int companyIDE,
        string NumeroPersonne,
        string Nom,
        string Prenom,
        DateTime? DateNaissance,
        string CIN,

        DateTime? DeliveryDateCin,
        string PlaceCin,
        string PassportNumber,
        DateTime? ValidityDateRP,
        DateTime? RecruitementDate,
        DateTime? TitularizationDate,
        string Tel,
        string TelGSM,
        string Mail,
        string Langue,
        string Adresse,
        string Ville,
        string CodePostal,

        string User,
            int? RegimeTravailID,
        Boolean ConsultantExterne,
        RhSens Sens
      

            );
        /********************************/
        public record SoldeCongeDto(
             int Id,
            DateTime? DateModif,
            DateTime DateCreat,
            string UserCreat,
            string UserModif,
            int companyID,
            float Solde,
            int Annee,
            int TypeCongeID ,
            int IdEmployee,
                DateTime? DateModifE,
            DateTime DateCreatE,
            string UserCreatE,
            string UserModifE,
            int companyIDE,
        string NumeroPersonne,
        string Nom,
        string Prenom,
        DateTime? DateNaissance,
        string CIN,
      
        DateTime? DeliveryDateCin,
        string PlaceCin,
        string PassportNumber,
        DateTime? ValidityDateRP,
        DateTime? RecruitementDate,
        DateTime? TitularizationDate,
        string Tel,
        string TelGSM,
        string Mail,
        string Langue,
        string Adresse,
        string Ville,
        string CodePostal,
     
        string User,
            int? RegimeTravailID,
        Boolean ConsultantExterne
            );


        /*******************************/
        public record TitreCongeDto(
            int Id,
           DateTime? DateModif,
           DateTime DateCreat,
           string UserCreat,
           string UserModif,
           int companyID,
          DateTime DateDemande,
          DateTime DateDebutConge,
           DateTime DateRepriseConge,
           float NbrConge,
            bool IsApremDebut,
             bool IsApremRetour,
             string NumeroTitre,
             string Commentaire,
             float NbrBonification,
             StatusDocument Statut,
            int? DelegationId,
              int TypeCongeID,
          //    TypeConge TypeConge,
              string NumeroDemande,
               int IdEmployee,
              DateTime? DateModifE,
           DateTime DateCreatE,
           string UserCreatE,
          string UserModifE,
           int companyIDE,
                  string NumeroPersonne,
       string Nom,
       string Prenom,
       DateTime? DateNaissance,
       string CIN,
    
       DateTime? DeliveryDateCin,
       string PlaceCin,
       string PassportNumber,
       DateTime? ValidityDateRP,
       DateTime? RecruitementDate,
       DateTime? TitularizationDate,
       string Tel,
       string TelGSM,
       string Mail,
       string Langue,
       string Adresse,
       string Ville,
       string CodePostal,
     
       string User,
       int? RegimeTravailID,
       Boolean ConsultantExterne,
              
          int IdRemplacant,
         DateTime? DateModifR,
           DateTime DateCreatR,
           string UserCreatR,
           string UserModifR,
           int companyIDR,
       string NumeroPersonneR,
       string NomR,
       string PrenomR,
       DateTime? DateNaissanceR,
       string CINR,
       
       DateTime? DeliveryDateCinR,
       string PlaceCinR,
       string PassportNumberR,
       DateTime? ValidityDateRPR,
       DateTime? RecruitementDateR,
       DateTime? TitularizationDateR,
       string TelR,
       string TelGSMR,
       string MailR,
       string LangueR,
       string AdresseR,
       string VilleR,
       string CodePostalR,
       string UserR,
      //     int? RegimeTravailIDR,
     Boolean ConsultantExterneR
           );

        
    }
}
