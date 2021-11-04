using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Domain.ViewsModels;
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
    public class TypeCongeController : ControllerBase
    {
        private readonly ITypeCongeServices typeCongeServices;

        public TypeCongeController(ITypeCongeServices typeCongeServices)
        {
            this.typeCongeServices = typeCongeServices;
        }

        [HttpGet, Route("GetAllTypeConges")]
        public IActionResult GetAllTypeConges()
        {
            var listTypeConge = typeCongeServices.GetAllTypeConges();
            return StatusCode(200, listTypeConge);
        }

        [HttpGet, Route("GetTypeCongesById")]
        public IActionResult GetTypeCongesById(int id)
        {
            var TypeConge = typeCongeServices.GetTypeCongetByID(id);
            return StatusCode(200, TypeConge);
        }

        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] TypeCongeViewModel typeConge)
        {
            TypeConge typeCongeCreated = new TypeConge();
            typeCongeCreated.Code = typeConge.Code;
            typeCongeCreated.Intitule = typeConge.Intitule;
            typeCongeCreated.CongeAnnuel = typeConge.CongeAnnuel;
            typeCongeCreated.IsRemplacement = typeConge.IsRemplacement;
            typeCongeCreated.soldeNegatif = typeConge.soldeNegatif;
            typeCongeCreated.notification = typeConge.notification;
            typeCongeCreated.messageNotification = typeConge.messageNotification;
            if (!typeConge.CongeAnnuel)
            {
                typeCongeCreated.NombreJours = typeConge.NombreJours;
                typeCongeCreated.NombreFois = typeConge.NombreFois;
                if (typeConge.Period != null)
                    typeCongeCreated.Period = (TypeCongePeriod)typeConge.Period;
            }
            try
            {
                if (typeConge == null) throw new Exception("Pas de données pour le TypeConge ");
                if (typeCongeServices.CheckSameTypeConge(typeConge))
                {
                    if (typeConge.CongeAnnuel == true)
                    {
                        if (typeCongeServices.CheckSameAnnualLeave(typeConge))
                        {
                            return StatusCode(400, "TypeCongeAnnuelExist");
                        }
                    }
                    typeCongeCreated.companyID = typeConge.companyID;
                    typeCongeCreated = typeCongeServices.Create(typeCongeCreated);
                    if (typeCongeCreated == null)
                    {
                        return StatusCode(400, "Failleavetype");
                    }
                }
                else
                {
                    return StatusCode(400, "TypeCongeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, typeCongeCreated);
        }
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] TypeCongeViewModel typeConge)
        {
            try
            {
                typeConge.Id = id;
                if (typeCongeServices.CheckSameTypeConge(typeConge))
                {
                    if (typeConge.CongeAnnuel == true)
                    {
                        if (typeCongeServices.CheckSameAnnualLeave(typeConge))
                        {
                            return StatusCode(400, "TypeCongeAnnuelExist");
                        }
                    }
                    var typeCongeModified = typeCongeServices.GetTypeCongetByID(id);
                    if (typeCongeModified == null)
                        return NotFound();
                    typeCongeModified.Code = typeConge.Code;
                    typeCongeModified.Intitule = typeConge.Intitule;
                    typeCongeModified.CongeAnnuel = typeConge.CongeAnnuel;
                    typeCongeModified.soldeNegatif = typeConge.soldeNegatif;
                    typeCongeModified.IsRemplacement = typeConge.IsRemplacement;
                    typeCongeModified.notification = typeConge.notification;
                    typeCongeModified.messageNotification = typeConge.messageNotification;
                    if (!typeConge.CongeAnnuel)
                    {
                        typeCongeModified.NombreJours = typeConge.NombreJours;
                        typeCongeModified.NombreFois = typeConge.NombreFois;
                        if (typeConge.Period != null)
                            typeCongeModified.Period = (TypeCongePeriod)typeConge.Period;
                    }
                    typeCongeModified.companyID = typeConge.companyID;
                    typeCongeModified = typeCongeServices.Edit(typeCongeModified);
                    return StatusCode(200, typeConge);
                }
                else
                {
                    return StatusCode(400, "TypeCongeExist");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id, [FromBody] TypeConge typeConge)
        {
            try
            {
                var type = typeCongeServices.Delete(id);
                if (type == null)
                {
                    return StatusCode(400, "FailDeleteTypeConge");
                }
                return StatusCode(200, type);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    }
}
