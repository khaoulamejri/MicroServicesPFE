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
    public class ProfileComposantController : ControllerBase
    {
        private readonly IProfileComposantService profileComposantService;

        public ProfileComposantController(IProfileComposantService profileComposantService)
        {
            this.profileComposantService = profileComposantService;
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_ProfileComposant)]
        [HttpGet, Route("GetProfileComposantById")]
        public IActionResult GetProfileComposantById(int profileId, int composantId)
        {
            var profileComposant = profileComposantService.GetProfileComposantById(profileId, composantId);
            return StatusCode(200, profileComposant);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_ProfileComposant)]
        [HttpGet, Route("GetProfileComposantsByComposantId")]
        public IActionResult GetProfileComposantsByComposantId(int composantId)
        {
            var profileComposant = profileComposantService.GetProfileComposantsByComposantId(composantId);
            return StatusCode(200, profileComposant);
        }

      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetProfileComposantsByprofileId")]
        public IActionResult GetProfileComposantByprofileId(int profileId)
        {
            var profileComposant = profileComposantService.GetProfileComposantsByprofileId(profileId);
            return StatusCode(200, profileComposant);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_ProfileComposant)]
        [HttpGet, Route("GetAllProfileComposants")]
        public IActionResult GetAllProfileComposants()
        {
            var profileComposantsList = profileComposantService.GetAllProfileComposants();
            return StatusCode(200, profileComposantsList);
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Add_ProfileComposant)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] ProfileComposant profileComposant)
        {
            try
            {
                var pc = profileComposantService.Create(profileComposant);
                if (pc == null) return StatusCode(400, "ProfileComposantExist");
                return StatusCode(200, pc);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Delete_ProfileComposant)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int profileId, int composantId)
        {
            try
            {
                var st = profileComposantService.Delete(profileId, composantId);
                if (st == "no")
                {
                    return StatusCode(400, "FailDeleteProfileComposant");
                }
                return StatusCode(200, st);

            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        //[ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Edit_ProfileComposant)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int oldProfileId, int oldComposantId, [FromBody] ProfileComposant newProfileComposant)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model_Invalid");
            }
            var pc = new ProfileComposant();
            try
            {
                var st = profileComposantService.Delete(oldProfileId, oldComposantId);
                if (st == "no")
                {
                    return StatusCode(400, "FailDeleteProfileComposant");
                }
                pc = profileComposantService.Create(newProfileComposant);
                if (pc == null) return StatusCode(400, "TypeOfExpenseExist");
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, pc);
        }
    }
}
