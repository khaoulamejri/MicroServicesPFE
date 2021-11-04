using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IFraisKilometriquesServices
    {
        FraisKilometriques GetFraisKilometriquesByID(int id);
        List<FraisKilometriques> GetAllFraisKilometriquesByNoteFrais(int id);
        List<FraisKilometriques> GetAllFraisKilometriques();
        FraisKilometriques Create(FraisKilometriques fraisKilometriques);
        FraisKilometriques Edit(FraisKilometriques fraisKilometriques);
        FraisKilometriques Delete(int fraisKilometriquesId);

    }
}
