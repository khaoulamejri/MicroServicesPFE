using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class ComposantService : IComposantService
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly IProfileComposantService profileComposantService;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ComposantService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IProfileComposantService pcomp)
        {
            Context = context;
            _httpContextAccessor = httpContextAccessor;
            profileComposantService = pcomp;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public Composant Create(Composant Composant)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                Composant.companyID = currentCompanyId;
                utOfWork.ComposantRepository.Add(Composant);
                utOfWork.Commit();
                return Composant;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Delete(int ComposantId)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var composant = utOfWork.ComposantRepository.GetMany(c => c.Id == ComposantId).Include(x => x.profileComposants).SingleOrDefault();
                    if (composant != null)
                    {
                        foreach (var item in composant.profileComposants)
                        {
                            profileComposantService.Delete(item.Id, composant.Id);
                        }
                        utOfWork.ComposantRepository.Delete(composant);
                        utOfWork.Commit();
                        scope.Complete();
                        return "ok";
                    }
                    else
                    {
                        return "no";
                    }
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        public Composant Edit(Composant Composant)
        {
            try
            {
                utOfWork.ComposantRepository.Update(Composant);
                utOfWork.Commit();
                return Composant;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Composant> GetAllComposants()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ComposantRepository.GetMany(c => c.companyID == currentCompanyId).ToList();
        }

        public Composant GetComposantByID(int ComposantId)
        {
            return utOfWork.ComposantRepository.Get(a => a.Id == ComposantId);
        }

        public Composant GetComposantByName(string name)
        {
            return utOfWork.ComposantRepository.GetMany(c => c.Name == name).First();
        }

        public bool CheckCombinationExist(string type, string action, string module)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ComposantRepository.GetMany(d => d.Type == type && d.Action == action && d.Module == module && d.companyID == currentCompanyId).Any();
        }

        public bool CheckComponentExist(string name)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ComposantRepository.GetMany(d => d.Name == name && d.companyID == currentCompanyId).Any();
        }

        public bool CheckCombinationExistByID(string type, string action, string module, int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ComposantRepository.GetMany(d => d.Type == type && d.Action == action && d.Module == module && d.companyID == currentCompanyId && d.Id != id).Any();
        }

        public bool CheckComponentExistByID(string name, int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.ComposantRepository.GetMany(d => d.Name == name && d.companyID == currentCompanyId && d.Id != id).Any();
        }
    }
}
