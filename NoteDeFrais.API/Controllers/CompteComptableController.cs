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
    public class CompteComptableController : ControllerBase
    {
        private ICompteComptableServices _compteComptableServices;
        public CompteComptableController(ICompteComptableServices compteComptableServices)
        {
            _compteComptableServices = compteComptableServices;
        }

      //[ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_CompteComptable)]
        [HttpGet, Route("GetCompteComptableById")]
        public IActionResult GetCompteComptableById(int id)
        {
            var CompteComtable = _compteComptableServices.GetCompteComptableByID(id);
            if (CompteComtable != null)
                return StatusCode(200, CompteComtable);
            else
                return NotFound();
        }


     // [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_CompteComptable + ";" + ApiPrivileges.ExpenseReportModule_Settings_Read_TypeOfExpenseList)]
        [HttpGet, Route("GetListCompteComptable")]
        public IActionResult GetListCompteComptable()
        {
            return StatusCode(200, _compteComptableServices.GetAllCompteComptableByCompanyID());
        }


   //   [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Add_CompteComptable)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] CompteComptable compteComptable)
        {
            try
            {
                if (compteComptable == null)
                {
                    return StatusCode(400, "FailCompteComptableObject");
                }
                if (_compteComptableServices.checkUnicity(compteComptable))
                {
                    compteComptable = _compteComptableServices.Create(compteComptable);
                    if (compteComptable == null)
                    {
                        return StatusCode(400, "FailCreateCompteComptable");
                    }
                }
                else
                {
                    return StatusCode(400, "AccountExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, compteComptable);
        }


      //[ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_CompteComptable)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] CompteComptable compteComptable)
        {
            try
            {
                compteComptable.Id = id;
                if (compteComptable == null)
                {
                    return StatusCode(400, "FailCompteComptableObject");
                }
                if (_compteComptableServices.checkUnicity(compteComptable))
                {
                    var cptmodified = _compteComptableServices.GetCompteComptableByID(id);
                    if (cptmodified == null)
                        return NotFound();
                    else
                    {
                        if (cptmodified == null) return StatusCode(400, "FailCompteComptableObject");
                        cptmodified.Code = compteComptable.Code;
                        cptmodified.Compte = compteComptable.Compte;
                        cptmodified.Description = compteComptable.Description;
                        cptmodified.companyID = compteComptable.companyID;
                        cptmodified = _compteComptableServices.Edit(cptmodified);
                        return StatusCode(200, cptmodified);
                    }

                }
                else
                {
                    return StatusCode(400, "AccountExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

     // [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Delete_CompteComptable)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var compte = _compteComptableServices.Delete(id);
                if (compte == null)
                {
                    return StatusCode(400, "FailDeleteCompteComptable");
                }
                return StatusCode(200, compte);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
    
