using Compank.Domain;
using Compank.Services;
using Conge.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Event.Contracts;

namespace Compank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyServices companyServices;
        private readonly IPublishEndpoint _publishEndpoint;

        public CompaniesController(ICompanyServices companyServices, IPublishEndpoint publishEndpoint)
        {
            this.companyServices = companyServices;
            _publishEndpoint = publishEndpoint;
           
        }
        [ProducesResponseType(200)]
        [HttpGet, Route("GetAllCompany")]
        public IActionResult GetAllCompany()
        {
            var listCompany = companyServices.GetAllCompany();
            return StatusCode(200, listCompany);
        }

        [ProducesResponseType(200)]
        [HttpGet, Route("GetCompanyById")]
        public IActionResult GetCompanyById(int id)
        {
            var Company = companyServices.GetCompanyByID(id);
            return StatusCode(200, Company);

        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost, Route("POST")]
        public async Task<IActionResult> POSTAsync([FromBody] Companyk companyK)
        {
            var company = new Companyk
            {
                Id = companyK.Id,
               UserCreat = companyK.UserCreat,
                UserModif = companyK.UserModif,
                DateCreat = DateTime.UtcNow,
                DateModif = DateTime.UtcNow,
                Name = companyK.Name,
                Description = companyK.Description,
                Adress = companyK.Adress,
            Telephone = companyK.Telephone,
                LegalStatus = companyK.LegalStatus,
                FiscalNumber = companyK.FiscalNumber,
                TradeRegister = companyK.TradeRegister,
                Numero = companyK.Numero,
                CodePostal = companyK.CodePostal,
                Ville = companyK.Ville,
                ComplementAdresse = companyK.ComplementAdresse




            };

            companyServices.Create(company);
            //  await _publishEndpoint.Publish(new CompanyCreated(company.Id, company.Name, company.Adress, company.Numero));
            await _publishEndpoint.Publish(new CompanyGetID(company.Id));
            return StatusCode(200, company);
        }



        [ProducesResponseType(200)]
        [HttpGet, Route("GetCompanyByName")]
        public IActionResult GetCompanyByName(string name)
        {
            var Company = companyServices.GetCompanyByName(name);
            return StatusCode(200, Company);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Societe = companyServices.Delete(id);
                if (Societe == null)
                {
                    return StatusCode(400, "FailDeleteCompany");
                }
                return StatusCode(200, Societe);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
