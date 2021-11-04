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
    public class EmployeeVehiculeController : Controller
    {
        private readonly IEmployeeVehiculeServices _employeeVehiculeServices;
        private readonly IEmployeeServices employeeServices;

        public EmployeeVehiculeController(IEmployeeVehiculeServices employeeVehiculeServices, IEmployeeServices emp)
        {
            _employeeVehiculeServices = employeeVehiculeServices;
            employeeServices = emp;
        }

        [ProducesResponseType(200)]
     // [ClaimRequirement("Privilege", ApiPrivileges.Employees_Read_NoteEmployee)]
        [HttpGet, Route("GetAllEmployeeVehicule")]
        public IActionResult GetAllEmployeeVehicule()
        {
            return StatusCode(200, _employeeVehiculeServices.GetAllEmployeeVehicule());
        }

        [ProducesResponseType(200)]
     // [ClaimRequirement("Privilege", ApiPrivileges.Employees_Read_NoteEmployee)]
        [HttpGet, Route("GetEmployeeVehiculeByID")]
        public IActionResult GetEmployeeVehiculeByID(int id)
        {
            var EmployeeVehicule = _employeeVehiculeServices.GetEmployeeVehiculeByID(id);
            if (EmployeeVehicule == null)
                return null;
            else
                return StatusCode(200, EmployeeVehicule);
        }

        [ProducesResponseType(200)]
     // [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_NoteEmployee)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] EmployeeVehicule employeeVehicule)
        {
            try
            {
                if (employeeVehicule == null) throw new Exception("FailEmployeeVehiculeObject");
                if (_employeeVehiculeServices.checkUnicity(employeeVehicule))
                {
                    employeeVehicule.DateDebut = employeeVehicule.DateDebut.Date;
                    employeeVehicule.DateFin = employeeVehicule.DateFin.Date;
                    employeeVehicule.ValiditeCarte = employeeVehicule.ValiditeCarte.Date;
                    var po = employeeServices.GetEmployeByID(employeeVehicule.EmployeeId);
                    employeeVehicule.Employee = po;
                    employeeVehicule = _employeeVehiculeServices.Create(employeeVehicule);
                    if (employeeVehicule == null)
                    {
                        return StatusCode(400, "FailCreateEmployeeVehicule");
                    }
                }
                else
                {
                    return StatusCode(400, "VehicleAlreadyAffected");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, employeeVehicule);
        }


    //  [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_NoteEmployee)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] EmployeeVehicule employeeVehicule)
        {
            try
            {
                employeeVehicule.Id = id;
                if (_employeeVehiculeServices.checkUnicity(employeeVehicule))
                {
                    var employeeVehiculeModified = _employeeVehiculeServices.GetEmployeeVehiculeByID(id);
                    if (employeeVehiculeModified == null)
                        return NotFound();
                    employeeVehiculeModified.Matricule = employeeVehicule.Matricule;
                    employeeVehiculeModified.Marque = employeeVehicule.Marque;
                    employeeVehiculeModified.PuissanceFiscale = employeeVehicule.PuissanceFiscale;
                    employeeVehiculeModified.TiTulaireVehProf = employeeVehicule.TiTulaireVehProf;
                    employeeVehiculeModified.Type = employeeVehicule.Type;
                    employeeVehiculeModified.TiTulaireCarteEssence = employeeVehicule.TiTulaireCarteEssence;
                    employeeVehiculeModified.Plafond = employeeVehicule.Plafond;
                    employeeVehiculeModified.TypeVehicule = employeeVehicule.TypeVehicule;
                    employeeVehiculeModified.DateDebut = employeeVehicule.DateDebut.Date;
                    employeeVehiculeModified.DateFin = employeeVehicule.DateFin.Date;
                    employeeVehiculeModified.ValiditeCarte = employeeVehicule.ValiditeCarte.Date;
                    employeeVehiculeModified = _employeeVehiculeServices.Edit(employeeVehiculeModified);
                    return StatusCode(200, employeeVehiculeModified);
                }
                else
                {
                    return StatusCode(400, "VehicleAlreadyAffected");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

     // [ClaimRequirement("Privilege", ApiPrivileges.Employees_Edit_NoteEmployee)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employeeVehicule = _employeeVehiculeServices.Delete(id);
                if (employeeVehicule == null)
                {
                    return StatusCode(400, "FailDeleteEmployeeVehicule");
                }
                return StatusCode(200, employeeVehicule);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [ProducesResponseType(200)]
     // [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllVehiculeByEmployeeId")]
        public IActionResult GetAllVehiculeByEmployeeId(int id)
        {
            return StatusCode(200, _employeeVehiculeServices.GetAllEmployeeVehiculeByEmployeeId(id));
        }

        [ProducesResponseType(200)]
    //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpPut, Route("GetAllVehiculeByEmployeeIdNote")]
        public IActionResult GetAllVehiculeByEmployeeIdNote([FromBody] VehiculeByEmployeeIdDateViewModel vehiculeByEmployeeIdDate)
        {
            return StatusCode(200, _employeeVehiculeServices.GetAllEmployeeVehiculeByEmployeeIdAndDate(vehiculeByEmployeeIdDate.id, vehiculeByEmployeeIdDate.dateDebut, vehiculeByEmployeeIdDate.dateFin));
        }

        [ProducesResponseType(200)]
     // [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllTypeVehiculeByEmployeeId")]
        public IActionResult GetAllTypeVehiculeByEmployeeId(int id)
        {
            return StatusCode(200, _employeeVehiculeServices.GetAllTypeVehiculeByEmployeeId(id));
        }
    }
}