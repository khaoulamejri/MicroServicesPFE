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
    public class TypeDepenseController : ControllerBase
    {
        private readonly ITypeDepenseServices _typeDepenseServices;
        private readonly ICompteComptableServices _compteComptableServices;

        public TypeDepenseController(ITypeDepenseServices typeDepenseServices, ICompteComptableServices compteComptableServices)
        {
            _typeDepenseServices = typeDepenseServices;
            _compteComptableServices = compteComptableServices;
        }

        [ProducesResponseType(200)]
 //       [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllTypeDepense")]
        public IActionResult GetAllTypeDepense()
        {
            return StatusCode(200, _typeDepenseServices.GetAllTypeDepense());
        }

        [ProducesResponseType(200)]
   //     [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetTypeDepenseByID")]
        public IActionResult GetTypeDepenseByID(int id)
        {
            return StatusCode(200, _typeDepenseServices.GetTypeDepenseByIDIncluded(id));
        }

  //      [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Add_TypeOfExpenseList)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] TypeDepense typeDepense)
        {
            try
            {
                if (typeDepense == null) throw new Exception("FailTypeDepenseObject");

                if (_typeDepenseServices.checkUnicity(typeDepense, true))
                {
                    var c = Convert.ToInt32(typeDepense.CompteComptableID);
                    var compte = _compteComptableServices.GetCompteComptableByID(c);
                    typeDepense.CompteComptable = compte;
                    typeDepense = _typeDepenseServices.Create(typeDepense);
                    if (typeDepense == null)
                    {
                        return StatusCode(400, "FailCreateTypeDepense");
                    }
                    typeDepense = _typeDepenseServices.GetTypeDepenseByIDIncluded(typeDepense.Id);
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
            return StatusCode(200, typeDepense);
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_TypeOfExpenseList)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] TypeDepense typeDepense)
        {
            try
            {
                if (typeDepense == null) throw new Exception("FailTypeDepenseObject");
                if (_typeDepenseServices.checkUnicity(typeDepense, false))
                {
                    var typeDepenseModified = _typeDepenseServices.GetTypeDepenseByIDIncluded(id);
                    if (typeDepenseModified == null) throw new Exception("FailTypeDepenseObject");
                    var compteComptable = _compteComptableServices.GetCompteComptableByID((int)typeDepense.CompteComptableID);
                    typeDepenseModified.Intitule = typeDepense.Intitule;
                    typeDepenseModified.Code = typeDepense.Code;
                    typeDepenseModified.CompteComptableID = typeDepense.CompteComptableID;
                    typeDepenseModified.TVA = typeDepense.TVA;
                    typeDepenseModified.CompteComptable = compteComptable;
                    typeDepenseModified = _typeDepenseServices.Edit(typeDepenseModified);
                    return StatusCode(200, typeDepenseModified);
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
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Delete_TypeOfExpenseList)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var typeDepense = _typeDepenseServices.Delete(id);
                if (typeDepense == null)
                {
                    return StatusCode(400, "FailDeleteTypeDepense");
                }
                return StatusCode(200, typeDepense);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}