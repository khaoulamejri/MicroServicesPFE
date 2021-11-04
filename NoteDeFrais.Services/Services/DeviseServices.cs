using NoteDeFrais.Data;
using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.Services
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
                return null;
            }
        }
        public Devise GetDeviseByID(int id)
        {
            return utOfWork.DeviseRepository.Get(a => a.Id == id);
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
    }
}
