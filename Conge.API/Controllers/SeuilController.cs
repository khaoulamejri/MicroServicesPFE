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
    public class SeuilController : ControllerBase
    {
        private readonly ISeuilServices _seuilServices;

        public SeuilController(ISeuilServices seuil)
        {
            _seuilServices = seuil;
        }

//        [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Read_Seuil)]
        [HttpGet, Route("GetAllseuils")]
        public IActionResult GetAllseuils()
        {
            var listSeuil = _seuilServices.GetAllseuils();
            return StatusCode(200, listSeuil);
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Add_Seuil)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Seuils seuil)
        {
            Seuils createdSeuil = new Seuils();
            try
            {
                if (seuil == null) throw new Exception("FailEmptyseuil");
                if (_seuilServices.checkUnicity(seuil))
                {
                    createdSeuil = _seuilServices.Create(seuil);
                    if (createdSeuil == null)
                    {
                        return StatusCode(400, "FailCreatseuil");
                    }
                }
                else
                {
                    return StatusCode(400, "CodeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, createdSeuil);
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_Seuil)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Seuils seuil)
        {
            Seuils sel = new Seuils();
            try
            {
                if (_seuilServices.checkUnicity(seuil))
                {
                    var SeuilsModified = _seuilServices.GetSeuilByID(id);
                    if (SeuilsModified == null)
                        return NotFound();
                    SeuilsModified.Seuil = seuil.Seuil;
                    SeuilsModified.Valeur = seuil.Valeur;
                    SeuilsModified = _seuilServices.Edit(SeuilsModified);
                    return StatusCode(200, seuil);
                }
                else
                {
                    return StatusCode(400, "CodeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Delete_Seuil)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id, [FromBody] Seuils seuil)
        {
            try
            {
                var sel = _seuilServices.Delete(id);
                if (sel == null)
                {
                    return StatusCode(400, "FailDeleteSeuil");
                }
                return StatusCode(200, sel);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}

