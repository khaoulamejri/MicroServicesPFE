using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface ITypeOrdreMissionServices : IDisposable
    {
        TypeOrdreMission GetTypeOrdreMissionById(int id);
        List<TypeOrdreMission> GetAllTypeOrdreMission();
        TypeOrdreMission Create(TypeOrdreMission typeOrdreMission);
        TypeOrdreMission Edit(TypeOrdreMission typeOrdreMission);
        TypeOrdreMission Delete(TypeOrdreMission typeOrdreMission);
        bool checkUnicity(TypeOrdreMission typeOrdreMission);
    }
}
