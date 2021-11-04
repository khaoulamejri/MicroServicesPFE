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
    public class RolesPrivilegesServices : IRolesPrivilegesServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;

        public RolesPrivilegesServices(ApplicationDbContext ctx)
        {
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public RolesPrivileges create(RolesPrivileges RolesPrivileges)
        {
            try
            {
                utOfWork.RolesPrivilegesRepository.Add(RolesPrivileges);
                utOfWork.Commit();
                return RolesPrivileges;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetRolesPrivilegesByRoleId(string roleID, string privilege)
        {
            var _db = Context;
            return (from p in _db.rolesPrivileges
                    where p.IdRole == roleID && p.Privilege == privilege
                    select p.Privilege).FirstOrDefault();
        }

        public RolesPrivileges GetRolesPrivilegesByRoleIdPrivilege(string roleID, string privilege)
        {
            return utOfWork.RolesPrivilegesRepository.GetMany(a => a.IdRole == roleID && a.Privilege == privilege).FirstOrDefault();
        }

        public RolesPrivileges Delete(RolesPrivileges RolesPrivileges)
        {
            try
            {
                if (RolesPrivileges != null)
                {
                    utOfWork.RolesPrivilegesRepository.Delete(RolesPrivileges);
                    utOfWork.Commit();
                    return RolesPrivileges;
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

        public List<RolesPrivileges> GetAllRolesPrivilegesByRoleId(string roleID)
        {
            return utOfWork.RolesPrivilegesRepository.GetMany(a => a.IdRole == roleID).ToList();
        }
    }
}
