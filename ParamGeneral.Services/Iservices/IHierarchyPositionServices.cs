using Microsoft.AspNetCore.Mvc.Rendering;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
 public interface IHierarchyPositionServices
    {
        List<HierarchyPosition> GetAllHierarchyPosition();
        HierarchyPosition GetHierarchyPositionByID(int id);
        List<HierarchyPosition> GetHierarchyPositionByPositionId(int id);
        HierarchyPosition Create(HierarchyPosition HierarchyPosition);
        string Edit(HierarchyPosition HierarchyPosition);
        HierarchyPosition Delete(int HierarchyPositionId);
        List<TypeHierarchyPosition> SelectListItemTypeHierarchyPositions();
        List<Position> SelectListItemPositions();
        List<SelectListItem> SelectListItemPositionsByCompany(int companyID);
        List<int> GetListPosInf(int PositionID, int type, List<int> test, bool SupInclut = true);
        List<int> GetListPosInfByCMPSelected(int PositionID, int type, List<int> test, bool SupInclut = true);
        List<Position> GetListPositionInferieur(int PositionID, int type, bool SupInclut = true);
        List<Position> GetListPositionInferieurByCMPSelected(int PositionID, int type, bool SupInclut = true);
        List<Position> GetListPositionInferieurWithoutCMP(int PositionID, int type, bool SupInclut = true);
    }
}
