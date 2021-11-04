using Conge.Data;
using Conge.Data.Common;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conge.Data.Common.DTOS;

namespace Conge.Services.services
{
    public class TitreCongeServices : ITitreCongeServices

    {

        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IMvtCongeServices mvtCongeServices;
        private readonly IEmployeeServices employeeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDelegationService _delegationService;

        public TitreCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor,
            IMvtCongeServices mvt, IEmployeeServices emp, IDelegationService delegationService)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            mvtCongeServices = mvt;
            employeeServices = emp;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);

            _delegationService = delegationService;
        }

        public List<TitreCongeDto> GetAllTitreConge()
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
          
            var result = from d in _db.titreConge
                              //   join e in _db.Employee on d.EmployeeID equals e.Id
                          join t in _db.typeConge on d.TypeCongeID equals t.Id
                          // join remp in _db.Employee on d.Remplacant equals remp.Id into r
                          // from remp in r.DefaultIfEmpty()
                          where d.companyID == currentCompanyId
                          select new TitreConge
                          {
                              Id = d.Id,
                              DateModif = d.DateModif,
                              DateCreat = d.DateCreat,
                              UserCreat = d.UserCreat,
                              UserModif = d.UserModif,
                              Commentaire = d.Commentaire,
                              companyID = d.companyID,
                              DateDebutConge = d.DateDebutConge,
                              DateDemande = d.DateDemande,
                              DateRepriseConge = d.DateRepriseConge,
                              NbrConge = d.NbrConge,
                              IsApremDebut = d.IsApremDebut,
                              IsApremRetour = d.IsApremRetour,
                              NbrBonification = d.NbrBonification,
                              NumeroDemande = d.NumeroDemande,
                              NumeroTitre = d.NumeroTitre,
                              Statut = d.Statut,
                              IdEmployee = d.IdEmployee,
                              //  Employee = e,
                              IdRemplacant = d.IdRemplacant,
                              //    EmpRp = remp,
                              TypeCongeID = d.TypeCongeID,
                              TypeConge = t
                          };
            var titre = result.Last();
            var empl = utOfWork.EmployeeRepository.Get(a => a.Id == titre.IdEmployee);
            var remplacants = employeeServices.GetAllEmployeeRemplacant(titre.IdRemplacant);
               var remp = remplacants.Single(aa => aa.Id == titre.IdRemplacant);
          //  var TitreCongeDto = titre.AsTitreConge(empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal,empl.User, empl.ConsultantExterne, remp.DateModifR, remp.DateCreatR, remp.UserCreatR, remp.UserModifR, remp.companyIDR, remp.NumeroPersonneR, remp.NomR, remp.PrenomR, remp.DateNaissanceR, remp.CINR, remp.GenderR, remp.MaritalStatusR, remp.DeliveryDateCinR, remp.PlaceCinR, remp.PassportNumberR, remp.ValidityDateRPR, remp.RecruitementDateR, remp.TitularizationDateR, remp.TelR, remp.TelGSMR, remp.MailR, remp.LangueR, remp.AdresseR, remp.VilleR, remp.CodePostalR, remp.PhotoR, remp.UserR, remp.ConsultantExterneR);

           var TitreCongeDto = titre.AsTitreConge(empl.DateModif, empl.DateCreat, empl.UserCreat, empl.UserModif, empl.companyID,
             empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User,empl.RegimeTravailID, empl.ConsultantExterne, remp.DateModifR, remp.DateCreatR, remp.UserCreatR, remp.UserModifR, remp.companyIDR, remp.NumeroPersonneR, remp.NomR, remp.PrenomR, remp.DateNaissanceR, remp.CINR,   remp.DeliveryDateCinR, remp.PlaceCinR, remp.PassportNumberR, remp.ValidityDateRPR, remp.RecruitementDateR, remp.TitularizationDateR, remp.TelR, remp.TelGSMR, remp.MailR, remp.LangueR, remp.AdresseR, remp.VilleR, remp.CodePostalR, remp.UserR,  remp.ConsultantExterneR);
            var listTitres =  new List<TitreCongeDto>();
            listTitres.Add(TitreCongeDto);

            return listTitres;
        }

        public TitreConge GetTitreCongeByID(int id)
        {
            return utOfWork.TitreCongeRepository.GetMany(d => d.Id == id).Include(obj => obj.TypeConge).First();
        }

        public List<TitreCongeDto> GetTitreCongeByEmployeeID(int id)
        {
            var _db = Context;
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);

            var result = from d in _db.titreConge
                             //   join e in _db.Employee on d.EmployeeID equals e.Id
                         join t in _db.typeConge on d.TypeCongeID equals t.Id
                         // join remp in _db.Employee on d.Remplacant equals remp.Id into r
                         // from remp in r.DefaultIfEmpty()
                          where d.IdEmployee == id
                         select new TitreConge
                         {
                             Id = d.Id,
                             DateModif = d.DateModif,
                             DateCreat = d.DateCreat,
                             UserCreat = d.UserCreat,
                             UserModif = d.UserModif,
                             Commentaire = d.Commentaire,
                             companyID = d.companyID,
                             DateDebutConge = d.DateDebutConge,
                             DateDemande = d.DateDemande,
                             DateRepriseConge = d.DateRepriseConge,
                             NbrConge = d.NbrConge,
                             IsApremDebut = d.IsApremDebut,
                             IsApremRetour = d.IsApremRetour,
                             NbrBonification = d.NbrBonification,
                             NumeroDemande = d.NumeroDemande,
                             NumeroTitre = d.NumeroTitre,
                             Statut = d.Statut,
                             IdEmployee = d.IdEmployee,
                             //  Employee = e,
                             IdRemplacant = d.IdRemplacant,
                             //    EmpRp = remp,
                             TypeCongeID = d.TypeCongeID,
                             TypeConge = t
                         };
            var titre = result.Last();
            var empl = utOfWork.EmployeeRepository.Get(a => a.Id == titre.IdEmployee);
            var remplacants = employeeServices.GetAllEmployeeRemplacant(titre.IdRemplacant);
            var remp = remplacants.Single(aa => aa.Id == titre.IdRemplacant);
            //    var TitreCongeDto = titre.AsTitreConge(empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal,empl.User, empl.ConsultantExterne, remp.DateModifR, remp.DateCreatR, remp.UserCreatR, remp.UserModifR, remp.companyIDR, remp.NumeroPersonneR, remp.NomR, remp.PrenomR, remp.DateNaissanceR, remp.CINR, remp.GenderR, remp.MaritalStatusR, remp.DeliveryDateCinR, remp.PlaceCinR, remp.PassportNumberR, remp.ValidityDateRPR, remp.RecruitementDateR, remp.TitularizationDateR, remp.TelR, remp.TelGSMR, remp.MailR, remp.LangueR, remp.AdresseR, remp.VilleR, remp.CodePostalR, remp.PhotoR, remp.UserR, remp.ConsultantExterneR);
            var TitreCongeDto = titre.AsTitreConge(empl.DateModif, empl.DateCreat, empl.UserCreat, empl.UserModif, empl.companyID,
               empl.NumeroPersonne, empl.Nom, empl.Prenom, empl.DateNaissance, empl.CIN, empl.DeliveryDateCin, empl.PlaceCin, empl.PassportNumber, empl.ValidityDateRP, empl.RecruitementDate, empl.TitularizationDate, empl.Tel, empl.TelGSM, empl.Mail, empl.Langue, empl.Adresse, empl.Ville, empl.CodePostal, empl.User, empl.RegimeTravailID, empl.ConsultantExterne, remp.DateModifR, remp.DateCreatR, remp.UserCreatR, remp.UserModifR, remp.companyIDR, remp.NumeroPersonneR, remp.NomR, remp.PrenomR, remp.DateNaissanceR, remp.CINR, remp.DeliveryDateCinR, remp.PlaceCinR, remp.PassportNumberR, remp.ValidityDateRPR, remp.RecruitementDateR, remp.TitularizationDateR, remp.TelR, remp.TelGSMR, remp.MailR, remp.LangueR, remp.AdresseR, remp.VilleR, remp.CodePostalR, remp.UserR, remp.ConsultantExterneR);
            var listTitres = new List<TitreCongeDto>();
            listTitres.Add(TitreCongeDto);

            return listTitres;
        }

        public TitreConge Create(TitreConge TitreConge)
        {

            try
            {
                int numero = 0;
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                TitreConge.companyID = currentCompanyId;
                DemandeConge d = utOfWork.DemandeCongeRepository.GetMany(dd => dd.NumeroDemande == TitreConge.NumeroDemande && dd.companyID == currentCompanyId).FirstOrDefault();
                TitreConge.companyID = d.companyID;
                numero = utOfWork.TitreCongeRepository.GetMany(t => t.companyID == currentCompanyId).Include(obj => obj.TypeConge).AsEnumerable().Select(p => Convert.ToInt32(p.NumeroTitre)).DefaultIfEmpty(0).Max();
                numero++;
                TitreConge.NumeroTitre = numero.ToString();
                utOfWork.TitreCongeRepository.Add(TitreConge);
                utOfWork.Commit();
                MvtConge mvtConge = mvtCongeServices.ValidateMvtSoldeCongeFromDemande(TitreConge.IdEmployee, TitreConge.TypeCongeID, TitreConge.DateDebutConge, TitreConge.companyID, -TitreConge.NbrConge, RhSens.Titre);
                return TitreConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TitreConge Edit(TitreConge TitreConge)
        {
            try
            {
                utOfWork.TitreCongeRepository.Update(TitreConge);
                utOfWork.Commit();
                return TitreConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public double GetAutomaticNumber(DateTime startDate, DateTime endDate, int EmployeeId, bool matinYNdeb, bool MatinYNfin)
        {
            throw new NotImplementedException();
        }

        public TitreConge GenererTitre(DemandeConge DemandeConge)
        {
            if (DemandeConge.Statut == StatusDocument.valider)
            {
                TitreConge TitreConge = new TitreConge();
                TitreConge.DateDebutConge = DemandeConge.DateDebutConge;
                TitreConge.DateRepriseConge = DemandeConge.DateRepriseConge;
                TitreConge.NbrConge = DemandeConge.NbrConge;
                TitreConge.IdRemplacant = DemandeConge.IdRemplacant;
                TitreConge.IsApremDebut = DemandeConge.IsApremDebut;
                TitreConge.IsApremRetour = DemandeConge.IsApremRetour;
                TitreConge.Statut = DemandeConge.Statut;
                TitreConge.TypeCongeID = DemandeConge.TypeCongeID;
                TitreConge.NumeroDemande = DemandeConge.NumeroDemande;
                TitreConge.TypeConge = DemandeConge.TypeConge;
                TitreConge.IdEmployee= DemandeConge.IdEmployee;
                TitreConge.NbrBonification = DemandeConge.NbrBonification;
                TitreConge.Commentaire = DemandeConge.Commentaire;
                TitreConge titre = Create(TitreConge);
                if (TitreConge.IdRemplacant!= null)
                {
                    Delegation delegation = new Delegation();
                    delegation.DateDebut = DemandeConge.DateDebutConge;
                    delegation.DateFin = DemandeConge.DateRepriseConge;
                    delegation.DateModif = DateTime.UtcNow;
                    delegation.TitreId = titre.Id;
                    delegation.companyID = DemandeConge.companyID;
                    delegation.IdEmployee = DemandeConge.IdEmployee;
                    delegation.IdRemplacant = (int)DemandeConge.IdRemplacant;
                    delegation.Statut = StatusDelegation.encours;
                    delegation.DateCreat = DateTime.UtcNow;
                    delegation.Titre = TitreConge;
                    delegation = _delegationService.Create(delegation);
                    titre.DelegationId = delegation.Id;
                    titre.Delegation = delegation;
                    TitreConge titreDelegation = Edit(titre);

                }
                return titre;
            }
            return null;
        }

        
    }
    }
