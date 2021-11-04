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
    public class DroitCongeController : ControllerBase
    {
        private readonly IDroitCongeServices _droitCongeServices;
        private readonly IEmployeeServices _employeeServices;
        private readonly IDetailsDroitCongeServices _detailsDroitCongeServices;
        private readonly IAffectationEmployeeServices _affectationEmployeeServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DroitCongeController(IAffectationEmployeeServices affectationEmployeeServices, IDetailsDroitCongeServices detailsDroitCongeServices,
              IDroitCongeServices droitCongeServices, IEmployeeServices employeeServices, IHttpContextAccessor httpContextAccessor)
        {
            _droitCongeServices = droitCongeServices;
            _employeeServices = employeeServices;
            _affectationEmployeeServices = affectationEmployeeServices;
            _detailsDroitCongeServices = detailsDroitCongeServices;
            _httpContextAccessor = httpContextAccessor;

        }
   //     [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Read_LeaveRightList)]
        [HttpGet, Route("GetAllDroitConge")]
        public IActionResult GetAllDroitConge()
        {
            var listDroitConge = _droitCongeServices.GetAllDroitConge();
            return StatusCode(200, listDroitConge);
        }

  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetDroitCongeByID")]
        public IActionResult GetDroitCongeByID(int id)
        {
            var listDroitConge = _droitCongeServices.GetDroitCongeByID(id);
            return StatusCode(200, listDroitConge);
        }

 //       [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Add_LeaveRightList)]
        [HttpPost, Route("Post")]
        public IActionResult Post([FromBody] DroitConge droitConge)
        {
            DroitConge droit = new DroitConge();
            try
            {
                var add = true;
                var droitCongeListByMonth = _droitCongeServices.GetDroitCongeByDate(droitConge.MoisAffectation);
                if (droitCongeListByMonth.Count > 0)
                {
                    var numbreDetailsDroitCongé = 0;
                    var emplList = _employeeServices.GetAllEmployeeByCompanyId().ToList();
                    List<Employee> activeEmplList = new List<Employee>();
                    foreach (var empl in emplList)

                    {
                        var actifAffection = _affectationEmployeeServices.GetAffectationActif(empl.Id);
                        if (actifAffection != null) activeEmplList.Add(empl);
                    }
                    foreach (var droit_conge in droitCongeListByMonth)

                    {
                        var detailsDroitCongeList = _detailsDroitCongeServices.GetDetailsDroitCongeByDroitCongeId(droit_conge.Id);
                        numbreDetailsDroitCongé = numbreDetailsDroitCongé + detailsDroitCongeList.Count;
                    }
                    if (numbreDetailsDroitCongé >= activeEmplList.Count)
                    {
                        add = false;
                    }
                }
                if (add)
                {
                    var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                    int currentCompanyId = int.Parse(session[2].Value);
                    droitConge.companyID = currentCompanyId;
                    droit = _droitCongeServices.Create(droitConge);
                    if (droit == null)
                    {
                        return StatusCode(400, "FailRightLeave");
                    }
                }
                else
                {
                    return StatusCode(400, "LeaveRightExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, droit);
        }

  //      [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("Details")]
        public IActionResult Details(int id)
        {
            var listDroitConge = _droitCongeServices.GetDroitCongeByID(id);
            return StatusCode(200, listDroitConge);
        }

//        [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveRightList)]
        [HttpPut, Route("Put")]
        public IActionResult Put(int id, [FromBody] DroitConge droitConge)
        {
            try
            {
                var drCong = _droitCongeServices.GetDroitCongeByID(id);
                if (drCong == null)
                {
                    return StatusCode(404, "NoDataFound");
                }

                var droitCongeListByMonth = _droitCongeServices.GetDroitCongeByDate(droitConge.MoisAffectation).Where(d => d.Id != id).ToList();
                if (droitCongeListByMonth.Count > 0)
                {
                    var numbreDetailsDroitCongé = 0;

                    foreach (var droit_conge in droitCongeListByMonth)

                    {
                        var detailsDroitCongeList = _detailsDroitCongeServices.GetDetailsDroitCongeByDroitCongeId(droit_conge.Id);
                        numbreDetailsDroitCongé = numbreDetailsDroitCongé + detailsDroitCongeList.Count;
                    }
                    var EmplList = _employeeServices.GetAllEmployeeByCompanyId().ToList();
                    List<Employee> ActiveEmplList = new List<Employee>();
                    foreach (var empl in EmplList)
                    {
                        var ActifAffect = _affectationEmployeeServices.GetAffectationActif(empl.Id);
                        if (ActifAffect != null) ActiveEmplList.Add(empl);
                    }
                    if (numbreDetailsDroitCongé >= ActiveEmplList.Count)
                    {
                        return StatusCode(400, "EditDroitNotAllowed");
                    }
                }
                drCong.Numero = droitConge.Numero;
                drCong.Date = droitConge.Date;
                drCong.MoisAffectation = droitConge.MoisAffectation;
                drCong.Status = droitConge.Status;
                drCong = _droitCongeServices.Edit(drCong);
                return StatusCode(200, drCong);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
