using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Domain.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conge.Data.Common.DTOS;

namespace Conge.Data.Common
{
  public static  class Extentions
    {
        public static DetailsDroitCongeViewModel AssDto(this Details_DroitConge item, string Nom, string Prenom)
        {
            return new DetailsDroitCongeViewModel(item.Id, item.DateModif, item.DateCreat, item.UserCreat, item.UserModif, item.Commentaire, item.companyID, item.Droit, item.DroitCongeId, item.DroitMisAJour, item.IdEmployee, Nom, Prenom); 
        }
        public static MvtCongeDto AssDtoo(this MvtConge item, DateTime? DateModifE,
         DateTime DateCreatE,
           string UserCreatE,
           string UserModifE,
          int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? RegimeTravailID, Boolean ConsultantExterne)
        {
            return new MvtCongeDto(item.Id, item.DateModif, item.DateCreat, item.UserCreat, item.UserModif, item.companyID, item.Date, item.SoldeApres, item.NbreJours, item.TypeCongeID, item.IdEmployee, DateModifE, DateCreatE, UserCreatE, UserModifE, companyIDE, NumeroPersonne, Nom, Prenom, DateNaissance, CIN, DeliveryDateCin, PlaceCin, PassportNumber, ValidityDateRP, RecruitementDate, TitularizationDate, Tel, TelGSM, Mail, Langue, Adresse, Ville, CodePostal, User, RegimeTravailID, ConsultantExterne, item.Sens);
        }
        //public static MvtCongeDto AssTESt( MvtConge item1, Employee item2)
        //{
        //    return new MvtCongeDto(item1.Id, item1.DateModif, item1.DateCreat, item1.UserCreat, item1.UserModif, item1.Date, item1.SoldeApres, item1.NbreJours, item1.TypeCongeID, item1.IdComapny, item1.IdEmployee, item1.Sens, item2.NumeroPersonne, item2.Nom, item2.Prenom, item2.DateNaissance, item2.CIN, item2.Gender, item2.MaritalStatus, item2.DeliveryDateCin, item2.PlaceCin, item2.PassportNumber, item2.ValidityDateRP, item2.RecruitementDate, item2.TitularizationDate, item2.Tel, item2.TelGSM, item2.Mail, item2.Langue, item2.Adresse, item2.Ville, item2.CodePostal, item2.Photo, item2.User, item2.ConsultantExterne);
        //}
        public static SoldeCongeDto AsSoldeConge(this SoldeConge item, DateTime? DateModifE,
         DateTime DateCreatE,
           string UserCreatE,
           string UserModifE,
          int companyIDE, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN,   DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? RegimeTravailID, Boolean ConsultantExterne)
        {
            return new SoldeCongeDto(item.Id, item.DateModif, item.DateCreat, item.UserCreat, item.UserModif, item.companyID, item.Solde, item.Annee, item.TypeCongeID, item.IdEmployee, DateModifE, DateCreatE, UserCreatE, UserModifE, companyIDE, NumeroPersonne, Nom, Prenom, DateNaissance, CIN, DeliveryDateCin, PlaceCin, PassportNumber, ValidityDateRP, RecruitementDate, TitularizationDate, Tel, TelGSM, Mail, Langue, Adresse, Ville, CodePostal,  User, RegimeTravailID, ConsultantExterne);
        }
        public static TitreCongeDto AsTitreConge(this TitreConge item,
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
       //    int? RegimeTravailIDR,
       Boolean ConsultantExterneR
           )
        {
            return new TitreCongeDto(item.Id, item.DateModif, item.DateCreat, item.UserCreat, item.UserModif, item.companyID, item.DateDemande, item.DateDebutConge, item.DateRepriseConge, item.NbrConge, item.IsApremDebut, item.IsApremRetour, item.NumeroTitre, item.Commentaire, item.NbrBonification, item.Statut,item.DelegationId, item.TypeCongeID, item.NumeroDemande, item.IdEmployee, DateModifE, DateCreatE, UserCreatE, UserModifE, companyIDE, NumeroPersonne, Nom, Prenom, DateNaissance, CIN,DeliveryDateCin, PlaceCin, PassportNumber, ValidityDateRP, RecruitementDate, TitularizationDate, Tel, TelGSM, Mail, Langue, Adresse, Ville, CodePostal, User, RegimeTravailID, ConsultantExterne, item.IdRemplacant, DateModifR, DateCreatR, UserCreatR, UserModifR, companyIDR, NumeroPersonneR, NomR, PrenomR, DateNaissanceR, CINR, DeliveryDateCinR, PlaceCinR, PassportNumberR, ValidityDateRPR, RecruitementDateR, TitularizationDateR, TelR, TelGSMR, MailR, LangueR, AdresseR, VilleR, CodePostalR,  UserR, ConsultantExterneR);



        }
    }
    
    }
   


