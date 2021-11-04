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
    public class UserRoleCompaniesServices : IUserRoleCompaniesServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;

        public UserRoleCompaniesServices(ApplicationDbContext ctx)
        {
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public bool CheckUnicityUserRoleCompanies(int companyID, string UserID, string roleID)
        {
            return !utOfWork.UserRoleCompaniesRepository.GetMany(x => x.companyId == companyID && x.RoleId == roleID && x.UserId == UserID).Any();
        }

        public ApplicationUserRoleCompanies create(ApplicationUserRoleCompanies UserRoleCompanies)
        {
            try
            {
                utOfWork.UserRoleCompaniesRepository.Add(UserRoleCompanies);
                utOfWork.Commit();
                return UserRoleCompanies;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ApplicationUserRoleCompanies> GetAllUserRoleCompaniesByUserCompanyID(string userID, int companyID)
        {
            return utOfWork.UserRoleCompaniesRepository.GetMany(x => x.UserId == userID && x.companyId == companyID).ToList();
        }

        public List<ApplicationUserRoleCompanies> GetAllUserRoleCompaniesByUserID(string userID)
        {
            return utOfWork.UserRoleCompaniesRepository.GetMany(x => x.UserId == userID).ToList();
        }

        public ApplicationUserRoleCompanies Delete(ApplicationUserRoleCompanies UserRoleCompanies)
        {
            try
            {
                if (UserRoleCompanies != null)
                {
                    utOfWork.UserRoleCompaniesRepository.Delete(UserRoleCompanies);
                    utOfWork.Commit();
                    return UserRoleCompanies;
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
    }
}
