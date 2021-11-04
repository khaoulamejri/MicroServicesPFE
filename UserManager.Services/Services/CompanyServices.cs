using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class CompanyServices : ICompanyServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CompanyServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;

        }

        public List<Company> GetAllCompany()
        {
            return utOfWork.CompanyRepository.GetAll().ToList();
        }
        public Company GetCompanyByID(int id)
        {
            return utOfWork.CompanyRepository.Get(a => a.Id == id);
        }
        public int GetCurrentCompanyID()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return currentCompanyId;
        }


        public Company GetCompanyByName(string name)
        {
            return utOfWork.CompanyRepository.GetMany(a => a.Name == name).First();
        }

        public Company Create(Company Company)
        {
            try
            {
                utOfWork.CompanyRepository.Add(Company);
                utOfWork.Commit();
                return Company;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Company Edit(Company Company)
        {
            try
            {
                utOfWork.CompanyRepository.Update(Company);
                utOfWork.Commit();
                return Company;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Company Delete(int CompanyId)
        {
            try
            {
                var Company = utOfWork.CompanyRepository.Get(a => a.Id == CompanyId);
                if (Company != null)
                {
                    utOfWork.CompanyRepository.Delete(Company);
                    utOfWork.Commit();
                    return Company;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SelectListItem> GetAllCompanyDropDownList()
        {
            var companiesList = utOfWork.CompanyRepository.GetAll().ToList();
            if (companiesList != null)
            {
                var CompanyList = new List<SelectListItem>();
                Parallel.ForEach(companiesList, item =>
                {
                    CompanyList.Add(new SelectListItem()
                    {
                        Text = item.Display,
                        Value = item.Id.ToString()
                    });
                });
                return CompanyList;
            }
            else
                return null;
        }

        public List<Company> GetCompaniesByUser(string UserId)
        {
            var _db = Context;
            var Companies = (from c in _db.Companies
                             join ur in _db.UserRoleCompanies on c.Id equals ur.companyId
                             where ur.UserId == UserId
                             select c)
                             .ToList();
            return Companies;
        }

        public bool CheckUnicityCompanyByName(string name)
        {
            return !utOfWork.CompanyRepository.GetMany(a => a.Name == name).Any();
        }

        public bool CheckUnicityCompanyByNameID(string name, int ID)
        {
            return !utOfWork.CompanyRepository.GetMany(a => a.Name == name && a.Id != ID).Any();
        }
    }
}