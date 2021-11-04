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
using System.Transactions;
using static Conge.Data.Common.DTOS;

namespace Conge.Services.services
{
    public class SoldeCongeServices : ISoldeCongeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ITypeCongeServices TypeCongeServices;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SoldeCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor, ITypeCongeServices typeCongeServices)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            TypeCongeServices = typeCongeServices;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<SoldeConge> GetAllSoldeConge()
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            List<SoldeConge> listSoldes = (from d in _db.soldeConge
                                               // join e in _db.Employee on d.EmployeeID equals e.Id
                                           join t in _db.typeConge on d.TypeCongeID equals t.Id
                                           where d.companyID == currentCompanyId
                                           //&& e.ConsultantExterne == false
                                           select new SoldeConge
                                           {
                                               Id = d.Id,
                                               DateModif = d.DateModif,
                                               DateCreat = d.DateCreat,
                                               UserCreat = d.UserCreat,
                                               UserModif = d.UserModif,
                                               companyID = d.companyID,
                                               Annee = d.Annee,
                                               Solde = d.Solde,
                                               IdEmployee = d.IdEmployee,
                                               //Employee = e,
                                               TypeCongeID = d.TypeCongeID,
                                               TypeConge = t
                                           }).ToList();
            return listSoldes;
        }
        public SoldeCongeDto GetAllSoldeCongeTest() {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var result = from d in _db.soldeConge
                             //  join e in _db.Employee on d.EmployeeID equals e.Id
                         join t in _db.typeConge on d.TypeCongeID equals t.Id
                         where d.companyID == currentCompanyId
                         select new SoldeConge
                         {
                             Id = d.Id,
                             DateModif = d.DateModif,
                             DateCreat = d.DateCreat,
                             UserCreat = d.UserCreat,
                             UserModif = d.UserModif,
                             companyID = d.companyID,
                             Annee = d.Annee,
                             Solde = d.Solde,
                             IdEmployee = d.IdEmployee,
                             //Employee = e,
                             TypeCongeID = d.TypeCongeID,
                             TypeConge = t
                         };

            var soldeConge = result.Last();
            var employe = utOfWork.EmployeeRepository.Get(a => a.Id == soldeConge.IdEmployee && a.ConsultantExterne == false);
            var SoldeCongeDto = soldeConge.AsSoldeConge(employe.DateModif, employe.DateCreat, employe.UserCreat, employe.UserModif, employe.companyID, employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.User, employe.RegimeTravailID, employe.ConsultantExterne);
            //  if  (SoldeCongeDto.ConsultantExterne == false)
            return SoldeCongeDto;
        }

        public float GetAllSoldeCongeTotalByEmpId(int EmployeeId)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.SoldeCongeRepository.GetMany(a => a.IdEmployee == EmployeeId && a.companyID == currentCompanyId).Select(a => a.Solde).Sum();
        }

        public List<SoldeConge> GetSoldeCongeByEmployeeId(int EmployeeId)
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            List<SoldeConge> listSoldes = (from d in _db.soldeConge
                                               // join e in _db.Employee on d.EmployeeID equals e.Id
                                           join t in _db.typeConge on d.TypeCongeID equals t.Id
                                           where d.companyID == currentCompanyId && d.IdEmployee == EmployeeId
                                           select new SoldeConge
                                           {
                                               Id = d.Id,
                                               DateModif = d.DateModif,
                                               DateCreat = d.DateCreat,
                                               UserCreat = d.UserCreat,
                                               UserModif = d.UserModif,
                                               companyID = d.companyID,
                                               Annee = d.Annee,
                                               Solde = d.Solde,
                                               IdEmployee = d.IdEmployee,
                                               //  Employee = e,
                                               TypeCongeID = d.TypeCongeID,
                                               TypeConge = t
                                           }).ToList();
            return listSoldes;
        }
        public SoldeCongeDto GetSoldeCongeByEmployeeIdTest(int EmployeeId)
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var result = from d in _db.soldeConge
                             //  join e in _db.Employee on d.EmployeeID equals e.Id
                         join t in _db.typeConge on d.TypeCongeID equals t.Id
                         where d.companyID == currentCompanyId && d.IdEmployee == EmployeeId
                         select new SoldeConge
                         {
                             Id = d.Id,
                             DateModif = d.DateModif,
                             DateCreat = d.DateCreat,
                             UserCreat = d.UserCreat,
                             UserModif = d.UserModif,
                             companyID = d.companyID,
                             Annee = d.Annee,
                             Solde = d.Solde,
                             IdEmployee = d.IdEmployee,
                             //Employee = e,
                             TypeCongeID = d.TypeCongeID,
                             TypeConge = t
                         };

            var soldeConge = result.Last();
            var employe = utOfWork.EmployeeRepository.Get(a => a.Id == soldeConge.IdEmployee);
            //    var SoldeCongeDto = soldeConge.AsSoldeConge(employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.Gender, employe.MaritalStatus, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.Photo, employe.User, employe.ConsultantExterne);
            var SoldeCongeDto = soldeConge.AsSoldeConge(employe.DateModif, employe.DateCreat, employe.UserCreat, employe.UserModif, employe.companyID, employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.User, employe.RegimeTravailID, employe.ConsultantExterne);

            //  if  (SoldeCongeDto.ConsultantExterne == false)

            return SoldeCongeDto;
        }

        public List<SoldeConge> GetSoldeCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId)
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            List<SoldeConge> listSoldes = (from d in _db.soldeConge
                                               // join e in _db.Employee on d.EmployeeID equals e.Id
                                           join t in _db.typeConge on d.TypeCongeID equals t.Id
                                           where d.companyID == currentCompanyId && d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId
                                           select new SoldeConge
                                           {
                                               Id = d.Id,
                                               DateModif = d.DateModif,
                                               DateCreat = d.DateCreat,
                                               UserCreat = d.UserCreat,
                                               UserModif = d.UserModif,
                                               companyID = d.companyID,
                                               Annee = d.Annee,
                                               Solde = d.Solde,
                                               IdEmployee = d.IdEmployee,
                                               //  Employee = e,
                                               TypeCongeID = d.TypeCongeID,
                                               TypeConge = t
                                           }).ToList();
            return listSoldes;
        }
        public SoldeCongeDto GetSoldeCongeByEmployeeIdTypeCongeIdTest(int EmployeeId, int TypeCongeId)
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var result = from d in _db.soldeConge
                             //  join e in _db.Employee on d.EmployeeID equals e.Id
                         join t in _db.typeConge on d.TypeCongeID equals t.Id
                         where d.companyID == currentCompanyId && d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId
                         select new SoldeConge
                         {
                             Id = d.Id,
                             DateModif = d.DateModif,
                             DateCreat = d.DateCreat,
                             UserCreat = d.UserCreat,
                             UserModif = d.UserModif,
                             companyID = d.companyID,
                             Annee = d.Annee,
                             Solde = d.Solde,
                             IdEmployee = d.IdEmployee,
                             //Employee = e,
                             TypeCongeID = d.TypeCongeID,
                             TypeConge = t
                         };
            var soldeConge = result.Last();
            var employe = utOfWork.EmployeeRepository.Get(a => a.Id == soldeConge.IdEmployee);
            var SoldeCongeDto = soldeConge.AsSoldeConge(employe.DateModif, employe.DateCreat, employe.UserCreat, employe.UserModif, employe.companyID, employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.User, employe.RegimeTravailID, employe.ConsultantExterne);

            //var SoldeCongeDto = soldeConge.AsSoldeConge(employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.Gender, employe.MaritalStatus, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.Photo, employe.User, employe.ConsultantExterne);
            //  if  (SoldeCongeDto.ConsultantExterne == false)
            return SoldeCongeDto;

        }

        public float GetTotalSoldeCongeByEmployeeIdTypeCongeIdDate(int EmployeeId, int TypeCongeId)
        {
            float sommesolde;
            sommesolde = utOfWork.SoldeCongeRepository.GetMany(m => m.IdEmployee == EmployeeId && m.TypeCongeID == TypeCongeId).Select(o => o.Solde).Sum();
            return sommesolde;
        }

        public List<SoldeConge> GetSoldeCongeByEmployeeIdTypeCongeId(int EmployeeId, int TypeCongeId, int companyID)
        {
            var a = utOfWork.SoldeCongeRepository.GetMany(d => d.companyID == companyID && d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId).Include(TC => TC.TypeConge).ToList();
            return (a);
        }

        public SoldeConge GetSoldeCongeByEmployeeIdTypeCongeIdDate(int EmployeeId, int TypeCongeId, DateTime date, int companyID)
        {
            SoldeConge sld = GetSoldeCongeByEmployeeIdTypeCongeId(EmployeeId, TypeCongeId, companyID).Where(d => d.Annee == date.Year).FirstOrDefault();
            return sld;
        }

        public SoldeConge Create(SoldeConge SoldeConge)
        {
            try
            {
                utOfWork.SoldeCongeRepository.Add(SoldeConge);
                utOfWork.Commit();
                return SoldeConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SoldeConge Edit(SoldeConge SoldeConge)
        {
            try
            {
                utOfWork.SoldeCongeRepository.Update(SoldeConge);
                utOfWork.Commit();
                return SoldeConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SoldeConge CreateOrUpdateFromMvtConge(MvtConge MvtConge)
        {
            try
            {
                SoldeConge SoldeConge = new SoldeConge();
                SoldeConge SldConge = utOfWork.SoldeCongeRepository.GetMany(m => m.IdEmployee == MvtConge.IdEmployee && m.TypeCongeID == MvtConge.TypeCongeID && m.Annee == MvtConge.Date.Year).FirstOrDefault();

                if (SldConge != null)
                {
                    var v = utOfWork.MvtCongeRepository.GetMany(m => m.Date.Year == MvtConge.Date.Year && m.IdEmployee == MvtConge.IdEmployee && m.TypeCongeID == MvtConge.TypeCongeID).Last();
                    List<SoldeConge> soldeByYear = utOfWork.SoldeCongeRepository.GetMany(m => m.IdEmployee == MvtConge.IdEmployee && m.TypeCongeID == MvtConge.TypeCongeID).OrderBy(d => d.Annee).ToList();
                    using (var scope = new TransactionScope(TransactionScopeOption.Required,
                     new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
                    {
                        try
                        {
                            foreach (var s in soldeByYear)
                            {
                                if (v.Date.Year == s.Annee)
                                {
                                    SldConge.Solde = SldConge.Solde + v.NbreJours;
                                    SoldeConge = Edit(SldConge);
                                }
                            }

                            scope.Complete();
                        }

                        finally
                        {
                            scope.Dispose();
                        }
                    }
                }
                else
                {
                    SoldeConge SdConge = new SoldeConge();
                    SdConge.companyID = MvtConge.companyID;
                    SdConge.IdEmployee = MvtConge.IdEmployee;
                    SdConge.Solde = MvtConge.NbreJours;
                    SdConge.TypeCongeID = MvtConge.TypeCongeID;
                    SdConge.Annee = MvtConge.Date.Year;
                    SoldeConge = Create(SdConge);
                }
                return SoldeConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SoldeConge ValidateCongeFromMvtCongeFromOldSolde(MvtConge MvtConge)
        {
            try
            {
                SoldeConge SoldeConge = new SoldeConge();
                List<SoldeConge> SldConge = utOfWork.SoldeCongeRepository.GetMany(m => m.IdEmployee == MvtConge.IdEmployee && m.TypeCongeID == MvtConge.TypeCongeID).ToList();
                TypeConge typeConge = TypeCongeServices.GetTypeCongetByID(MvtConge.TypeCongeID);
                if ((SldConge.Any()) && (typeConge.CongeAnnuel == true))
                {
                    float NombreJours = MvtConge.NbreJours;
                    for (int i = 0; i < SldConge.Count; i++)
                    {
                        if ((i == SldConge.Count - 1) && (NombreJours != 0.0))
                        {
                            SldConge[i].Solde = SldConge[i].Solde - NombreJours;
                            Edit(SldConge[i]);
                        }
                        else if ((i != SldConge.Count - 1) && (NombreJours != 0.0))
                        {
                            if (SldConge[i].Solde > 0)
                            {
                                if (NombreJours <= SldConge[i].Solde)
                                {
                                    SldConge[i].Solde = SldConge[i].Solde - NombreJours;
                                    NombreJours = 0;
                                    Edit(SldConge[i]);
                                }
                                else
                                {
                                    NombreJours = NombreJours - SldConge[i].Solde;
                                    SldConge[i].Solde = 0;
                                    Edit(SldConge[i]);
                                }
                            }
                        }
                    }
                }
                else if ((SldConge.Count == 0) && (MvtConge.TypeConge.CongeAnnuel == true))
                {
                    SoldeConge SdConge = new SoldeConge();
                    SdConge.companyID = MvtConge.companyID;
                    SdConge.IdEmployee = MvtConge.IdEmployee;
                    SdConge.Solde = MvtConge.NbreJours * (-1);
                    SdConge.TypeCongeID = MvtConge.TypeCongeID;
                    SdConge.Annee = MvtConge.Date.Year;
                    SoldeConge = Create(SdConge);
                }

                return SoldeConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SoldeConge AnnulerCongeFromMvtCongeFromOldSolde(MvtConge MvtConge)
        {
            try
            {
                var _db = Context;
                SoldeConge SoldeConge = new SoldeConge();
                List<SoldeConge> SldConge = utOfWork.SoldeCongeRepository.GetMany(m => m.IdEmployee == MvtConge.IdEmployee && m.TypeCongeID == MvtConge.TypeCongeID).ToList();
                if (SldConge != null)
                {
                    for (int i = 0; i < SldConge.Count; i++)
                    {
                        if (MvtConge.Sens.Equals(RhSens.Droit))
                        {
                            var a = SldConge.Last().Solde;
                            SldConge.Last().Solde = SldConge.Last().Solde + MvtConge.NbreJours;
                            SoldeConge = Edit(SldConge.Last());
                            break;
                        }
                    }
                }
                else
                {
                    SoldeConge SdConge = new SoldeConge();
                    SdConge.companyID = MvtConge.companyID;
                    SdConge.IdEmployee = MvtConge.IdEmployee;
                    SdConge.Solde = MvtConge.NbreJours;
                    SdConge.TypeCongeID = MvtConge.TypeCongeID;
                    SdConge.Annee = MvtConge.Date.Year;
                    SoldeConge = Create(SdConge);
                }
                return SoldeConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<SoldeConge> GetSoldeCongeByEmployeeIdTypeCongeIdAllYear(int EmployeeId, int TypeCongeId, DateTime date)
        {
            var _db = Context;
            List<SoldeConge> listSoldes = (from d in _db.soldeConge
                                               //join e in _db.Employee on d.EmployeeID equals e.Id
                                           join t in _db.typeConge on d.TypeCongeID equals t.Id
                                           where d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId
                                           select new SoldeConge
                                           {
                                               Id = d.Id,
                                               DateModif = d.DateModif,
                                               DateCreat = d.DateCreat,
                                               UserCreat = d.UserCreat,
                                               UserModif = d.UserModif,
                                               companyID = d.companyID,
                                               Annee = d.Annee,
                                               Solde = d.Solde,
                                               IdEmployee = d.IdEmployee,
                                               // Employee = e,
                                               TypeCongeID = d.TypeCongeID,
                                               TypeConge = t
                                           }).ToList();
            return listSoldes;
        }
        public SoldeCongeDto GetSoldeCongeByEmployeeIdTypeCongeIdAllYearTest(int EmployeeId, int TypeCongeId, DateTime date)
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var result = from d in _db.soldeConge
                             //  join e in _db.Employee on d.EmployeeID equals e.Id
                         join t in _db.typeConge on d.TypeCongeID equals t.Id
                         where d.IdEmployee == EmployeeId && d.TypeCongeID == TypeCongeId
                         select new SoldeConge
                         {
                             Id = d.Id,
                             DateModif = d.DateModif,
                             DateCreat = d.DateCreat,
                             UserCreat = d.UserCreat,
                             UserModif = d.UserModif,
                             companyID = d.companyID,
                             Annee = d.Annee,
                             Solde = d.Solde,
                             IdEmployee = d.IdEmployee,
                             //Employee = e,
                             TypeCongeID = d.TypeCongeID,
                             TypeConge = t
                         };
            var soldeConge = result.Last();
            var employe = utOfWork.EmployeeRepository.Get(a => a.Id == soldeConge.IdEmployee);
            var SoldeCongeDto = soldeConge.AsSoldeConge(employe.DateModif, employe.DateCreat, employe.UserCreat, employe.UserModif, employe.companyID, employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.User, employe.RegimeTravailID, employe.ConsultantExterne);

            //var SoldeCongeDto = soldeConge.AsSoldeConge(employe.NumeroPersonne, employe.Nom, employe.Prenom, employe.DateNaissance, employe.CIN, employe.Gender, employe.MaritalStatus, employe.DeliveryDateCin, employe.PlaceCin, employe.PassportNumber, employe.ValidityDateRP, employe.RecruitementDate, employe.TitularizationDate, employe.Tel, employe.TelGSM, employe.Mail, employe.Langue, employe.Adresse, employe.Ville, employe.CodePostal, employe.Photo, employe.User, employe.ConsultantExterne);

            return SoldeCongeDto;
        }
    }

   
}
