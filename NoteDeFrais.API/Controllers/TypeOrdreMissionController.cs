using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDeFrais.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOrdreMissionController : ControllerBase
    {
        private readonly ITypeOrdreMissionServices _typeOrdreMissionServices;
        private readonly IOrdreMissionServices _ordreMissionServices;

        public TypeOrdreMissionController(ITypeOrdreMissionServices typeOrdreMissionServices, IOrdreMissionServices ordreMissionServices)
        {
            _typeOrdreMissionServices = typeOrdreMissionServices;
            _ordreMissionServices = ordreMissionServices;
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_MissionOrderTypeList)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] TypeOrdreMission typeOrdreMission)
        {
            try
            {
                if (typeOrdreMission == null) throw new Exception("NullMissionOrderType");
                if (_typeOrdreMissionServices.checkUnicity(typeOrdreMission))
                {
                    typeOrdreMission = _typeOrdreMissionServices.Create(typeOrdreMission);

                    if (typeOrdreMission == null)
                    {
                        return StatusCode(400, "FailCreateTypeOrdreMission");
                    }
                }
                else
                {
                    return StatusCode(400, "MissionOrderTypeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, typeOrdreMission);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_MissionOrderTypeList)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] TypeOrdreMission typeOrdreMission)
        {
            try
            {
                if (_typeOrdreMissionServices.checkUnicity(typeOrdreMission))
                {
                    var typeOrdreMissionModified = _typeOrdreMissionServices.GetTypeOrdreMissionById(id);
                    if (typeOrdreMissionModified == null) throw new Exception("NullMissionOrderType");
                    typeOrdreMissionModified.Intitule = typeOrdreMission.Intitule;
                    typeOrdreMissionModified.Code = typeOrdreMission.Code;
                    typeOrdreMissionModified.IsAbroad = typeOrdreMission.IsAbroad;
                    typeOrdreMissionModified = _typeOrdreMissionServices.Edit(typeOrdreMissionModified);
                    return StatusCode(200, typeOrdreMissionModified);
                }
                else
                {
                    return StatusCode(400, "MissionOrderTypeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Delete_MissionOrderTypeList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var typeOrdreMission = _typeOrdreMissionServices.GetTypeOrdreMissionById(id);
                if (typeOrdreMission != null)
                {
                    var relatedMissionOrders = _ordreMissionServices.getMissionOrdersByType(id);
                    if (relatedMissionOrders.Count > 0) { return StatusCode(400, "ImpossibleDeletion"); }
                    else
                    {
                        typeOrdreMission = _typeOrdreMissionServices.Delete(typeOrdreMission);
                    }
                }
                if (typeOrdreMission == null)
                {
                    return StatusCode(400, "ImpossibleDeletion");
                }
                return StatusCode(200, typeOrdreMission);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
 //       [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllMissionOrderType")]
        public IActionResult GetAllMissionOrderType()
        {
            return StatusCode(200, _typeOrdreMissionServices.GetAllTypeOrdreMission());
        }

        [ProducesResponseType(200)]
  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetMissionOrderTypeById")]
        public IActionResult GetMissionOrderTypeById(int id)
        {
            return StatusCode(200, _typeOrdreMissionServices.GetTypeOrdreMissionById(id));
        }
    }
}
