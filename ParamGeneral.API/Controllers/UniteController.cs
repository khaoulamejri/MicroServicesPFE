using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
 // [ApiController]
    public class UniteController : ControllerBase
    {
        private readonly IUniteService uniteService;
        private readonly IPublishEndpoint _publishEndpoint;


        public UniteController(IUniteService uni, IPublishEndpoint publishEndpoint)
        {
            uniteService = uni;
            _publishEndpoint = publishEndpoint;
        }

      //[ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetUnites")]
        public IActionResult GetUnites()
        {
            var listdepartement = uniteService.GetAllUnite();
            return StatusCode(200, listdepartement);
        }

      //[ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Unite)]
        [HttpGet, Route("GetUniteById")]
        public IActionResult GetUniteById(int id)
        {
            var unite = uniteService.GetUniteByID(id);
            return StatusCode(200, unite);
        }

     // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Add_Unite)]
        [HttpPost, Route("POST")]
        public async Task<IActionResult> POSTAsync([FromBody] Unite Unite)
        {
            try
            {
                if (Unite == null)
                {
                    return StatusCode(400, "FailEmptyUnitie");
                }

                if (uniteService.CheckUnicityUnite(Unite.Code))
                {
                    var unt = uniteService.Create(Unite);
                     await _publishEndpoint.Publish(new UniteConsumed(Unite.Id, Unite.UserCreat, Unite.UserModif, Unite.DateCreat, Unite.DateModif, Unite.companyID, Unite.Code, Unite.Intitule));

                    if (unt == null)
                    {
                        return StatusCode(400, "FailCreateUnite");
                    }
                    return StatusCode(200, unt);
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

    //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Unite)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Unite Unite)
        {
            try
            {
                if (uniteService.CheckUnicityUniteByID(Unite.Code, id))
                {
                    var UniteModified = uniteService.GetUniteByID(id);
                    UniteModified.Intitule = Unite.Intitule;
                    UniteModified.Code = Unite.Code;
                    uniteService.Edit(UniteModified);
                    return StatusCode(200, UniteModified);
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

     // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Delete_Unite)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var unt = uniteService.Delete(id);
                if (unt == null)
                {
                    return StatusCode(400, "CodeExist");
                }
                return StatusCode(200, unt);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
