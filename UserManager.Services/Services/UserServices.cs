using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class UserServices : IUserServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfileServices profileServices;

        public UserServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public ApplicationUser Edit(ApplicationUser user)
        {
            try
            {
                utOfWork.UserRepository.Update(user);
                utOfWork.Commit();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ApplicationUser GetUserByID(string userId)
        {
            return utOfWork.UserRepository.Get(r => r.Id == userId);
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return utOfWork.UserRepository.GetAll().ToList();
        }

        public List<SelectListItem> GetAllLanguages()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Français", Value = "fr" });
            items.Add(new SelectListItem { Text = "Anglais", Value = "en" });
            return items;
        }

        public ApplicationUser GetUserByUserName()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            string UserName = session[0].Value;
            return utOfWork.UserRepository.Get(r => r.UserName == UserName);
        }

        public ApplicationUser GetUserByUserName(string UserName)
        {
            return utOfWork.UserRepository.Get(r => r.UserName == UserName);
        }

        public bool GetRoleUserByUserName()
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            var currentCompanyId = int.Parse(session[2].Value);
            var UserName = session[0].Value;
            var Role = (from u in _db.Users
                        join ur in _db.UserRoleCompanies on u.Id equals ur.UserId
                        join r in _db.Roles on ur.RoleId equals r.Id
                        where r.Name == "SysAdmin" && u.UserName == UserName && ur.companyId == currentCompanyId
                        select r.Name).Any();
            return Role;
        }

        public string CheckMatriculeEmployee(ApplicationUser user)
        {
            if ((utOfWork.UserRepository.GetMany(d => d.UserName == user.UserName && d.Id != user.Id).Any()) && (utOfWork.UserRepository.GetMany(d => d.Email == user.Email && d.Email != null && d.Id != user.Id).Any()) && user.Email != null && user.Email != "")
            {
                return "EmployeeExitMatriculeMail";
            }
            if (utOfWork.UserRepository.GetMany(d => d.UserName == user.UserName && d.Id != user.Id).Any())
            {
                return "EmployeeExitMatricule";
            }
            if ((utOfWork.UserRepository.GetMany(d => d.Email == user.Email && d.Email != null && d.Id != user.Id).Any()) && user.Email != null && user.Email != "")
            {
                return "EmployeeExitMail";
            }
            else return "";
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return utOfWork.UserRepository.Get(u => u.Email == email);
        }

        public List<UserViewModel> GetFullUsers()
        {
            var listUsers = GetAllUsers().Where(a => a.UserName != "admin").ToList();
            List<UserViewModel> fullUsers = new List<UserViewModel>();
            foreach (var item in listUsers)
            {
                var emp = utOfWork.EmployeeRepository.GetMany(d => d.User == item.UserName).FirstOrDefault();
                var user = new UserViewModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    IsActif = item.IsActif,
                    Language = item.Language,
                    Nom = emp != null ? emp.Nom : null,
                    Prenom = emp != null ? emp.Prenom : null
                };
                fullUsers.Add(user);
            }

            if (fullUsers != null)
            {
                return fullUsers;
            }
            else
                return null;
        }

        public string GetUserName()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            var userName = session[0].Value;
            return userName;
        }

        public int GetNBActifUsers()
        {
            return GetAllUsers().Where(u => u.IsActif == true).Count();
        }

      
    }
}