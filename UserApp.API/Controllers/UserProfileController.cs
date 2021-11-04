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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_UserProfile)]
        [HttpGet, Route("GetUserProfileById")]
        public IActionResult GetUserProfileById(int id)
        {
            var userProfile = userProfileService.GetUserProfileById(id);
            return StatusCode(200, userProfile);
        }

       // [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetUserProfileByUserName")]
        public IActionResult GetUserProfileByUserName(string username)
        {
            var userProfile = userProfileService.GetUserProfilesByUsername(username);
            return StatusCode(200, userProfile);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_UserProfile)]
        [HttpGet, Route("GetAllUserProfiles")]
        public IActionResult GetAllUserProfiles()
        {
            var UserProfilesList = userProfileService.GetAllUserProfiles();
            return StatusCode(200, UserProfilesList);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Read_UserProfile)]
        [HttpGet, Route("GetAllUsersByProfile")]
        public IActionResult GetAllUsersByProfile(int ProfileId)
        {
            var UserProfilesList = userProfileService.GetAllUsersByProfileId(ProfileId);
            return StatusCode(200, UserProfilesList);
        }
      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Add_UserProfile)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] UserProfile userProfile)
        {
            try
            {
                var up = userProfileService.Create(userProfile);
                if (up == null) return StatusCode(400, "userProfileExist");
                return StatusCode(200, up);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Delete_UserProfile)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int ProfileId, string UserName)
        {
            try
            {
                var st = userProfileService.Delete(ProfileId, UserName);
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

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_KPI_Edit_UserProfile)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int id, [FromBody] UserProfile userProfile)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(400, "Model_Invalid");
            }
            if (userProfile != null)
            {
                try
                {
                    var myPr = userProfileService.GetUserProfileById(id);
                    if (myPr != null)
                    {
                        if (userProfile.ProfileId != null) { myPr.ProfileId = userProfile.ProfileId; }
                        if (userProfile.UserName != null) { myPr.UserName = userProfile.UserName; }

                        var pr = userProfileService.Edit(myPr);
                        if (pr == null)
                        {
                            return StatusCode(400, "Problème lors de modification du UserProfile ");
                        }
                        return StatusCode(200, pr);
                    }
                    else
                    {
                        return StatusCode(400, "UserProfile existe");
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(400, e.Message);
                }
            }
            return StatusCode(200, userProfile);
        }
    }
}
