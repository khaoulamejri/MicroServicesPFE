using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IPaysServices
    {

        Pays GetPaysByID(int id);
        public Pays GetPayByID(int? id);
        Pays Create(Pays Pays);
        Pays Edit(Pays Pays);
        Pays Delete(int PaysId);
       
    }
}
