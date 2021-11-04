using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleCompaniesServices UserRoleCompaniesServices;
        private readonly IUserRolesServices UserRolesServices;
        private readonly ICompanyServices CompanyServices;
        private readonly IUserServices userServices;
        private readonly IEmployeeServices employeeServices;

        public UserRolesController(IUserRoleCompaniesServices userRoleCompaniesServices, IUserRolesServices userRolesServices, ICompanyServices companyServices, IUserServices UserServices, IEmployeeServices EmployeeServices)
        {
            UserRoleCompaniesServices = userRoleCompaniesServices;
            UserRolesServices = userRolesServices;
            CompanyServices = companyServices;
            userServices = UserServices;
            employeeServices = EmployeeServices;
        }

        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Users)]
        [HttpPut, Route("SaveUserRole")]
        public void SaveUserRole(int companyId, string UserId, [FromBody] JArray Role)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    var URC = UserRoleCompaniesServices.GetAllUserRoleCompaniesByUserCompanyID(UserId, companyId);
                    foreach (var x in URC)
                    {
                        UserRoleCompaniesServices.Delete(x);
                    }
                    if (Role != null)
                    {
                        ApplicationUserRoleCompanies UserRole;
                        var records = new List<SelectListItem>();
                        records = JsonConvert.DeserializeObject<List<SelectListItem>>(Role.ToString());
                        foreach (var record in records)
                        {
                            if (record.Selected)
                            {
                                UserRole = new ApplicationUserRoleCompanies
                                {
                                    companyId = companyId,
                                    UserId = UserId,
                                    RoleId = record.Text
                                };
                                UserRoleCompaniesServices.create(UserRole);
                            }
                        }
                        scope.Complete();
                    }
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Users)]
        [HttpPut, Route("SaveAllUserRoles")]
        public void SaveAllUserRoles(string UserId, [FromBody] List<UserRolesViewModel> UserRoles)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    if (UserRoles != null)
                    {
                        var URC = UserRoleCompaniesServices.GetAllUserRoleCompaniesByUserID(UserId);
                        foreach (var x in URC)
                        {
                            UserRoleCompaniesServices.Delete(x);
                        }



                        foreach (var record in UserRoles)
                        {
                            foreach (var role in record.Roles)
                            {
                                if (role.Selected)
                                {
                                    var UserRole = new ApplicationUserRoleCompanies
                                    {
                                        companyId = record.CompanyId,
                                        UserId = UserId,
                                        RoleId = role.Text
                                    };
                                    UserRoleCompaniesServices.create(UserRole);
                                }
                            }
                        }


                        scope.Complete();
                    }
                }
                finally
                {
                    scope.Dispose();
                }
            }
        }

        // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Users)]
        [HttpGet, Route("GetAllRolesByUser")]
        public IActionResult GetAllRolesByUser(string id, int cmpId)
        {
            var rolesList = UserRolesServices.GetRoles();
            var SelectedRoles = new List<SelectListItem>();

            foreach (var item in rolesList)
            {
                var Role = UserRolesServices.GetRoleName(item.Name, id, cmpId);
                if (Role != null)
                    SelectedRoles.Add(new SelectListItem()
                    {
                        Selected = true,
                        Value = item.Name,
                        Text = item.Id
                    });
                else
                    SelectedRoles.Add(new SelectListItem()
                    {
                        Selected = false,
                        Value = item.Name,
                        Text = item.Id
                    });
            }
            return Ok(SelectedRoles);
        }

        // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Users)]
        [HttpGet, Route("GetAllCompaniesRolesByUser")]
        public IActionResult GetAllCompaniesRolesByUser(string id)
        {
            var rolesList = UserRolesServices.GetRoles();
            var companiesList = CompanyServices.GetAllCompany();
            return Ok(UserRolesServices.GetRoleGrouppedByUserId(id, rolesList, companiesList));
        }
    }
}
