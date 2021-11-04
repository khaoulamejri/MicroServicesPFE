using Conge.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.Iservices
{
    public interface ISeuilServices
    {
        List<Seuils> GetAllseuils();
        Seuils GetSeuilByID(int id);
        Seuils Create(Seuils Seuil);
        Seuils Edit(Seuils Seuil);
        Seuils Delete(int SeuilId);
        List<SelectListItem> SelectListseuils();
        bool checkUnicity(Seuils Seuil);

    }
}
