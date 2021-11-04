using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
    public interface IPaysServices
    {
        List<Pays> GetAllPays();
        Pays GetPaysByID(int id);
        Pays Create(Pays Pays);
        Pays Edit(Pays Pays);
        Pays Delete(int PaysId);
        bool CheckUnicityPays(string code);
        bool CheckUnicityPaysByID(string code, int id);
    }
}
