using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Services.IServices;

namespace UserApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IUserRolesServices _roleServices;

        public RolesController(IUserRolesServices rolesServices)
        {
            _roleServices = rolesServices;
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Roles)]
        [HttpGet, Route("GetAllRoles")]
        public List<ApplicationRole> GetAllRoles()
        {
            var rolesList = _roleServices.GetRoles();
            return (rolesList);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Roles)]
        [HttpGet, Route("GetRoleByName")]
        public IActionResult GetRoleByName(string name)
        {
            try
            {
                var role = _roleServices.GetRoleByName(name);
                return StatusCode(200, role);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Roles)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] ApplicationRole role)
        {
            try
            {
                var newRole = _roleServices.GetRoleByName(role.Name);
                if (newRole != null)
                {
                    return StatusCode(400, "Roleexists");
                }
                newRole = _roleServices.AddRole(role);
                return StatusCode(200, role);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Roles)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT([FromBody] ApplicationRole role)
        {
            try
            {
                var roleSameName = _roleServices.GetRoleByName(role.Name);
                if (roleSameName != null && roleSameName.Id != role.Id)
                {
                    return StatusCode(400, "Roleexists");
                }
                var roleModified = _roleServices.UpdateRole(role);
                if (roleModified == null)
                {
                    return StatusCode(400, "NoDataFound");
                }
                return StatusCode(200, roleModified);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Roles)]
        [HttpDelete, Route("DeleteRole")]
        public IActionResult DeleteRole(string Name)
        {
            try
            {
                if (Name == "" || Name == null)
                {
                    return StatusCode(400, "FailDeleteRole");
                }
                var isRoleAssignedToUsers = _roleServices.IsRoleAssigned(Name);
                if (isRoleAssignedToUsers)
                {
                    return StatusCode(400, "RoleAlreadyAssigned");
                }

                var roleModified = _roleServices.DeleteRole(Name);
                if (roleModified == null)
                {
                    return StatusCode(400, "FailDeleteRole");
                }
                return StatusCode(200, roleModified);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}