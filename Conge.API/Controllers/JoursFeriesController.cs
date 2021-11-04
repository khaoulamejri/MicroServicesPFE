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
    public class JoursFeriesController : ControllerBase
    {
        private readonly IJoursFeriesServices _joursFeriesServices;

        public JoursFeriesController(IJoursFeriesServices joursFeriesServices)
        {
            _joursFeriesServices = joursFeriesServices;
        }

  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetJoursFeries")]
        public IActionResult GetJoursFeries()
        {
            var listJoursFeries = _joursFeriesServices.GetAllJoursFeries();
            return StatusCode(200, listJoursFeries);
        }

  //      [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Read_PublicHoliday)]
        [HttpGet, Route("GetJoursFeriesByID")]
        public IActionResult GetJoursFeriesByID(int id)
        {
            var joursFeries = _joursFeriesServices.GetJoursFeriesByID(id);
            return StatusCode(200, joursFeries);
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Add_PublicHoliday)]
        [HttpPost, Route("Post")]
        public IActionResult Post([FromBody] JoursFeries joursFeries)
        {
            JoursFeries jFeries = new JoursFeries();
            try
            {
                if (joursFeries == null) throw new Exception("Failholiday");
                if (_joursFeriesServices.CheckUnicity(joursFeries))
                {
                    jFeries = _joursFeriesServices.Create(joursFeries);
                    if (jFeries == null)
                    {
                        return StatusCode(400, "FailCreationHoliday");
                    }
                }
                else
                {
                    return StatusCode(400, "PublicHolidayExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, jFeries);
        }


    //    [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_PublicHoliday)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int id, [FromBody] JoursFeries joursFeries)
        {
            try
            {
                if (_joursFeriesServices.CheckUnicity(joursFeries, id))
                {
                    var joursFeriesModified = _joursFeriesServices.GetJoursFeriesByID(id);
                    if (joursFeriesModified == null)
                        return NotFound();
                    joursFeriesModified.jour = joursFeries.jour;
                    joursFeriesModified.Description = joursFeries.Description;
                    joursFeriesModified = _joursFeriesServices.Edit(joursFeriesModified);
                    return StatusCode(200, joursFeries);
                }
                else
                {
                    return StatusCode(400, "PublicHolidayExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


      //  [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Delete_PublicHoliday)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Societe = _joursFeriesServices.Delete(id);
                if (Societe == null)
                {
                    return StatusCode(400, "Problème lors du suppression de jour férier ");
                }
                return StatusCode(200, Societe);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}