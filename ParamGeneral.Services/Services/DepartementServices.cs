using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParamGeneral.Data;
using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Services
{
    public class DepartementServices : IDepartementServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartementServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<Departement> GetAllDepartements()
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //return utOfWork.DepartementRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
              return utOfWork.DepartementRepository.GetAll().ToList();
        }

        public Departement GetDepartementByID(int id)
        {
            return utOfWork.DepartementRepository.Get(a => a.Id == id);
        }

        public Departement Create(Departement Departement)
        {
            try
            {
                //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                //int currentCompanyId = int.Parse(session[2].Value);
                //Departement.companyID = currentCompanyId;

                utOfWork.DepartementRepository.Add(Departement);
                utOfWork.Commit();
                return Departement;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Departement Edit(Departement Departement)
        {
            try
            {
                utOfWork.DepartementRepository.Update(Departement);
                utOfWork.Commit();
                return Departement;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Departement Delete(int DepartementId)
        {
            try
            {
                var Departement = GetDepartementByID(DepartementId);
                if (Departement != null)
                {
                    utOfWork.DepartementRepository.Delete(Departement);
                    utOfWork.Commit();
                    return Departement;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //public List<SelectListItem> SelectListItemDepartements()
        //{
        //    var rolesList = GetAllDepartements();
        //    if (rolesList != null)
        //    {
        //        var UserList = new List<SelectListItem>();
        //        UserList.Add(new SelectListItem()
        //        {
        //            Text = "--- TOUS---",
        //            Value = "0"
        //        });
        //        Parallel.ForEach(rolesList, item =>
        //        {
        //            UserList.Add(new SelectListItem()
        //            {
        //                Text = item.Display,
        //                Value = item.Id.ToString()
        //            });
        //        });
        //        return UserList;
        //    }
        //    else
        //        return null;
        //}

        public bool CheckUnicityDepartement(string code)
        {

            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //return !utOfWork.DepartementRepository.GetMany(d => d.Code == code && d.companyID == currentCompanyId).Any();
               return !utOfWork.DepartementRepository.GetMany(d => d.Code == code).Any();

        }

        public bool CheckUnicityDepartementByID(string code, int ID)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return !utOfWork.DepartementRepository.GetMany(d => d.Code == code && d.companyID == currentCompanyId && d.Id != ID).Any();
            // return !utOfWork.DepartementRepository.GetMany(d => d.Code == code && d.Id != ID).Any();

        }

        public List<SelectListItem> SelectListItemDepartements()
        {
            throw new NotImplementedException();
        }
    }
}