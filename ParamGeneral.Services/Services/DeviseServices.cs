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
    public class DeviseServices : IDeviseServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;

        public DeviseServices(ApplicationDbContext context)
        {
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }


        public Devise GetDeviseByID(int id)
        {
            return utOfWork.DeviseRepository.Get(a => a.Id == id);
        }

        public Devise GetDeviseByPays(int id)
        {
            var pays = utOfWork.PaysRepository.GetMany(pp => pp.Id == id).First();
            if (pays != null) return utOfWork.DeviseRepository.GetMany(dd => dd.Id == pays.DeviseID).First();
            else return null;
        }

        public List<Devise> GetAllDevise()
        {
            return utOfWork.DeviseRepository.GetAll().ToList();
        }


        public Devise Create(Devise devise)
        {
            try
            {
                utOfWork.DeviseRepository.Add(devise);
                utOfWork.Commit();
                return devise;
            }
            catch (Exception e)
            {
                return null;
            }
        }
      

      
        public Devise Edit(Devise devise)
        {
            try
            {
                utOfWork.DeviseRepository.Update(devise);
                utOfWork.Commit();
                return devise;

            }
            catch (Exception e)
            {
                //  return null;
                return devise;
            }
        }
        public Devise Delete(int deviseId)
        {
            try
            {
                var devise = GetDeviseByID(deviseId);
                if (devise != null)
                {
                    utOfWork.DeviseRepository.Delete(devise);
                    utOfWork.Commit();
                    return devise;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool checkUnicity(Devise devise)
        {
            return !utOfWork.DeviseRepository.GetMany(de => de.Code == devise.Code && de.Id != devise.Id).Any();
        }
    }
}
