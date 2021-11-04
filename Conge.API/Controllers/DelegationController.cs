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
    public class DelegationController : ControllerBase
    {
        
        private readonly IDelegationService _delegationService;
        private readonly IEmployeeServices _employeeServices;

        public DelegationController(IDelegationService delegationService, IEmployeeServices employeeServices)
        {
           
            _delegationService = delegationService;
            _employeeServices = employeeServices;

        }
 //       [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllDelegations")]
        public IActionResult GetAllDelegations()
        {
            var listDelegation = _delegationService.GetAllDelegations();
            return StatusCode(200, listDelegation);
        }

   //     [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetDelegationById")]
        public IActionResult GetDelegationById(int id)
        {
            var delegation = _delegationService.GetDelegationById(id);
            return StatusCode(200, delegation);
        }
  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetDelegationByTitre")]
        public IActionResult GetDelegationByTitre(int id)
        {
            var delegation = _delegationService.GetDelegationByTitre(id);
            return StatusCode(200, delegation);
        }
  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpPost, Route("POST")]
        public IActionResult CreateDelegation(Delegation delegation)
        {
            var Delegation = _delegationService.Create(delegation);
            if (Delegation != null)
            {
                return StatusCode(200, Delegation);
            }
            else
            {
                return StatusCode(400, "Not_Found");
            }
        }

  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("HasDelegation")]
        public IActionResult hasDelegation(int id)
        {
            var delegation = _delegationService.hasDelegation(id);
            return StatusCode(200, delegation);
        }

  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("getDelegator")]
        public IActionResult getDelegator(int id)
        {
            string delegatorName = null;
            var delegator = _delegationService.getDelegator(id);
            var employee = _employeeServices.GetEmployeeByID(delegator);
            if (employee != null)
            {
                delegatorName = employee.User;
            }
            return StatusCode(200, delegatorName);
        }

  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("getSubstituter")]
        public IActionResult getSubstituter(int id)
        {
            int SubstituterName = 0;
            var substituter = _delegationService.getSubstituter(id);
            var employee = _employeeServices.GetEmployeeByID(substituter);
            if (employee != null)
            {
                SubstituterName = employee.Id;
            }
            return StatusCode(200, SubstituterName);
        }

     //   [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("getDelegation")]
        public IActionResult getDelegation(int id)
        {
            var delegation = _delegationService.getDelegation(id);

            return StatusCode(200, delegation);
        }

      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("getDelegationBySubstituter")]
        public IActionResult getDelegationBySubstituter(int id)
        {
            var delegation = _delegationService.getDelegationByRemplacant(id);

            return StatusCode(200, delegation);
        }


    }
}
