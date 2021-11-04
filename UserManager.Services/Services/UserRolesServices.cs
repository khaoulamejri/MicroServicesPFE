using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApp.Data;
using UserApp.Data.Infrastructure;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.Services.Services
{
    public class UserRolesServices : IUserRolesServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public UserRolesServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;


        }

        public List<ApplicationRole> GetRoles()
        {
            return utOfWork.UserRoleRepository.GetAll().ToList();
        }

        public ApplicationRole GetRoleById(string id)
        {
            return utOfWork.UserRoleRepository.Get(a => a.Id == id);
            // return utOfWork.UserRoleRepository.GetMany(r => r.Id.Equals(id, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public ApplicationRole GetRoleByName(string name)
        {
            //  return utOfWork.UserRoleRepository.GetMany(r => r.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return utOfWork.UserRoleRepository.Get(a => a.Name == name);
        }

            public ApplicationRole AddRole(ApplicationRole role)
        {
            try
            {
                utOfWork.UserRoleRepository.Add(role);
                utOfWork.Commit();
                return role;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ApplicationRole UpdateRole(ApplicationRole role)
        {
            try
            {
                var roleModified = GetRoleById(role.Id);
                if (roleModified != null)
                    roleModified.Name = role.Name;
                roleModified.Description = role.Description;
                utOfWork.UserRoleRepository.Update(roleModified);
                utOfWork.Commit();
                return roleModified;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ApplicationRole DeleteRole(string name)
        {
            try
            {
                var thisRole = utOfWork.UserRoleRepository.Get(a => a.Name == name);
                if (thisRole != null)
                {
                    utOfWork.UserRoleRepository.Delete(thisRole);
                    utOfWork.Commit();
                    return thisRole;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool IsRoleAssigned(string name)
        {
            try
            {
                var role = utOfWork.UserRoleRepository.Get(a => a.Name == name);
                if (role == null)
                    return false;
                var userRoles = utOfWork.UserRoleCompaniesRepository.GetMany(ur => ur.RoleId == role.Id);
                return (userRoles.Any());
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool IsSysAdmin()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            var roles = session.Where(c => c.Type == ClaimTypes.Role).ToList();
            bool SysAdmin = false;
            foreach (var role in roles)
            {
                if (role.Value.Contains("SysAdmin"))
                {
                    var rolesNumber = GetRolesNumber("SysAdmin");
                    if (rolesNumber > 0)
                    {
                        SysAdmin = true;
                        break;
                    }
                }
            }
            return SysAdmin;
        }

        public int GetRolesNumber(string name)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            var UserName = session[0].Value;
            var currentCompanyId = int.Parse(session[2].Value);
            var rolesNumber = (from u in Context.Users
                               join ur in Context.UserRoleCompanies on u.Id equals ur.UserId
                               join r in Context.Roles on ur.RoleId equals r.Id
                               where r.Name == name && u.UserName == UserName && ur.companyId == currentCompanyId
                               select r.Name).Count();
            return rolesNumber;
        }

        public string GetRoleName(string name, string id, int cmpId)
        {
            var _db = Context;
            var Role = (from u in _db.Users
                        join ur in _db.UserRoleCompanies on u.Id equals ur.UserId
                        join r in _db.Roles on ur.RoleId equals r.Id
                        where r.Name == name && u.Id == id && ur.companyId == cmpId
                        select r.Name).FirstOrDefault();
            return Role;
        }

        public List<Object> GetRoleGrouppedByUserId(string id, List<ApplicationRole> rolesList, List<Company> companiesList)
        {
            var _db = Context;
            var GrouppedRoles = (from u in _db.Users.Where(user => user.Id == id)
                                 from ur in _db.UserRoleCompanies.Where(userroleCompany => u.Id == userroleCompany.UserId)
                                 from r in _db.Roles.Where(role => ur.RoleId == role.Id)
                                 from c in _db.Companies.Where(company => ur.companyId == company.Id)
                                 select new
                                 {
                                     CompanyId = ur.companyId,
                                     CompanyName = c.Name,
                                     Role = new SelectListItem()
                                     {
                                         Selected = true,
                                         Value = r.Name,
                                         Text = r.Id
                                     }
                                 }).GroupBy(item => new { item.CompanyId, item.CompanyName });
            var Roles = new List<Object>();
            foreach (var company in companiesList)
            {
                var foundgroup = GrouppedRoles.SingleOrDefault(c => c.Key.CompanyId == company.Id);
                var companyRoles = new List<SelectListItem>();
                if (foundgroup != null)
                {
                    foreach (var role in rolesList)
                    {
                        var foundRole = foundgroup.SingleOrDefault(i => i.Role.Text == role.Id);
                        if (foundRole != null)
                            companyRoles.Add(foundRole.Role);
                        else
                            companyRoles.Add(new SelectListItem
                            {
                                Selected = false,
                                Value = role.Name,
                                Text = role.Id
                            });
                    }
                }
                else
                {
                    foreach (var role in rolesList)
                    {
                        companyRoles.Add(new SelectListItem
                        {
                            Selected = false,
                            Value = role.Name,
                            Text = role.Id
                        });
                    }
                }
                Roles.Add(new
                {
                    CompanyId = company.Id,
                    CompanyName = company.Name,
                    Roles = companyRoles
                });
            }
            return Roles;
        }

        public List<string> getListRolesNames(string id, int cmpId)
        {
            var roles = (from ur in Context.UserRoleCompanies
                         join r in Context.Roles on ur.RoleId equals r.Id
                         where ur.UserId == id && ur.companyId == cmpId
                         select r.Name).ToList();
            return roles;
        }

        public int GetRoleNameLength(string name, int cmpId, string UserName)
        {
            var _db = Context;
            var Role = (from u in _db.Users
                        join ur in _db.UserRoleCompanies on u.Id equals ur.UserId
                        join r in _db.Roles on ur.RoleId equals r.Id
                        where r.Name == name && u.UserName == UserName && ur.companyId == cmpId
                        select r.Name).FirstOrDefault().Count();
            return Role;
        }
    }
}