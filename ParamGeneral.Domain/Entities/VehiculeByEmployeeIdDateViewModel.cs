using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class VehiculeByEmployeeIdDateViewModel
    {
        public int id { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
    }
}
