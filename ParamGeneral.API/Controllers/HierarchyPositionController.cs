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
  //  [ApiController]
    public class HierarchyPositionController : ControllerBase
    {
        private readonly IHierarchyPositionServices hierarchyPositionServices;
        private readonly IPositionServices positionServices;
        private readonly ITypeHierarchyPositionsServices TypeHierarchyPositionsServices;

        public HierarchyPositionController(IHierarchyPositionServices hp, IPositionServices pos, ITypeHierarchyPositionsServices typehpos)
        {
            hierarchyPositionServices = hp;
            positionServices = pos;
            TypeHierarchyPositionsServices = typehpos;
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetHierarchyPositionByPositionId")]
        public IActionResult GetHierarchyPositionByPositionId(int positionId)
        {
            var listHierarchyPosition = hierarchyPositionServices.GetHierarchyPositionByPositionId(positionId);
            foreach (var hp in listHierarchyPosition)
            {
                var positioSup = positionServices.GetPositionByID(hp.PositionSupID);
                hp.PositionSup = positioSup;
            }
            return StatusCode(200, listHierarchyPosition);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetHierarchyPositionById")]
        public IActionResult GetHierarchyPositionById(int hpositionId)
        {
            var hierarchyPosition = hierarchyPositionServices.GetHierarchyPositionByID(hpositionId);
            var positioSup = positionServices.GetPositionByID(hierarchyPosition.PositionSupID);
            var po = positionServices.GetPositionByID(hierarchyPosition.PositionID);
            hierarchyPosition.PositionSup = positioSup;
            hierarchyPosition.Position = po;
            hierarchyPosition.companyID = positioSup.companyID;
            return StatusCode(200, hierarchyPosition);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Positions)]
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] HierarchyPosition hierarchyPosition)
        {
            var hierarchyPos = new HierarchyPosition();
            try
            {
                var thp = TypeHierarchyPositionsServices.GetTypeHierarchyPositionByID(hierarchyPosition.TypeHierarchyPositionID);
                var hp = positionServices.GetPositionByID(hierarchyPosition.PositionSupID);
                var p = positionServices.GetPositionByID(hierarchyPosition.PositionID);
                hierarchyPosition.companyID = p.companyID;
                hierarchyPosition.PositionSup = hp;
                hierarchyPosition.TypeHierarchyPosition = thp;
                hierarchyPosition.Position = p;
                hierarchyPos = hierarchyPositionServices.Create(hierarchyPosition);
                if (hierarchyPos == null)
                {
                    return StatusCode(400, "FailAddHierarchyPosition");
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }

            return StatusCode(200, hierarchyPos);
        }

       // [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Positions)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] HierarchyPosition hierarchyPosition)
        {
            try
            {
                var HierarchyPosition = hierarchyPositionServices.GetHierarchyPositionByID(id);
                HierarchyPosition.PositionID = hierarchyPosition.PositionID;
                HierarchyPosition.PositionSupID = hierarchyPosition.PositionSupID;
                HierarchyPosition.TypeHierarchyPosition = null;
                HierarchyPosition.TypeHierarchyPositionID = hierarchyPosition.TypeHierarchyPositionID;
                var mess = hierarchyPositionServices.Edit(HierarchyPosition);
                if (mess == "no")
                {
                    return StatusCode(400, "Probléme lors de modification d'hierarchy de poste");
                }
                return StatusCode(200, HierarchyPosition);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Positions)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var mess = hierarchyPositionServices.Delete(id);
                if (mess == null)
                {
                    return StatusCode(400, "FailDeleteHierarchyPosition");
                }
                return StatusCode(200, mess);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

    //    [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetAllPositionByCompany")]
        public IActionResult GetAllPositionByCompany(int companyId)
        {
            var ListPositionByCompany = positionServices.GetAllPositionByCompanyId(companyId);
            return StatusCode(200, ListPositionByCompany);
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetAllPositionVMByCompany")]
        public IActionResult GetAllPositionVMByCompany(int companyId)
        {
            var ListPositionByCompany = positionServices.GetAllPositionByCompanyIdVM(companyId);
            return StatusCode(200, ListPositionByCompany);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetAllPositionByCompanyWithoutSamePosition")]
        public IActionResult GetAllPositionByCompanyWithoutSamePosition(int companyId, int pos)
        {
            var posWithoutSupAndCurrent = new List<int>();
            var positioSup = new Position();
            var listHierarchyPosition = hierarchyPositionServices.GetHierarchyPositionByPositionId(pos);
            foreach (var hp in listHierarchyPosition)
            {
                positioSup = positionServices.GetPositionByID(hp.PositionSupID);
                posWithoutSupAndCurrent.Add(positioSup.Id);
            }
            var posCurrent = positionServices.GetPositionByID(pos);
            posWithoutSupAndCurrent.Add(posCurrent.Id);
            var ListPositionByCompany = positionServices.GetAllExcludingSpecificPositions(posWithoutSupAndCurrent, companyId);
            return StatusCode(200, ListPositionByCompany);
        }
    }
}
