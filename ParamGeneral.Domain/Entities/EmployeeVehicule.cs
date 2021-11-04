using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class EmployeeVehicule : BaseModel
    {
        public string Marque { get; set; }
        public string Matricule { get; set; }
        public string PuissanceFiscale { get; set; }
        public Boolean Type { get; set; }
        public Boolean TiTulaireVehProf { get; set; }
        public string TypeVehicule { get; set; }
        public Boolean TiTulaireCarteEssence { get; set; }
        public float Plafond { get; set; }
        public DateTime ValiditeCarte { get; set; } = DateTime.Today;
        public DateTime DateDebut { get; set; } = DateTime.Today;
        public DateTime DateFin { get; set; } = DateTime.Today;
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
