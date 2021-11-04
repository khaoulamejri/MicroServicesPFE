using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
  //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DepartementController : ControllerBase
    {
        private IDepartementServices departementServices;
        private IPositionServices positionservices;

        public DepartementController(IDepartementServices departement, IPositionServices pservices)
        {
            departementServices = departement;
            positionservices = pservices;
        }

       //ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetDepartementById")]
        public IActionResult GetDepartementById(int id)
        {
            var departement = departementServices.GetDepartementByID(id);
            return StatusCode(200, departement);
        }

       //ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetListDepartement")]
        public IActionResult GetListDepartement()
        {
            var listdepartement = departementServices.GetAllDepartements();
            return StatusCode(200, listdepartement);
        }

     // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Add_Department)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Departement Departement)
        {
            var dep = new Departement();
            try
            {
                if (Departement == null)
                {
                    return StatusCode(400, "FailDepartementObject");
                }

                if (departementServices.CheckUnicityDepartement(Departement.Code))
                {
                    dep = departementServices.Create(Departement);
                    if (dep == null)
                    {
                        return StatusCode(400, "FailCreateDepartement");
                    }
                }
                else
                {
                    return StatusCode(400, "CodeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, dep);
        }

      //[ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Department)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Departement Departement)
        {
            var dep = new Departement();
            try
            {
                if (departementServices.CheckUnicityDepartementByID(Departement.Code, id))
                {
                    var departementModified = departementServices.GetDepartementByID(id);
                    departementModified.Intitule = Departement.Intitule;
                    departementModified.Code = Departement.Code;
                    departementServices.Edit(departementModified);
                    return StatusCode(200, departementModified);
                }
                else
                {
                    return StatusCode(400, "CodeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

     // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Delete_Department)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (positionservices.GetPositionByDepartementID(id).Count() >= 1)
                {
                    return StatusCode(400, "AssignedPositionsDepartement");
                }
                else
                {
                    var dep = departementServices.Delete(id);
                    if (dep == null)
                    {
                        return StatusCode(400, "FailDeleteDepartement");
                    }
                    return StatusCode(200, dep);
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
