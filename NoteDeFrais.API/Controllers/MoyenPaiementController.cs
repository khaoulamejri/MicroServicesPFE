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
    public class MoyenPaiementController : ControllerBase
    {
        private readonly IMoyenPaiementServices _moyenPaiementServices;
        public MoyenPaiementController(IMoyenPaiementServices moyenPaiementServices)
        {
            _moyenPaiementServices = moyenPaiementServices;
        }

        [ProducesResponseType(200)]
    //    [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllMoyenPaiement")]
        public IActionResult GetAllMoyenPaiement()
        {
            return StatusCode(200, _moyenPaiementServices.GetAllMoyenPaiement());
        }

        [ProducesResponseType(200)]
  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetMoyenPaiementByID")]
        public IActionResult GetMoyenPaiementByID(int id)
        {
            return StatusCode(200, _moyenPaiementServices.GetMoyenPaiementByID(id));
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Add_MeansOfPayment)]
        [HttpPost, Route("POST")]

        public IActionResult POST([FromBody] MoyenPaiement moyenPaiement)
        {
            try
            {
                if (moyenPaiement == null) throw new Exception("FailMoyenPaiementObject");
                if (_moyenPaiementServices.checkUnicity(moyenPaiement, true))
                {
                    moyenPaiement = _moyenPaiementServices.Create(moyenPaiement);

                    if (moyenPaiement == null)
                    {
                        return StatusCode(400, "FailCreateMoyenPaiement");
                    }
                }
                else
                {
                    return StatusCode(400, "paymentMethodExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, moyenPaiement);
        }

   //     [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_MeansOfPayment)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] MoyenPaiement moyenPaiement)
        {
            try
            {
                if (_moyenPaiementServices.checkUnicity(moyenPaiement, false))
                {
                    var moyenPaiementModified = _moyenPaiementServices.GetMoyenPaiementByID(id);
                    if (moyenPaiementModified == null)
                        return NotFound();
                    moyenPaiementModified.Intitule = moyenPaiement.Intitule;
                    moyenPaiementModified.Code = moyenPaiement.Code;
                    moyenPaiementModified.Type = moyenPaiement.Type;
                    moyenPaiementModified = _moyenPaiementServices.Edit(moyenPaiementModified);
                    return StatusCode(200, moyenPaiementModified);
                }
                else
                {
                    return StatusCode(400, "paymentMethodExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

  //      [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Delete_MeansOfPayment)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var moyenPaiement = _moyenPaiementServices.Delete(id);
                if (moyenPaiement == null)
                {
                    return StatusCode(400, "FailDeleteMoyenPaiement");
                }
                return StatusCode(200, moyenPaiement);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}