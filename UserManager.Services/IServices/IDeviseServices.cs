using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
   public interface IDeviseServices
    {
        Devise Create(Devise devise);
        Devise Edit(Devise devise);
        Devise Delete(int deviseId);
        Devise GetDeviseByID(int id);
        List<Devise> GetAllDevise();
    }
}
