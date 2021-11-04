using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.ViewsModels;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
  public  class TypeCongeServices : ITypeCongeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TypeCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<TypeConge> GetAllTypeConges()
        {
           var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
          //  var companys = utOfWork.CompanyRepository.GetAll();
           // var comp = companys.Single(comp => comp.Id == demandeConge.IDCompanyConsumed);
         //  var comp = companys.Single(comp => comp.Id == )
            int currentCompanyId = int.Parse(session[2].Value);
          
            //return utOfWork.TypeCongeRepository.GetMany(d => d.Company.Id == currentCompanyId).ToList();
            // return utOfWork.TypeCongeRepository.GetAll().ToList();
            return utOfWork.TypeCongeRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

        public TypeConge GetTypeCongetByID(int id)
        {
            return utOfWork.TypeCongeRepository.Get(a => a.Id == id);
        }

        public TypeConge Create(TypeConge TypeConge)
        {
            try
            {
                utOfWork.TypeCongeRepository.Add(TypeConge);
                utOfWork.Commit();
                return TypeConge;
            }
            catch
            {
                return null;
            }
        }

        public TypeConge Edit(TypeConge TypeConge)
        {
            try
            {
                utOfWork.TypeCongeRepository.Update(TypeConge);
                utOfWork.Commit();
                return TypeConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TypeConge Delete(int TypeCongeId)
        {

            try
            {

              
                TypeConge TypeConge = GetTypeCongetByID(TypeCongeId);
                if (TypeConge != null)
                {
                    utOfWork.TypeCongeRepository.Delete(TypeConge);
                    utOfWork.Commit();
                    return TypeConge;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TypeConge GetTypeCongetAnnuel()
        {
            return GetAllTypeConges().FirstOrDefault(d => d.CongeAnnuel == true);
        }

        public bool CheckSameTypeConge(TypeCongeViewModel TypeConge)
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //TypeConge.companyID = currentCompanyId;
            // return !utOfWork.TypeCongeRepository.GetMany(d => (d.Code == TypeConge.Code || d.Intitule == TypeConge.Intitule) && d.CompanyID == TypeConge.CompanyID && d.Id != TypeConge.Id).Any();
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            //TypeConge.CompanyID = currentCompanyId;
            return !utOfWork.TypeCongeRepository.GetMany(d => (d.Code == TypeConge.Code || d.Intitule == TypeConge.Intitule) && d.companyID == currentCompanyId && d.Id != TypeConge.Id).Any();
        }

        public bool CheckSameAnnualLeave(TypeCongeViewModel TypeConge)
        {
            return utOfWork.TypeCongeRepository.GetMany(d => d.Id != TypeConge.Id && d.CongeAnnuel == true && d.companyID == TypeConge.companyID).Any();
        }

        public List<SelectListItem> SelectListItemTypeConge()
        {
            var rolesList = GetAllTypeConges();
            List<SelectListItem> UserList = new List<SelectListItem>();
            UserList.Add(new SelectListItem()
            {
                Text = "",
                Value = "",
            });
            foreach (var item in rolesList)
            {
                UserList.Add(new SelectListItem()
                {
                    Text = item.Display,
                    Value = item.Id.ToString()
                });
            }
            return UserList;
        }
    }
}
