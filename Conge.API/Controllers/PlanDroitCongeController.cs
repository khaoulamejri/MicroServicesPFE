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
    public class PlanDroitCongeController : ControllerBase
    {
        private readonly IPlanDroitCongeServices _planDroitCongeServices;

        public PlanDroitCongeController(IPlanDroitCongeServices planDroitCongeServices)
        {
            _planDroitCongeServices = planDroitCongeServices;
        }

 //       [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllPlanDroitConge")]
        public IActionResult GetAllPlanDroitConge()
        {
            var listTypeConge = _planDroitCongeServices.GetAllPlanDroitConge();
            return StatusCode(200, listTypeConge);
        }

   //     [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetPlanDroitCongeByID")]
        public IActionResult GetPlanDroitCongeByID(int id)
        {
            var listTypeConge = _planDroitCongeServices.GetPlanDroitCongeByID(id);
            return StatusCode(200, listTypeConge);
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Add_LeaveRightPlanList)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] PlanDroitConge planDroitConge)
        {
            PlanDroitConge dep = new PlanDroitConge();

            try
            {
                if (planDroitConge == null) throw new Exception("PlanDroitConge vide !!");
                if (_planDroitCongeServices.checkUnicity(planDroitConge))
                {
                    dep = _planDroitCongeServices.Create(planDroitConge);
                    if (dep == null)
                    {
                        return StatusCode(400, "FailCreateplan");
                    }
                }
                else
                {
                    return StatusCode(400, "PlanDroitCongeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, dep);
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightPlanList)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] PlanDroitConge planDroitConge)
        {
            try
            {
                if (_planDroitCongeServices.checkUnicity(planDroitConge))
                {
                    var planDroitCongeModified = _planDroitCongeServices.GetPlanDroitCongeById(id);
                    if (planDroitCongeModified == null)
                        return NotFound();
                    planDroitCongeModified.Code = planDroitConge.Code;
                    planDroitCongeModified.Intitule = planDroitConge.Intitule;
                    planDroitCongeModified = _planDroitCongeServices.Edit(planDroitCongeModified);
                    return StatusCode(200, planDroitConge);
                }
                else
                {
                    return StatusCode(400, "PlanDroitCongeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

//        [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Delete_LeaveRightPlanList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id, [FromBody] PlanDroitConge planDroitConge)
        {
            try
            {
                var dep = _planDroitCongeServices.Delete(id);
                if (dep == null)
                {
                    return StatusCode(400, "FailDeletePlanDroitConge");
                }
                return StatusCode(200, dep);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}

