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
    public class GroupeFraisController : ControllerBase
    {
        private readonly IGroupeFraisServices _groupeFraisServices;

        public GroupeFraisController(IGroupeFraisServices groupeFraisServices)
        {
            _groupeFraisServices = groupeFraisServices;
        }

        [ProducesResponseType(200)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_GroupOfExpenseList)]
        [HttpGet, Route("GetAllGroupeFrais")]
        public IActionResult GetAllGroupeFrais()
        {
            return StatusCode(200, _groupeFraisServices.GetAllGroupeFrais());
        }

        [ProducesResponseType(200)]
   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_GroupOfExpenseList)]
        [HttpGet, Route("GetGroupeFraisByID")]
        public IActionResult GetGroupeFraisByID(int id)
        {
            var GroupeFrais = _groupeFraisServices.GetGroupeFraisByID(id);
            if (GroupeFrais == null)
                return NotFound();
            else
                return StatusCode(200, GroupeFrais);
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_GroupOfExpenseList)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] GroupeFrais groupeFrais)
        {
            try
            {
                if (groupeFrais == null) throw new Exception("FailGroupeFraisObject");

                if (_groupeFraisServices.checkUnicity(groupeFrais))
                {
                    groupeFrais = _groupeFraisServices.Create(groupeFrais);
                    if (groupeFrais == null)
                    {
                        return StatusCode(400, "FailCreateGroupeFrais");
                    }
                }
                else
                {
                    return StatusCode(400, "ExpensesGroupExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, groupeFrais);
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_GroupOfExpenseList)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] GroupeFrais groupeFrais)
        {
            try
            {
                if (_groupeFraisServices.checkUnicity(groupeFrais))
                {
                    var groupeFraisModified = _groupeFraisServices.GetGroupeFraisByID(id);
                    if (groupeFraisModified == null)
                        return NotFound();
                    groupeFraisModified.Intitule = groupeFrais.Intitule;
                    groupeFraisModified.Code = groupeFrais.Code;
                    groupeFraisModified.PrixUPerso = groupeFrais.PrixUPerso;
                    groupeFraisModified.PrixUPro = groupeFrais.PrixUPro;
                    groupeFraisModified = _groupeFraisServices.Edit(groupeFraisModified);
                    return StatusCode(200, groupeFrais);
                }
                else
                {
                    return StatusCode(400, "ExpensesGroupExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Delete_GroupOfExpenseList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var group = _groupeFraisServices.Delete(id);
                if (group == null)
                {
                    return StatusCode(400, "FailDeleteGroupeFrais");
                }
                return StatusCode(200, group);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}