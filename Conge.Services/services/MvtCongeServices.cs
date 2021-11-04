using Conge.Data;
using Conge.Data.Common;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Domain.ViewsModels;
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
    public class MvtCongeServices : IMvtCongeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly ISoldeCongeServices soldeCongeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MvtCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor ,
            ISoldeCongeServices soldservice)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            soldeCongeServices = soldservice;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }
        public MvtCongeDto GetMvtCongeByEmployeeIdtest(int EmployeeId)
        {
            
            // var result = from mvt in _db.mvtConge
            // join t in _db.typeConge on mvt.TypeCongeID equals t.Id
            //  select new { mvt, t };
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
           // var listeMvtConge = utOfWork.MvtCongeRepository.Get(d => d.companyID == currentCompanyId && d.IdEmployee == EmployeeId);

            var result = from mvt in _db.mvtConge
                         join t in _db.typeConge on mvt.TypeCongeID equals t.Id
                         where mvt.companyID == currentCompanyId && mvt.IdEmployee == EmployeeId
                         //  select new { mvt, t };
                         select new MvtConge
                         {
                             Id = mvt.Id,
                             companyID = currentCompanyId,
                             DateCreat = mvt.DateCreat,
                             DateModif = mvt.DateModif,
                             UserCreat = mvt.UserCreat,
                             UserModif = mvt.UserModif,
                             Date = mvt.Date,
                             NbreJours = mvt.NbreJours,
                             Sens = mvt.Sens,
                             SoldeApres = mvt.SoldeApres,
                             IdEmployee = EmployeeId,
                             TypeCongeID = mvt.TypeCongeID,
                             TypeConge = t
                         };
            var employe = utOfWork.EmployeeRepository.Get(a => a.Id == EmployeeId);
           // var employes = congeServices.GetAllEmploye();
          //  var empl = employes.Single(empl => empl.Id == demandeConge.IDEmployeeConsumed);
            var mvtConge = result.Last();
            var MvtCongeDtoo = mvtConge.AssDtoo(employe.DateModif, employe.DateCreat, employe.UserCreat, employe.UserModif, employe.companyID, employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.User, employe.RegimeTravailID, employe.ConsultantExterne);
            return MvtCongeDtoo;


            // var MvtCongeDto = listeMvtConge.AssDto(employe.Nom, employe.Prenom);
            // var MvtCongeDtoo = new MvtCongeDto();


        }
        public List<MvtCongeDto> GetMvtCongeByEmployeeIdtesttest(int EmployeeId)
        {
            List<MvtCongeDto> listDetails = new List<MvtCongeDto>();

            MvtCongeDto mvtlist = GetMvtCongeByEmployeeIdtest(EmployeeId);
            listDetails.Add(mvtlist);
            return listDetails;

        }
        public List<MvtConge> GetMvtCongeByEmployeeId(int EmployeeId)
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);

            List< MvtConge > listMvt = (from mvt in _db.mvtConge
                                         join t in _db.typeConge on mvt.TypeCongeID equals t.Id
                                         // join e in _db.Employee on mvt.EmployeeID equals e.Id
                                         where mvt.companyID == currentCompanyId && mvt.IdEmployee == EmployeeId
                                         select new MvtConge
                                         {
                                             Id = mvt.Id,
                                             companyID = currentCompanyId,
                                             DateCreat = mvt.DateCreat,
                                             DateModif = mvt.DateModif,
                                             UserCreat = mvt.UserCreat,
                                             UserModif = mvt.UserModif,
                                             Date = mvt.Date,
                                             NbreJours = mvt.NbreJours,
                                             Sens = mvt.Sens,
                                             SoldeApres = mvt.SoldeApres,
                                             IdEmployee = EmployeeId,
                                             //  Employee = e,
                                             TypeCongeID = mvt.TypeCongeID,
                                             TypeConge = t
                                         }).ToList();
            return listMvt;
           
        }
        /*********************/
        //public List<MvtCongeModel> GetMvtCongeByEmployeeIdTest2(int EmployeeId)
        //{
        //    var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
        //    int currentCompanyId = int.Parse(session[2].Value);
        //    //var listeDeroitConge = utOfWork.MvtCongeRepository.GetAll(d => d.companyID == currentCompanyId).Include(T => T.TypeConge).ToList();
        //    var listeDeroitConge = utOfWork.MvtCongeRepository.GetMany(d => d.companyID == currentCompanyId).ToList();

        //    var employe = utOfWork.EmployeeRepository.GetAll(a => a.Id == EmployeeId);

        //    List<MvtCongeModel> listMvtConge = new List<MvtCongeModel>();
        //    listMvtConge.Select(item => new DetailsDroitCongeViewModell
        //    {
        //        Id = listeDeroitConge.Id,
        //        DateModif = listeDeroitConge.DateModif,
        //        DateCreat = listeDeroitConge.DateCreat,
        //        UserCreat = listeDeroitConge.UserCreat,
        //        UserModif = listeDeroitConge.UserModif,
        //        Commentaire = listeDeroitConge.Commentaire,
        //        companyID = listeDeroitConge.companyID,
        //        Droit = listeDeroitConge.Droit,
        //        DroitCongeId = listeDeroitConge.DroitCongeId,
        //        DroitMisAJour = listeDeroitConge.DroitMisAJour,
        //        IdEmployee = listeDeroitConge.IdEmployee,
        //        Nom = employe.Nom,
        //        Prenom = employe.Prenom



        //    }).ToList();
        //    return listMvtConge;
        //}

        /********************/

        public List<MvtConge> GetMvtCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId)
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);

            return utOfWork.MvtCongeRepository.GetMany(d => d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId).ToList();
        }

        public MvtConge GetLastMvtCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId)
        {
            MvtConge MvtConge = new MvtConge();
            List<MvtConge> mvtlist = GetMvtCongeByEmployeeIdTypeCongeId(EmployeeId, TypeCongeId);
            MvtConge = mvtlist.OrderBy(d => d.Date).Last();
            return MvtConge;
           

            //return utOfWork.MvtCongeRepository.Get(d => d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId);
           // List<MvtConge> yy = mvtlist.OrderBy(d => d.Date).ToList();



        }

        public List<MvtConge> GetLastMvtCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId, bool Sens = false)
        {
            return utOfWork.MvtCongeRepository.GetMany(d => d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId).ToList();
        }

        public List<MvtConge> GetMvtCongeByEmployeeIdTypeCongeIdDate(int EmployeeId, int TypeCongeId, DateTime date, bool Sens = false)
        {
            List<MvtConge> MvtConge = new List<MvtConge>();
            List<MvtConge> mvtlist = GetMvtCongeByEmployeeIdTypeCongeId(EmployeeId, TypeCongeId);
            if (Sens)
                MvtConge.Add(mvtlist.Where(d => d.Date <= date).OrderBy(d => d.Date).LastOrDefault());
            else
                MvtConge = mvtlist.Where(d => d.Date > date).OrderBy(d => d.Date).ToList();
            return MvtConge;
        }

        public MvtConge Create(MvtConge MvtConge)
        {
            try
            {
                utOfWork.MvtCongeRepository.Add(MvtConge);
                utOfWork.Commit();
                soldeCongeServices.ValidateCongeFromMvtCongeFromOldSolde(MvtConge);
                return MvtConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public MvtConge Edit(MvtConge MvtConge)
        {

            try
            {
                utOfWork.MvtCongeRepository.Update(MvtConge);
                utOfWork.Commit();
                return MvtConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public MvtConge ValidateMvtSoldeConge(int EmployeeID, int TypeCongeID, DateTime date, int companyID, float nbreJours, RhSens sens)
        {
            try
            {
                MvtConge mCongeBefore = GetLastMvtCongeByEmployeeIdTypeCongeId(EmployeeID, TypeCongeID, true).LastOrDefault();
              //  List<MvtConge> ListmCongeAfter = GetMvtCongeByEmployeeIdTypeCongeIdDate(EmployeeID, TypeCongeID, date);
                MvtConge mvtConge = new MvtConge();
                mvtConge.companyID = companyID;
                mvtConge.Date = date;
                mvtConge.IdEmployee = EmployeeID;
                mvtConge.TypeCongeID = TypeCongeID;
                mvtConge.NbreJours = nbreJours;
                if (mCongeBefore != null)
                    mvtConge.SoldeApres = mCongeBefore.SoldeApres + nbreJours;
                else
                    mvtConge.SoldeApres = nbreJours;
             //   mvtConge.SoldeApres = mCongeBefore.SoldeApres + nbreJours;
                mvtConge.Sens = sens;
                //var employe = utOfWork.EmployeeRepository.Get(a => a.Id == mvtConge.IdEmployee);
                //var employe2 = utOfWork.EmployeeRepository.GetById( mvtConge.IdEmployee);
                //var company2 = utOfWork.CompanyRepository.Get(a => a.Id == mvtConge.IdComapny);

              //Employee e = utOfWork.EmployeeRepository.GetById(mvtConge.IdEmployee);
              //  Company c = utOfWork.CompanyRepository.GetById(e.companyID);
              //  mvtConge.companyID = c.Id;
                MvtConge mvtcreate = CreateFromGenerateDroit(mvtConge);
                return mvtcreate;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public MvtConge ValidateMvtSoldeCongeFromDemande(int EmployeeID, int TypeCongeID, DateTime date, int companyID, float nbreJours, RhSens sens)
        {
            try
            {
                MvtConge mCongeBefore = GetLastMvtCongeByEmployeeIdTypeCongeId(EmployeeID, TypeCongeID, true).LastOrDefault();
                List<MvtConge> ListmCongeAfter = new List<MvtConge>();
                ListmCongeAfter.Add(mCongeBefore);
                MvtConge mvtConge = new MvtConge();
                mvtConge.companyID = companyID;
                mvtConge.Date = date;
                mvtConge.IdEmployee = EmployeeID;
                mvtConge.TypeCongeID = TypeCongeID;
                mvtConge.NbreJours = Math.Abs(nbreJours);
                if (mCongeBefore != null)
                    mvtConge.SoldeApres = mCongeBefore.SoldeApres + nbreJours;
                else
                    mvtConge.SoldeApres = nbreJours;
                mvtConge.Sens = sens;
                //Employee e = employeeServices.GetEmployeeByID(mvtConge.EmployeeID);
                //Company c = companyService.GetCompanyByID(e.companyID);
                //**************************
                //Employee e = utOfWork.EmployeeRepository.GetById(mvtConge.IdEmployee);
                //Company c = utOfWork.CompanyRepository.GetById(e.companyID);
                //mvtConge.companyID = c.Id;
                MvtConge mvtcreate = Create(mvtConge);
                if (ListmCongeAfter.Any() && mvtcreate != null)
                {
                    MvtConge mConge = mvtcreate;
                }
                return mvtcreate;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public MvtConge AnnulerMvtSoldeCongeFromDemande(int EmployeeID, int TypeCongeID, DateTime date, int companyID, float nbreJours, RhSens sens)
        {

            try
            {
                MvtConge mCongeBefore = GetMvtCongeByEmployeeIdTypeCongeIdDate(EmployeeID, TypeCongeID, date, true).FirstOrDefault();
                List<MvtConge> ListmCongeAfter = new List<MvtConge>();
                ListmCongeAfter.Add(mCongeBefore);
                MvtConge mvtConge = new MvtConge();
                mvtConge.companyID = companyID;
                mvtConge.Date = date;
                mvtConge.IdEmployee = EmployeeID;
                mvtConge.TypeCongeID = TypeCongeID;
                mvtConge.NbreJours = Math.Abs(nbreJours);

                var solde = soldeCongeServices.GetAllSoldeCongeTotalByEmpId(EmployeeID);
                if (solde != 0)
                {
                    mvtConge.SoldeApres = solde + Math.Abs(nbreJours);
                }
                else
                    mvtConge.SoldeApres = Math.Abs(nbreJours);
                mvtConge.Sens = RhSens.Droit;
             //   Employee e = employeeServices.GetEmployeeByID(mvtConge.EmployeeID);
              //  Company c = companyService.GetCompanyByID(e.companyID);
              /*******************/
                //Employee e = utOfWork.EmployeeRepository.GetById(mvtConge.IdEmployee);
                //Company c = utOfWork.CompanyRepository.GetById(e.companyID);
                //mvtConge.companyID = c.Id;
                mvtConge.DateModif = DateTime.UtcNow;
                MvtConge mvtcreate = CreateTitreAnnule(mvtConge);
                if (ListmCongeAfter.Any() && mvtcreate != null)
                {
                    MvtConge mConge = mvtcreate;
                }
                return mvtcreate;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool ExistMvtCongeInDate(int TypeCongeID, DateTime date, int companyID, RhSens sens)
        {
            bool result = false;
            int i = 0;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            List<MvtConge> mvtlist = utOfWork.MvtCongeRepository.GetMany(d => d.companyID == currentCompanyId && d.TypeCongeID == TypeCongeID && d.Sens == sens).ToList();
            foreach (var item in mvtlist)
            {
                if (item.Date.Month == date.Month) i++;
            }
            if (i == mvtlist.Count && mvtlist.Any())
                result = true;
            return result;
        }

        public List<MvtConge> GetAllMvtConge()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);

            return utOfWork.MvtCongeRepository.GetMany(d => d.companyID == currentCompanyId).Include(T => T.TypeConge).ToList();
        }
        public MvtConge CreateTitreAnnule(MvtConge MvtConge)
        {
            try
            {
                utOfWork.MvtCongeRepository.Add(MvtConge);
                utOfWork.Commit();
                soldeCongeServices.AnnulerCongeFromMvtCongeFromOldSolde(MvtConge);
                return MvtConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public MvtConge CreateFromGenerateDroit(MvtConge MvtConge)
        {
            try
            {
                utOfWork.MvtCongeRepository.Add(MvtConge);
                utOfWork.Commit();
                soldeCongeServices.CreateOrUpdateFromMvtConge(MvtConge);
                return MvtConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
