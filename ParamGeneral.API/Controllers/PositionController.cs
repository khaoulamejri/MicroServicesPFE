using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class PositionController : ControllerBase
    {
        private readonly IPositionServices positionServices;
        private readonly IHierarchyPositionServices hierarchyPositionServices;
        private readonly IEmploiServices _emploiServices;
        private readonly IUniteService UniteService;
        private IDepartementServices departementServices;

        public PositionController(IPositionServices pos, IHierarchyPositionServices h, IEmploiServices emploiServices, IUniteService uniteService, IDepartementServices departement)
        {
            positionServices = pos;
            hierarchyPositionServices = h;
            _emploiServices = emploiServices;
            UniteService = uniteService;
            departementServices = departement;
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetPositions")]
        public IActionResult GetPositions()
        {
           var ListPosition = new List<Position>();
            //var json = "";
            //if (DepartementId > 0)
            //{
            //    ListPosition = positionServices.GetPositionByDepartementID(DepartementId.Value);
            //}
            //else
            //{
                ListPosition = positionServices.GetAllPosition();
        //    }
            return StatusCode(200, ListPosition);
        }

     //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetAllPositionAllCompany")]
        public IActionResult GetAllPositionAllCompany(int id)
        {
            if (id > 0)
            {
                var ListPosition = new List<Position>();
                var json = "";
                var p = positionServices.GetPositionByID(id);
                var h = hierarchyPositionServices.GetHierarchyPositionByPositionId(id);

                foreach (var hp in h)
                {
                    var positioSup = new Position();
                    positioSup = positionServices.GetPositionByID(hp.PositionSupID);
                    ListPosition.Add(positioSup);
                }
                json = JsonConvert.SerializeObject(ListPosition, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                return StatusCode(200, ListPosition);
            }
            else
            {
                return StatusCode(400, "Not_Found");
            }
        }

      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetPositionByID")]
        public IActionResult GetPositionByID(int id, int? depId)
        {
            var Position = positionServices.GetPositionByID(id);
            return StatusCode(200, Position);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Read_Positions)]
        [HttpGet, Route("GetPositionByEmploiID")]
        public IActionResult GetPositionByEmploiID(int id)
        {
            var Position = positionServices.GetAllPositionByEmploiId(id);
            return StatusCode(200, Position);
        }

        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Positions)]
        //[HttpPost, Route("POST")]
        //public IActionResult POST([FromBody] Position Position)
        //{
        //    var poste = new Position();
        //    try
        //    {
        //        if (Position.DepartementID == 0 && Position.Intitule == "" && !Position.EmploiID.HasValue)
        //        {
        //            return StatusCode(400, String.Format("FieldRequired", "Department"));
        //        }
        //        if (Position.DepartementID == 0 && Position.Intitule != "" && Position.EmploiID.HasValue)
        //        {
        //            return StatusCode(400, String.Format("PositionDepartmentRequired", "Department"));
        //        }
        //        if (Position.DepartementID > 0 && Position.Intitule == "" && Position.EmploiID.HasValue)
        //        {
        //            return StatusCode(400, String.Format("PositionIntituleRequired", "Department"));
        //        }
        //        if (Position.DepartementID != 0 && Position.Intitule != "" && !Position.EmploiID.HasValue)
        //        {
        //            return StatusCode(400, String.Format("PositionEmploiRequired", "Department"));
        //        }
        //        if (Position.DepartementID > 0 && Position.Intitule != "" && Position.EmploiID.HasValue)
        //        {
        //            if (Position.UniteID == 0 || Position.UniteID == null)
        //            {
        //                var u = UniteService.GetAllUnite().FirstOrDefault();
        //                //Position.Unite = u;
        //                Position.UniteID = u.Id;
        //            }
        //            poste = positionServices.Creat(Position);
        //            if (poste == null)
        //            {
        //                return StatusCode(400, "CodeExist");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(400, e.Message);
        //    }
        //    return StatusCode(200, poste);
        //}
        [HttpPost, Route("POST")]
        public IActionResult POST([FromBody] Position Position)
        {
            var poste = new Position();
            try
            {
                if (Position.DepartementID == 0 && Position.Intitule == "" && !Position.EmploiID.HasValue)
                {
                    return StatusCode(400, String.Format("FieldRequired", "Department"));
                }
                if (Position.DepartementID == 0 && Position.Intitule != "" && Position.EmploiID.HasValue)
                {
                    return StatusCode(400, String.Format("PositionDepartmentRequired", "Department"));
                }
                if (Position.DepartementID > 0 && Position.Intitule == "" && Position.EmploiID.HasValue)
                {
                    return StatusCode(400, String.Format("PositionIntituleRequired", "Department"));
                }
                if (Position.DepartementID != 0 && Position.Intitule != "" && !Position.EmploiID.HasValue)
                {
                    return StatusCode(400, String.Format("PositionEmploiRequired", "Department"));
                }
                if (Position.DepartementID > 0 && Position.Intitule != "" && Position.EmploiID.HasValue)
                {
                    if (Position.UniteID == 0 || Position.UniteID == null)
                    {
                        var u = UniteService.GetAllUnite().FirstOrDefault();
                        //Position.Unite = u;
                        Position.UniteID = u.Id;
                    }
                    var a = Convert.ToInt32(Position.DepartementID);
                    var b = Convert.ToInt32(Position.EmploiID);
                    var c = Convert.ToInt32(Position.UniteID);
                    var dep = departementServices.GetDepartementByID(a);
                    var emplo = _emploiServices.GetEmploiById(b);
                    var unit = UniteService.GetUniteByID(c);
                    Position.Departement = dep;
                    Position.Emploi = emplo;
                    Position.Unite = unit;
                    poste = positionServices.Creat(Position);
                    if (poste == null)
                    {
                        return StatusCode(400, "CodeExist");
                    }
                }

            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, poste);
        }

        //   [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Positions)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] Position Position)
        {
            var poste = new Position();
            try
            {
                if (Position.DepartementID > 0)
                {
                    if (positionServices.CheckUnicityPositionById(Position.Intitule, Position.DepartementID, id))
                    {
                        var emploiChanged = false;
                        var PositionModified = positionServices.GetPositionIncludingEmploiById(id);
                        if (!PositionModified.EmploiID.Equals(Position.EmploiID)) emploiChanged = true;
                        if (emploiChanged)
                        {
                            var emploi = _emploiServices.GetEmploiById(Position.EmploiID.Value);
                            var PositionsWithSameRef = positionServices.GetAllPositionByReference(emploi.Reference);
                            var maxNumber = 1;
                            if (PositionsWithSameRef.Count > 0)
                            {
                                maxNumber = PositionsWithSameRef.Max(p => int.Parse(new String(p.Code.Where(Char.IsDigit).ToArray()))) + 1;
                            }
                            PositionModified.Code = emploi.Reference + maxNumber;
                        }

                        PositionModified.Intitule = Position.Intitule;
                        PositionModified.DepartementID = Position.DepartementID;
                        PositionModified.EmploiID = Position.EmploiID;
                        PositionModified.Emploi = Position.Emploi;

                        if (Position.UniteID == 0 || Position.UniteID == null)
                        {
                            var u = UniteService.GetAllUnite().FirstOrDefault();
                            PositionModified.Unite = u;
                            PositionModified.UniteID = u.Id;
                        }
                        positionServices.Edit(PositionModified);
                    }
                    else
                    {
                        return StatusCode(400, "CodeExist");
                    }
                }
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
            return StatusCode(200, poste);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.Settings_Edit_Positions)]
        [HttpDelete, Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var poste = positionServices.Delete(id);
                if (poste == null)
                {
                    return StatusCode(400, "FailDeletePosition");
                }
                return StatusCode(200, poste);
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
