using Microsoft.AspNetCore.Http;
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
    public class PaysServices : IPaysServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaysServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<Pays> GetAllPays()
        {
            return utOfWork.PaysRepository.GetAll().ToList();
        }
        public Pays GetPaysByID(int id)
        {
            return utOfWork.PaysRepository.Get(a => a.Id == id);
        }

        public Pays Create(Pays Pays)
        {
            try
            {
               
                utOfWork.PaysRepository.Add(Pays);
                utOfWork.Commit();
                return Pays;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Pays Edit(Pays Pays)
        {
            try
            {
                utOfWork.PaysRepository.Update(Pays);
                utOfWork.Commit();
                return Pays;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Pays Delete(int PaysId)
        {
            try
            {
                var Pays = GetPaysByID(PaysId);
                if (Pays != null)
                {
                    utOfWork.PaysRepository.Delete(Pays);
                    utOfWork.Commit();
                    return Pays;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CheckUnicityPays(string code)
        {
            return !utOfWork.PaysRepository.GetMany(a => a.Code == code).Any();
        }

        public bool CheckUnicityPaysByID(string code, int id)
        {
            return !utOfWork.PaysRepository.GetMany(a => a.Code == code && a.Id != id).Any();
        }
    }
}
