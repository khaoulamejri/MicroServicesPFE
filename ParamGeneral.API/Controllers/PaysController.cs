using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class PaysController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPaysServices paysServices;
        private readonly IDeviseServices deviseServices;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaysController(IConfiguration configuration, IPaysServices p, IDeviseServices d, IPublishEndpoint publishEndpoint)
        {
            _configuration = configuration;
            paysServices = p;
            deviseServices = d;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet, Route("GetAllPays")]
        public IActionResult GetAllPays()
        {
            var listPays = paysServices.GetAllPays().OrderBy(x => x.Intitule);
            return StatusCode(200, listPays);
        }

        [ProducesResponseType(200)]
        [HttpGet, Route("GetPaysById")]
        public IActionResult PaysById(int id)
        {
            var p = paysServices.GetPaysByID(id);
            return StatusCode(200, p);
        }

        [HttpPost, Route("POST")]
        public async Task<IActionResult> POSTAsync([FromBody] Pays Pays)
        {
            var p = new Pays();
            try
            {
                if (Pays == null)
                {
                    return StatusCode(400, "FailPaysObject");
                }

                if (paysServices.CheckUnicityPays(Pays.Code))
                {
                    var a = Convert.ToInt32(Pays.DeviseID);
                    var dev = deviseServices.GetDeviseByID(a);
                    Pays.Devise = dev;
                    Pays.DeviseCode = dev.Intitule;
                    p = paysServices.Create(Pays);
                    await _publishEndpoint.Publish(new PaysUserCreated(Pays.Id, Pays.Code, Pays.Intitule, Pays.DeviseCode));
                    await _publishEndpoint.Publish(new PayCreated(Pays.Id, Pays.Code, Pays.Intitule, Pays.DeviseCode));
                    await _publishEndpoint.Publish(new PayNoteCreated(Pays.Id, Pays.Code, Pays.Intitule, Pays.DeviseCode));


                    if (p == null)
                    {
                        return StatusCode(400, "FailCreatePays");
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
            return StatusCode(200, p);
        }

        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Pays Pays)
        {
            var p = new Pays();
            try
            {
                if (paysServices.CheckUnicityPaysByID(Pays.Code, id))
                {
                    var PaysModified = paysServices.GetPaysByID(id);
                    PaysModified.Intitule = Pays.Intitule;
                    PaysModified.Code = Pays.Code;
                    PaysModified.DeviseID = Pays.DeviseID;
                    var a = Convert.ToInt32(Pays.DeviseID);
                    var dev = deviseServices.GetDeviseByID(a);
                    PaysModified.DeviseCode = dev.Intitule;
                    PaysModified.Devise = dev;
                    paysServices.Edit(PaysModified);
                    _publishEndpoint.Publish(new PaysUpdated(PaysModified.Id, PaysModified.Code, PaysModified.Intitule, PaysModified.DeviseCode));

                    return StatusCode(200, PaysModified);
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

        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var p = paysServices.Delete(id);
                _publishEndpoint.Publish(new PaysDeleted(id));
                if (p == null)
                {
                    return StatusCode(400, "FailDeletePays");
                }
                return StatusCode(200, p);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}