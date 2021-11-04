using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WFDocumentsController : ControllerBase
    {
        private readonly IWFDocumentServices _wFDocumentServices;

        public WFDocumentsController(IWFDocumentServices wFDocumentServices)
        {
            _wFDocumentServices = wFDocumentServices;
        }
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] WFDocument wFDocument)
        {
            try
            {
                if (wFDocument == null) throw new Exception("FailwFDocumentObject");
                if (_wFDocumentServices.checkUnicity(wFDocument))
                { 
                wFDocument = _wFDocumentServices.Create(wFDocument);
                    if (wFDocument == null)
                    {
                        return StatusCode(400, "FailCreatewFDocument");
                    }
                }
                else
                {
                    return StatusCode(400, "ExpenseswFDocumentxist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, wFDocument);
        }


    }
}
