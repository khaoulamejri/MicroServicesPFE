using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviseController : ControllerBase
    {
        private readonly IDeviseServices _deviseServices;
        private readonly IPublishEndpoint _publishEndpoint;


        public DeviseController(IDeviseServices deviseServices, IPublishEndpoint publishEndpoint)
        {
            _deviseServices = deviseServices;
            _publishEndpoint = publishEndpoint;
          
        }


        [ProducesResponseType(200)]
      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllDevise")]
        public IActionResult GetAllDevise()
        {
            return StatusCode(200, _deviseServices.GetAllDevise());
        }



        [ProducesResponseType(200)]
      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetDeviseByID")]
        public IActionResult GetDeviseByID(int id)
        {
            var devise = _deviseServices.GetDeviseByID(id);
            if (devise == null)
                return NotFound();
            else
                return StatusCode(200, devise);
        }
      


        //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Read_CurrencyList)]
        [HttpGet, Route("GetDeviseByPays")]
        public IActionResult GetDeviseByPays(int id)
        {
            return StatusCode(200, _deviseServices.GetDeviseByPays(id));
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Add_CurrencyList)]
        [HttpPost, Route("POST")]

        public async Task<IActionResult> POSTAsync([FromBody] Devise devise)
        {
            try
            {
                if (devise == null) throw new Exception("FailDeviseObject");
                if (_deviseServices.checkUnicity(devise))
                {
                    devise.DateModif = DateTime.Now;
                    devise = _deviseServices.Create(devise);
                    await _publishEndpoint.Publish(new DeviseCongeCreated(devise.Id, devise.Code, devise.Intitule, devise.Decimal, devise.ExchangeRate, devise.DateModif));
                    await _publishEndpoint.Publish(new DeviseCreated(devise.Id, devise.Code, devise.Intitule, devise.Decimal, devise.ExchangeRate, devise.DateModif));
                    await _publishEndpoint.Publish(new DeviseNote(devise.Id,devise.Decimal));

                    
                    if (devise == null)
                    {
                        return StatusCode(400, "FailCreateDevise");
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
            return StatusCode(200, devise);
        }

      

        //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Edit_CurrencyList)]
        [HttpPut, Route("PUT")]
        public async Task<IActionResult> PUTAsync(int id, [FromBody] Devise devise)
        {
            try
            {
                if (devise == null) throw new Exception("FailDeviseObject");
                devise.Id = id;
                if (_deviseServices.checkUnicity(devise))
                {
                    var deviseModified = _deviseServices.GetDeviseByID(id);
                    if (deviseModified == null) throw new Exception("FailDeviseObject");
                    deviseModified.Intitule = devise.Intitule;
                    deviseModified.Code = devise.Code;
                    deviseModified.Decimal = devise.Decimal;
                    deviseModified.ExchangeRate = devise.ExchangeRate;
                    deviseModified.DateModif = DateTime.Now;

                    deviseModified = _deviseServices.Edit(deviseModified);
                    await _publishEndpoint.Publish(new DeviseUpdated(deviseModified.Id, deviseModified.Code, deviseModified.Intitule, deviseModified.Decimal, deviseModified.ExchangeRate, deviseModified.DateModif));

                    return StatusCode(200, deviseModified);
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

      //  [ClaimRequirement("Privilege", ApiPrivileges.ExpenseReportModule_Settings_Delete_CurrencyList)]
        [HttpDelete, Route("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var devise = _deviseServices.Delete(id);
                await _publishEndpoint.Publish(new DeviseDeleted(id));
                if (devise == null)
                {
                    return StatusCode(400, "FailDeleteDevise");
                }
                return StatusCode(200, devise);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    }
}