using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
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
    public class SeuilServices : ISeuilServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IUniteService uniteService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SeuilServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<Seuils> GetAllseuils()
        {
            return utOfWork.SeuilRepository.GetAll().ToList();
        }

        public Seuils GetSeuilByID(int id)
        {
            return utOfWork.SeuilRepository.Get(a => a.Id == id);
        }

        public Seuils Create(Seuils Seuil)
        {
            try
            {
                utOfWork.SeuilRepository.Add(Seuil);
                utOfWork.Commit();
                return Seuil;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Seuils Edit(Seuils Seuil)
        {
            try
            {
                utOfWork.SeuilRepository.Update(Seuil);
                utOfWork.Commit();
                return Seuil;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Seuils Delete(int SeuilId)
        {
            try
            {
                Seuils Seuil = GetSeuilByID(SeuilId);
                if (Seuil != null)
                {
                    utOfWork.SeuilRepository.Delete(Seuil);
                    utOfWork.Commit();
                    return Seuil;
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

        public List<SelectListItem> SelectListseuils()
        {
            var AllUniteList = uniteService.GetAllUnite();
            List<SelectListItem> UniteList = new List<SelectListItem>();
            UniteList.Add(new SelectListItem()
            {
                Text = "--- TOUS---",
                Value = "0"
            });
            foreach (var item in AllUniteList)
            {
                UniteList.Add(new SelectListItem()
                {
                    Text = item.Display,
                    Value = item.Id.ToString()
                });
            }

            return UniteList;
        }

        public bool checkUnicity(Seuils Seuil)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            Seuil.companyID = currentCompanyId;
            return !utOfWork.SeuilRepository.GetMany(d => d.Seuil == Seuil.Seuil && d.companyID == Seuil.companyID && d.Id != Seuil.Id).Any();
        }
    }
}
    

