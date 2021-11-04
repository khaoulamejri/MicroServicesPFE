using Compank.Data;
using Compank.Data.Infrastructure;
using Compank.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compank.Services
{
    public class CompanyServices : ICompanyServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
      


        public CompanyServices(ApplicationDbContext ctx)
        {
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
           

        }
        public bool CheckUnicityCompanyByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool CheckUnicityCompanyByNameID(string name, int ID)
        {
            return !utOfWork.CompanyRepository.GetMany(a => a.Name == name && a.Id != ID).Any();
        }

        public Companyk Create(Companyk Company)
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

        public Companyk Delete(int CompanyId)
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

        public Companyk Edit(Companyk Company)
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

        public List<Companyk> GetAllCompany()
        {
            return utOfWork.CompanyRepository.GetAll().ToList();
        }

            public List<Companyk> GetCompaniesByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public Companyk GetCompanyByID(int id)
        {
            return utOfWork.CompanyRepository.Get(a => a.Id == id);
        }

        public Companyk GetCompanyByName(string name)
        {
            return utOfWork.CompanyRepository.GetMany(a => a.Name == name).First();
        }

        public int GetCurrentCompanyID()
        {
            throw new NotImplementedException();
        }
    }
}
