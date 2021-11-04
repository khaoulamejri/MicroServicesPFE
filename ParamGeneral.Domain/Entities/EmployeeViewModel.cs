using ParamGeneral.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class EmployeeViewModel : BaseModel
    {
        public string NumeroPersonne { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateNaissance { get; set; }
        public string CIN { get; set; }
        public RhGendar? Gender { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDateCin { get; set; }
        public string PlaceCin { get; set; }
        public DateTime? RecruitementDate { get; set; }
        [Phone]
        public string Tel { get; set; }
        [Phone]
        public string TelGSM { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public int? PlanDroitCongeIDConsumed { get; set; }
        //public virtual PlanDroitConge PlanDroitConge { get; set; }
        public int? RegimeTravailID { get; set; }
        public virtual RegimeTravail RegimeTravail { get; set; }
        //public virtual ICollection<WFDocument> WFDocument { get; set; }
        //public virtual ICollection<WFDocument> WFDocumentReq { get; set; }
        [JsonIgnore]
        public virtual ICollection<Document> Documents { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeVehicule> EmployeeVehicule { get; set; }
        [JsonIgnore]
        public virtual ICollection<AffectationEmployee> AffectationEmployee { get; set; }
     //   public virtual ICollection<EmployeeGroupe> EmployeeGroupe { get; set; }
        public RhMaritalStatus? MaritalStatus { get; set; }
        public string PassportNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ValidityDateRP { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TitularizationDate { get; set; }
        public string Langue { get; set; }
     
        public string User { get; set; }
        //public virtual ICollection<DemandeConge> DemandeConge { get; set; }
        //public virtual ICollection<NotesFrais> NotesFrais { get; set; }
        //public virtual ICollection<TitreConge> TitreConge { get; set; }
        //public virtual ICollection<SoldeConge> SoldeConge { get; set; }
        //public virtual ICollection<MvtConge> MvtConge { get; set; }
        //public int? LeaveWorkflowID { get; set; }
        //public int? ExpenseWorkflowID { get; set; }
        //public int? OMWorkflowID { get; set; }
        //public int? TitreCongeWorkflowID { get; set; }
        public Boolean ConsultantExterne { get; set; }
        public Employee MappingAddEmployee(EmployeeViewModel e1, Employee e2)
        {
            e2.DateModif = e1.DateModif;
            e2.UserModif = e1.UserModif;
            e2.companyID = e1.companyID;
            e2.NumeroPersonne = e1.NumeroPersonne;
            e2.Nom = e1.Nom;
            e2.Prenom = e1.Prenom;
            e2.DateNaissance = e1.DateNaissance;
            e2.CIN = e1.CIN;
            e2.Gender = e1.Gender;
            e2.DeliveryDateCin = e1.DeliveryDateCin;
            e2.PlaceCin = e1.PlaceCin;
            e2.RecruitementDate = e1.RecruitementDate;
            e2.Tel = e1.Tel;
            e2.TelGSM = e1.TelGSM;
            e2.Mail = e1.Mail;
            e2.Adresse = e1.Adresse;
            e2.Ville = e1.Ville;
            e2.CodePostal = e1.CodePostal;
            e2.PlanDroitCongeIDConsumed = e1.PlanDroitCongeIDConsumed;
            e2.RegimeTravailID = e1.RegimeTravailID;
            e2.RegimeTravail = e1.RegimeTravail;
            e2.MaritalStatus = e1.MaritalStatus;
            e2.PassportNumber = e1.PassportNumber;
            e2.ValidityDateRP = e1.ValidityDateRP;
            e2.TitularizationDate = e1.TitularizationDate;
           
           
            e2.Langue = e1.Langue;
          
         
            e2.User = e1.User;
            e2.ConsultantExterne = e1.ConsultantExterne;
           
            //e2.DemandeConge = e1.DemandeConge;
            //e2.NotesFrais = e1.NotesFrais;
            //e2.TitreConge = e1.TitreConge;
            //e2.SoldeConge = e1.SoldeConge;
          
            //e2.MvtConge = e1.MvtConge;
            //e2.WFDocument = e1.WFDocument;
            //e2.WFDocumentReq = e1.WFDocumentReq;
          //e2.Documents = e1.Documents;
        //  e2.AffectationEmployee = e1.AffectationEmployee;
         // e2.EmployeeVehicule = e1.EmployeeVehicule;
          //  e2.EmployeeGroupe = e1.EmployeeGroupe;
         // e2.RegimeTravail = e1.RegimeTravail;
         // e2.PlanDroitCongeIDConsumed = e1.PlanDroitCongeIDConsumed;
            //e2.companyID = e1.companyID;
            //e2.LeaveWorkflowID = e1.LeaveWorkflowID;
            //e2.ExpenseWorkflowID = e1.ExpenseWorkflowID;
            //e2.OMWorkflowID = e1.OMWorkflowID;
            //e2.TitreCongeWorkflowID = e1.TitreCongeWorkflowID;
            return e2;
        }
        public string RandomPassword()
        {
            string lettre = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string caractere = "!-_*+&$";
            string number = "0123456789";
            string ensemble = "";
            ensemble += lettre;
            ensemble += number;
            ensemble += caractere;
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            // Ici, ensemble contient donc la totalité des caractères autorisés
            string password = "";
            int taillePwd = 8;
            Random rand = new Random();
            for (int i = 0; i < taillePwd; i++)
            {
                // On ajoute un caractère parmi tous les caractères autorisés
                password += ensemble[rand.Next(0, ensemble.Length)];
                if ((password.Length == 8) && (!hasLowerChar.IsMatch(password) || !hasUpperChar.IsMatch(password)))
                {

                    i = -1;
                    password = "";
                }

            }
            return password;
        }
    }
}