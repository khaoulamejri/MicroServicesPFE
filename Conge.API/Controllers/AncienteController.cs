using Conge.Domain.Entities;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AncienteController : ControllerBase
    {
        private readonly IAncienteServices _ancienteServices;
        public AncienteController(IAncienteServices ancienteServices)
        {
            _ancienteServices = ancienteServices;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
//        [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Read_LeaveRightPlanList)]
        [HttpGet, Route("GetListAnciente")]
        public IActionResult GetListAnciente(int planDroitCongeId)
        {
            var listAnciente = _ancienteServices.GetAncienteByPlanDroitCongeId(planDroitCongeId);
            return StatusCode(200, listAnciente);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightPlanList)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Anciente anciente, int planDroitCongeId)
        {
            try
            {
                Anciente Anciente = new Anciente();
                Anciente.PlanDroitCongeID = planDroitCongeId;
                Anciente.ToAnc = anciente.ToAnc;
                Anciente.JourIncrimente = anciente.JourIncrimente;
                if (_ancienteServices.checkUnicity(Anciente, true))
                {
                    var AncienteToAdd = _ancienteServices.Create(Anciente);
                    if (Anciente != null)
                    {
                        return StatusCode(200, AncienteToAdd);
                    }
                    else
                    {
                        return StatusCode(400, "Not_Found");
                    }
                }
                else
                {
                    return StatusCode(400, "DataVerify");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
 //       [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightPlanList)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Anciente Anciente)
        {
            try
            {
                var ancienteModified = _ancienteServices.GetAncienteByID(id);
                if (ancienteModified != null)
                {
                    if (_ancienteServices.checkUnicity(ancienteModified, false))
                    {
                        ancienteModified.ToAnc = Anciente.ToAnc;
                        ancienteModified.JourIncrimente = Anciente.JourIncrimente;
                        _ancienteServices.Edit(ancienteModified);
                        return StatusCode(200, Anciente);
                    }
                    return StatusCode(400, "DataVerify");
                }
                return StatusCode(400, Anciente);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
     //   [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightPlanList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                string message = _ancienteServices.Delete(id);
                if (message == null)
                {
                    return StatusCode(400, "Problème lors de suppression d'ancienté ");
                }
                return StatusCode(200, message);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}