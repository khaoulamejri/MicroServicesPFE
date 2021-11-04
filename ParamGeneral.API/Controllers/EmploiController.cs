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
  ///  [ApiController]
    public class EmploiController : ControllerBase
    {
        private readonly IEmploiServices _emploiServices;
        private readonly IPositionServices _positionServices;
        

        public EmploiController(IEmploiServices emploiServices, IPositionServices positionServices )
        {
            _emploiServices = emploiServices;
            _positionServices = positionServices;
           
        }

     //   [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetEmplois")]
        public IActionResult GetEmplois(int? CompanyId = 0)
        {
            var ListEmplois = new List<Emploi>();
            if (CompanyId > 0)
            {
                ListEmplois = _emploiServices.GetAllEmploiByCompany(CompanyId.Value);
            }
            else
            {
                ListEmplois = _emploiServices.GetAllEmploi();
            }
            return StatusCode(200, ListEmplois);
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Emploi)]
        [HttpGet, Route("GetEmploiById")]
        public IActionResult GetEmploiById(int id)
        {
            var emploi = _emploiServices.GetEmploiById(id);
            return StatusCode(200, emploi);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Add_Emploi)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Emploi Emploi)
        {
            var emploi = new Emploi();
            try
            {
                var existants = _emploiServices.GetAllEmploiByKey(Emploi.Reference);
                if (existants.Count() > 0)
                    return StatusCode(400, "EmploiExist");
                emploi = _emploiServices.Create(Emploi);
                if (emploi == null)
                    return StatusCode(400, "VeifyData");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, emploi);
        }
        

        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Emploi)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Emploi Emploi)
        {
            try
            {
                if (Emploi == null) throw new Exception("FailEmploiObject");
                if (_emploiServices.checkUnicity(Emploi, false))
                {
                    var emploiModified = _emploiServices.GetEmploiById(id);
                    if (emploiModified == null) throw new Exception("FailEmploiObject");
                    emploiModified.Intitule = Emploi.Intitule;
                    emploiModified.Description = Emploi.Description;
                    emploiModified.Reference = Emploi.Reference;
                    emploiModified = _emploiServices.Edit(emploiModified);
                    return StatusCode(200, emploiModified);
                }
                else
                {
                    return StatusCode(400, "EmploiExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.Settings_Delete_Emploi)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_positionServices.CheckAssignedPositionsJob(id))
                {
                    return StatusCode(400, "AssignedPositionsJob");
                }
                else
                {
                    var emploi = _emploiServices.Delete(id);
                    if (emploi == null)
                        return StatusCode(400, "FailDeleteEmploi");
                    return StatusCode(200, emploi);
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}