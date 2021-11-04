using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
    public interface IDeviseServices
    {
        Devise GetDeviseByID(int id);
        List<Devise> GetAllDevise();
        Devise Create(Devise devise);
        Devise Edit(Devise devise);
        Devise Delete(int deviseId);
        Devise GetDeviseByPays(int id);
        bool checkUnicity(Devise devise);
    
    }
}
