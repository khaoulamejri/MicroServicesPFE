

using NoteDeFrais.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
  public  class Contracts
    {
        public record CompanyGetID(int IdCompany);
        public record AffectationEmployeeCreated(int IdAffectationEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, int EmployeeID, DateTime DateDebut, DateTime DateFin, int PositionID, Boolean Principal);
        public record AffectationEmployeeUpdated(int IdAffectationEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, int EmployeeID, DateTime DateDebut, DateTime DateFin, int PositionID, Boolean Principal);
        public record AffectationEmployeeDeleted(int IdAffectationEmployee);
        //public record EmployeUpdated(Guid IdEmploye, string NumeroPersonne, string Nom);
        public record PaysUserCreated(int PaysIdConsumed, string Code, string Intitule, string DeviseCode);
          public record PayNoteCreated(int PaysIdConsumed, string Code, string Intitule, string DeviseCode);
        public record PayCreated(int PaysIdConsumed, string Code, string Intitule, string DeviseCode);
        public record PaysCreated(int PaysIdCon, string Code, string Intitule,string DeviseCode);
        public record PaysUpdated(int PaysIdConsu, string Code, string Intitule, string DeviseCode);
        public record PaysDeleted(int PaysIdConsumed);
        public record DeviseCreated(int DeviseIDConsumed, string Code, string Intitule, int Decimal, float ExchangeRate, DateTime? DateModif);
        public record DeviseCongeCreated(int DeviseIDConsumed, string Code, string Intitule, int Decimal, float ExchangeRate, DateTime? DateModif);
        public record DeviseUpdated(int DeviseIDConsumed, string Code, string Intitule, int Decimal, float ExchangeRate, DateTime? DateModif);
        public record DeviseDeleted(int DeviseIDConsumed);
        public record EmployeeNoteCreated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne);
        public record EmployeeCongeCreated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne);

        public record EmployeeUserCreated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne);
        public record EmployeeUserUpdated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne);
        public record EmployeeUpdated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne);
        public record EmployeeCongeUpdated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailID, Boolean ConsultantExterne);
        public record EmployeeUserDeleted(int IdEmployee);
        public record WFDocumentCreated(int IdWFDocument, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string TypeDocument, bool Finished, int AffectedToId, int DocumentId);
        public record DeviseNote(int DeviseIdConsumed, int Decimal);
        public record DemandeCongeNoteCreated(int IdDemandeConge, DateTime DateDebutConge, DateTime DateRepriseConge, StatusDocument Statut, int EmployeeIDConsumed);
        //   public record WFDocumentCreated(int IdDeviseConsumed, string TypeDocument, bool Finished, int AffectedToId, int DocumentId);
        public record EmployeeCongeated(int IdEmployee, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string NumeroPersonne, string Nom, string Prenom, DateTime? DateNaissance, string CIN, RhGendar? Gender, RhMaritalStatus? MaritalStatus , DateTime? DeliveryDateCin, string PlaceCin, string PassportNumber, DateTime? ValidityDateRP, DateTime? RecruitementDate, DateTime? TitularizationDate, string Tel, string TelGSM, string Mail, string Langue, string Adresse, string Ville, string CodePostal, byte[] Photo, string User, int? PlanDroitCongeIDConsumed, int? RegimeTravailIDConsumed, Boolean ConsultantExterne);
        public record CompanyConsumed(int IdCompan, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, string Name, string Description, string Adress, string Telephone, string LegalStatus, string FiscalNumber, string TradeRegister, string Numero, string CodePostal, string Ville, string ComplementAdresse);
        public record CompanynoteConsumed(int IdCompan, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, string Name, string Description, string Adress, string Telephone, string LegalStatus, string FiscalNumber, string TradeRegister, string Numero, string CodePostal, string Ville, string ComplementAdresse);
        public record UniteConsumed(int IdUnite, string UserCreat, string UserModif, DateTime DateCreat, DateTime? DateModif, int companyID, string Code, string Intitule);
    }
}
