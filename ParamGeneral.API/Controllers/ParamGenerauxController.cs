using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Domain.Enum;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParamGeneral.API.Controllers
{
    [Route("api/[controller]")]
  //  [ApiController]
    public class ParamGenerauxController : ControllerBase
    {
        private readonly IParamGenerauxServices paramGenerauxServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITypeHierarchyPositionsServices typeHierarchyPositionsServices;


        public ParamGenerauxController(IParamGenerauxServices param, IHttpContextAccessor httpContextAccessor, ITypeHierarchyPositionsServices thp)
        {
            paramGenerauxServices = param;
            _httpContextAccessor = httpContextAccessor;
            typeHierarchyPositionsServices = thp;
        }

      //  [ClaimRequirement("Auth", "Authenticated")]
        [HttpGet, Route("GetParamGeneraux")]
        public IActionResult GetParamGeneraux()
        {
            var parmGeneraux = paramGenerauxServices.GetParamGeneraux();
            return StatusCode(200, parmGeneraux);
        }

      //  [ClaimRequirement("Privilege", ApiPrivileges.LeaveModule_Settings_Edit_LeaveOptions)]
        [HttpPut, Route("PUT")]
        public IActionResult PUT(int id, [FromBody] ParamGeneraux paramGeneraux)
        {
            var response = "";
            var paramG = new ParamGeneraux();
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                if (id == 0)
                {
                    if (paramGeneraux == null)
                    {
                        return StatusCode(400, "FailEmptyGeneralSettings");
                    }
                    if (paramGenerauxServices.CheckUnicityParamGeneraux(paramGeneraux.Id))
                    {
                        RhDays enumWeekEnd1 = (RhDays)paramGeneraux.WeekEnd1;
                        RhDays enumWeekEnd2 = (RhDays)paramGeneraux.WeekEnd2;
                        paramGeneraux.WeekEnd1 = enumWeekEnd1;
                        paramGeneraux.WeekEnd2 = enumWeekEnd2;
                        paramGeneraux.Souche = "PO0";
                        paramGeneraux.companyID = currentCompanyId;
                        var po = typeHierarchyPositionsServices.GetTypeHierarchyPositioByID(paramGeneraux.TpHierPosID);
                        paramGeneraux.TypeHierarchyPosition = po;
                        paramG = paramGenerauxServices.Create(paramGeneraux);
                        if (paramG == null)
                        {
                            return StatusCode(400, "FailCreateGeneralSettings");
                        }
                    }
                    response = "ParametresGenereaux ajoutés";
                }
                else
                {
                    if (paramGeneraux.WeekEnd1 == paramGeneraux.WeekEnd2)
                        throw new Exception("ParamGenerauxControl");

                    var param = paramGenerauxServices.GetParamGenerauxByID(id);
                    param.DebutAnneeBonifiation = paramGeneraux.DebutAnneeBonifiation;
                    param.FinAnneeBonifiation = paramGeneraux.FinAnneeBonifiation;
                    RhDays enumWeekEnd1 = (RhDays)paramGeneraux.WeekEnd1;
                    RhDays enumWeekEnd2 = (RhDays)paramGeneraux.WeekEnd2;
                    param.WeekEnd1 = enumWeekEnd1;
                    param.WeekEnd2 = enumWeekEnd2;
                    param.WeekEnd1_inclut = paramGeneraux.WeekEnd1_inclut;
                    param.WeekEnd2_inclut = paramGeneraux.WeekEnd2_inclut;
                    param.TpHierPosID = paramGeneraux.TpHierPosID;
                    param.Souche = paramGeneraux.Souche;
                    param.Remplacant_autre_company = paramGeneraux.Remplacant_autre_company;
                    param.AfficheJB = paramGeneraux.AfficheJB;
                    param.AfficheOU = paramGeneraux.AfficheOU;
                    paramG = paramGenerauxServices.Edit(param);
                    if (paramG == null)
                    {
                        throw new Exception("ParamGenerauxControl");
                    }
                    response = "ParametresGenereaux modifiés";
                }
                return StatusCode(200, paramG);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}