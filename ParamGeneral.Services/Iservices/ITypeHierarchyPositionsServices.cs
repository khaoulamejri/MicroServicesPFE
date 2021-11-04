using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
    public interface ITypeHierarchyPositionsServices
    {
        List<TypeHierarchyPosition> GetAllTypeHierarchyPosition();
        TypeHierarchyPosition GetTypeHierarchyPositionByID(int id);
        TypeHierarchyPosition GetTypeHierarchyPositioByID(int? id);
        TypeHierarchyPosition Create(TypeHierarchyPosition TypeHierarchyPosition);
        TypeHierarchyPosition Edit(TypeHierarchyPosition TypeHierarchyPosition);
        TypeHierarchyPosition Delete(int TypeHierarchyPositionId);
        bool CheckUnicityTypeHierarchyPosition(string code, int id);
    }
}
         