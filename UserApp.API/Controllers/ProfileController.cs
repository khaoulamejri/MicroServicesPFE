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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileServices ProfileServices;

        public ProfileController(IProfileServices profileServices)
        {
            ProfileServices = profileServices;
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Add_ListProfiles)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Profile profile)
        {
            var profileToAdd = new Profile { Description = profile.Description, Intitule = profile.Intitule };
            var pr = ProfileServices.Create(profileToAdd);
            return StatusCode(200, pr);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_ListProfiles)]
        [HttpGet, Route("GetProfileByID")]
        public IActionResult GetProfileByID(int id)
        {
            var Profile = ProfileServices.GetProfileByID(id);
            return StatusCode(200, Profile);
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_ListProfiles)]
        [HttpGet, Route("GetProfileByIntitule")]
        public IActionResult GetProfileByIntitule(string intitule)
        {
            var Profile = ProfileServices.GetProfileByIntitule(intitule);
            return StatusCode(200, Profile);
        }

    //    [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllProfiles")]
        public IActionResult GetAllProfiles()
        {
            var Profiles = ProfileServices.GetAllProfiles();
            return StatusCode(200, Profiles);
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Edit_ListProfiles)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int id, [FromBody] Profile profile)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model_Invalid");
            }

            if (profile != null)
            {
                try
                {
                    var myPr = ProfileServices.GetProfileByID(id);
                    if (myPr != null)
                    {
                        if (profile.Description != null) { myPr.Description = profile.Description; }
                        if (profile.Intitule != null) { myPr.Intitule = profile.Intitule; }

                        var pr = ProfileServices.Edit(myPr);
                        if (pr == null)
                        {
                            return StatusCode(400, "Problème lors de modification du profile ");
                        }
                        return StatusCode(200, pr);
                    }
                    else
                    {
                        return StatusCode(400, "profile existe");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(400, e.Message);
                }
            }
            return StatusCode(200, profile);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Delete_ListProfiles)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var st = ProfileServices.Delete(id);
                if (st == "no")
                {
                    return StatusCode(400, "FailDeleteProfile");
                }
                return StatusCode(200, st);

            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
