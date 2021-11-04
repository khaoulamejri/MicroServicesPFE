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
    //[ApiController]
    public class TypeHierarchyPositionsController : ControllerBase
    {
        private readonly ITypeHierarchyPositionsServices typeHierarchyPositionsServices;

        public TypeHierarchyPositionsController(ITypeHierarchyPositionsServices thp)
        {
            typeHierarchyPositionsServices = thp;
        }

      //[ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetAllTypeHierarchyPosition")]
        public IActionResult GetAllTypeHierarchyPosition()
        {
            var listTypeHierarchyPositions = typeHierarchyPositionsServices.GetAllTypeHierarchyPosition();
            return StatusCode(200, listTypeHierarchyPositions);
        }

    //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Add_HierarchyPosition)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] TypeHierarchyPosition TypeHierarchyPosition)
        {
            var hierarchiePos = new TypeHierarchyPosition();
            try
            {
                var test = typeHierarchyPositionsServices.GetAllTypeHierarchyPosition();
                if (TypeHierarchyPosition == null) throw new Exception("Pas de données pour le Type Hierarchy Position) ");
                if (typeHierarchyPositionsServices.CheckUnicityTypeHierarchyPosition(TypeHierarchyPosition.Code, TypeHierarchyPosition.Id))
                {
                    hierarchiePos = typeHierarchyPositionsServices.Create(TypeHierarchyPosition);
                    if (hierarchiePos == null)
                    {
                        return StatusCode(400, "CodeExist111");
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
            return StatusCode(200, hierarchiePos);
        }

       //ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_HierarchyPosition)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] TypeHierarchyPosition TypeHierarchyPosition)
        {
            try
            {
                if (typeHierarchyPositionsServices.CheckUnicityTypeHierarchyPosition(TypeHierarchyPosition.Code, id))
                {
                    var hierarchiePosModified = typeHierarchyPositionsServices.GetTypeHierarchyPositionByID(id);
                    hierarchiePosModified.Intitule = TypeHierarchyPosition.Intitule;
                    hierarchiePosModified.Code = TypeHierarchyPosition.Code;
                    typeHierarchyPositionsServices.Edit(hierarchiePosModified);
                    return StatusCode(200, TypeHierarchyPosition);
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

    //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Delete_HierarchyPosition)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var TypeHierarchy = typeHierarchyPositionsServices.Delete(id);
                if (TypeHierarchy == null)
                {
                    return StatusCode(400, "FailDeleteTypeHierarchy");
                }
                return StatusCode(200, TypeHierarchy);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
                throw new Exception();
            }
        }
    }
}
