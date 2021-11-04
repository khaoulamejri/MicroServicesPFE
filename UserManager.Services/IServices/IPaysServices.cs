using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
  public  interface IPaysServices
    {
        Pays GetPaysByID(int id);
        Pays Create(Pays Pays);
        Pays Edit(Pays Pays);
        Pays Delete(int PaysId);
        List<Pays> GetAllPays();
    }
}
