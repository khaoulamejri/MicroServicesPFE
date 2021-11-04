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
    public class GroupeDepenseController : ControllerBase
    {
        private readonly IGroupeDepenseServices _groupeDepenseServices;
        private readonly IGroupeFraisServices _groupeFraisServices;
        private readonly ITypeDepenseServices _typeDepenseServices;

        public GroupeDepenseController(IGroupeDepenseServices groupeDepenseServices, IGroupeFraisServices groupeFraisServices, ITypeDepenseServices typeDepenseServices)
        {
            _groupeDepenseServices = groupeDepenseServices;
            _groupeFraisServices = groupeFraisServices;
            _typeDepenseServices = typeDepenseServices;
        }

        [ProducesResponseType(200)]
     //   [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_GroupOfExpenseList)]
        [HttpGet, Route("GetAllGroupeFraisDepense")]
        public IActionResult GetAllGroupeFraisDepense()
        {
            return StatusCode(200, _groupeDepenseServices.GetAllGroupeFraisDepense());
        }

        [ProducesResponseType(200)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_GroupOfExpenseList)]
        [HttpGet, Route("GetGroupeFraisDepenseByID")]
        public IActionResult GetGroupeFraisDepenseByID(int id)
        {
            var groupeDepense = _groupeDepenseServices.GetGroupeFraisDepenseByID(id);
            if (groupeDepense == null)
                return NotFound();
            else
                return StatusCode(200, groupeDepense);
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_GroupOfExpenseList)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] GroupeFraisDepense groupeFraisDepense)
        {
            try
            {
                if (_groupeDepenseServices.checkUnicity(groupeFraisDepense))
                {
                    var groupeFrais = _groupeFraisServices.GetGroupeFraisByID(groupeFraisDepense.GroupeFraisID);
                    if (groupeFrais == null)
                        return NotFound();
                    groupeFraisDepense.companyID = groupeFrais.companyID;
                    groupeFraisDepense.TypeDepense = null;
                    var c = Convert.ToInt32(groupeFraisDepense.TypeDepenseID);
                    var tp = _typeDepenseServices.GetTypeDepenseById(c);
                    groupeFraisDepense.TypeDepense = tp;
                    var b = Convert.ToInt32(groupeFraisDepense.GroupeFraisID);
                    var grfrais = _groupeFraisServices.GetGroupeFraisByID(b);
                    groupeFraisDepense.GroupeFrais = grfrais;
                    groupeFraisDepense = _groupeDepenseServices.Create(groupeFraisDepense);
                }
                else
                {
                    return StatusCode(400, "TypeOfExpenseExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, groupeFraisDepense);
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_GroupOfExpenseList)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] GroupeFraisDepense groupeFraisDepense)
        {
            var groupeFraisDepenseModified = new GroupeFraisDepense();
            try
            {
                groupeFraisDepenseModified = _groupeDepenseServices.GetGroupeFraisDepenseByID(groupeFraisDepense.Id);
                if (groupeFraisDepenseModified == null)
                    return NotFound();
                groupeFraisDepenseModified.GroupeFrais = groupeFraisDepense.GroupeFrais;
                groupeFraisDepenseModified.GroupeFraisID = groupeFraisDepense.GroupeFraisID;
                groupeFraisDepenseModified.TypeDepense = groupeFraisDepense.TypeDepense;
                groupeFraisDepenseModified.TypeDepenseID = groupeFraisDepense.TypeDepenseID;
                groupeFraisDepenseModified.Plafond = groupeFraisDepense.Plafond;
                groupeFraisDepenseModified.Forfait = groupeFraisDepense.Forfait;
                groupeFraisDepenseModified = _groupeDepenseServices.Edit(groupeFraisDepenseModified);

            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, groupeFraisDepenseModified);
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_GroupOfExpenseList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var groupefraisDepense = _groupeDepenseServices.Delete(id);
                return StatusCode(200, groupefraisDepense);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_GroupOfExpenseList)]
        [HttpGet, Route("GetAllTypeDepenseByGroupeId")]
        public IActionResult GetAllTypeDepenseByGroupeId(int id)
        {
            return StatusCode(200, _groupeDepenseServices.GetAllGroupeDepenseByGroupeId(id));
        }

        [ProducesResponseType(200)]
 //       [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetGroupefraisByTypeDepense")]
        public IActionResult GetGroupefraisByTypeDepense(int idTypeDepense, int groupeFraisID)
        {
            return StatusCode(200, _groupeDepenseServices.GetGroupefraisByTypeDepense(idTypeDepense, groupeFraisID));
        }
    }
}