using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface ITypeDepenseServices
    {
        TypeDepense GetTypeDepenseByIDIncluded(int id);
        TypeDepense GetTypeDepenseById(int id);
        List<TypeDepense> GetAllTypeDepense();
        TypeDepense Create(TypeDepense typeDepense);
        TypeDepense Edit(TypeDepense typeDepense);
        TypeDepense Delete(int typeDepenseId);
        bool checkUnicity(TypeDepense typeDepense, bool create);
    }
}
