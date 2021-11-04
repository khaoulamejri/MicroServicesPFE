using Microsoft.AspNetCore.Mvc.Rendering;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
    public interface IUniteService
    {
        List<Unite> GetAllUnite();
        Unite GetUniteByID(int id);
        Unite Create(Unite Unite);
        Unite Edit(Unite Unite);
        Unite Delete(int UniteId);
     List<SelectListItem> SelectListUnite();
        bool CheckUnicityUnite(string code);
        bool CheckUnicityUniteByID(string code, int id);
    }
}
