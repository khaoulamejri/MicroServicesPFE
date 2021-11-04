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
    public class RegimeTravailServices : IRegimeTravailServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegimeTravailServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<RegimeTravail> GetAllRegimeTravail()
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //return utOfWork.RegimeTravailRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
            return utOfWork.RegimeTravailRepository.GetAll().ToList();

        }

        public RegimeTravail Create(RegimeTravail regimeTravail)
        {
            try
            {
                //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                //int currentCompanyId = int.Parse(session[2].Value);
                //regimeTravail.companyID = currentCompanyId;

                utOfWork.RegimeTravailRepository.Add(regimeTravail);
                utOfWork.Commit();
                return regimeTravail;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public RegimeTravail GetRegimeTravailByID(int id)
        {
            return utOfWork.RegimeTravailRepository.Get(a => a.Id == id);
        }
        public RegimeTravail GetRegimTravailByID(int? id)
        {
            return utOfWork.RegimeTravailRepository.Get(a => a.Id == id);
        }

        public RegimeTravail Edit(RegimeTravail regimeTravail)
        {
            try
            {
                utOfWork.RegimeTravailRepository.Update(regimeTravail);
                utOfWork.Commit();
                return regimeTravail;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public RegimeTravail Delete(int regimeTravailId)
        {
            try
            {
                var regimeTravail = GetRegimeTravailByID(regimeTravailId);
                if (regimeTravail != null)
                {
                    utOfWork.RegimeTravailRepository.Delete(regimeTravail);
                    utOfWork.Commit();
                    return regimeTravail;
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

        public List<SelectListItem> SelectListItemRegimeTravail()
        {
            var regimeList = GetAllRegimeTravail();
            if (regimeList != null)
            {
                var RegimeTravailList = new List<SelectListItem>();
                Parallel.ForEach(regimeList, item =>
                {
                    RegimeTravailList.Add(new SelectListItem()
                    {
                        Text = item.Display,
                        Value = item.Id.ToString()
                    });
                });
                return RegimeTravailList;
            }
            else
                return null;
        }

        public bool CheckRegimeTravailExist(string intitule)
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
 //   return utOfWork.RegimeTravailRepository.GetMany(a => a.Intitule == intitule && a.companyID == currentCompanyId).Any();

            return utOfWork.RegimeTravailRepository.GetMany(a => a.Intitule == intitule).Any();
        }

        public bool CheckRegimeTravailUnicity(string code)
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
         // return !utOfWork.RegimeTravailRepository.GetMany(a => a.Code == code && a.companyID == currentCompanyId).Any();
            return !utOfWork.RegimeTravailRepository.GetMany(a => a.Code == code ).Any();


        }
    }
}