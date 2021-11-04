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
   // [ApiController]
    public class RegimeTravailController : ControllerBase
    {
        private readonly IRegimeTravailServices regimeTravailServices;

        public RegimeTravailController(IRegimeTravailServices reg)
        {
            regimeTravailServices = reg;
        }

      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllRegimeTravail")]
        public IActionResult GetAllRegimeTravail()
        {
            var allRegimeTravail = regimeTravailServices.GetAllRegimeTravail();
            return StatusCode(200, allRegimeTravail);
        }

      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetRegimeTravailByID")]
        public IActionResult GetRegimeTravailByID(int id)
        {
            var regimeTravail = regimeTravailServices.GetRegimeTravailByID(id);
            return StatusCode(200, regimeTravail);
        }
        [HttpGet, Route("GetRegimeTravailByIDP/{id}")]
        public IActionResult GetRegimeTravailByIDP(int id)
        {
            var regimeTravail = regimeTravailServices.GetRegimeTravailByID(id);
            return StatusCode(200, regimeTravail);
        }

        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Add_WorkRegimes)]
        [HttpPost, Route("Poste")]
        public IActionResult Poste([FromBody] RegimeTravail regimeTravail)
        {
            try
            {
                if (regimeTravail == null)
                {
                    return StatusCode(400, "FailEmptyRegime");
                }
                if (regimeTravailServices.CheckRegimeTravailExist(regimeTravail.Intitule))
                {
                    return StatusCode(400, "RegimeTravailExist");
                }
                if (regimeTravailServices.CheckRegimeTravailUnicity(regimeTravail.Code))
                {
                    var regTrav = regimeTravailServices.Create(regimeTravail);
                    if (regTrav == null)
                    {
                        return StatusCode(400, "FailCreatRegime");
                    }
                    return StatusCode(200, regTrav);
                }
                else
                {
                    return StatusCode(400, "RegimeTravailExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        //[ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_WorkRegimes)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int id, [FromBody] RegimeTravail regimeTravail)
        {
            var Regimetravail = new RegimeTravail();
            try
            {
                var RegimeTravailModified = regimeTravailServices.GetRegimeTravailByID(id);
                RegimeTravailModified.Code = regimeTravail.Code;
                RegimeTravailModified.Intitule = regimeTravail.Intitule;
                RegimeTravailModified.ParamWeek = regimeTravail.ParamWeek;
                regimeTravailServices.Edit(RegimeTravailModified);
                return StatusCode(200, RegimeTravailModified);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Delete_WorkRegimes)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var regTrav = regimeTravailServices.Delete(id);
                if (regTrav == null)
                {
                    return StatusCode(400, "FailDeleteRegimeTravail");
                }
                return StatusCode(200, regTrav);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
                throw new Exception();
            }
        }
    }
}