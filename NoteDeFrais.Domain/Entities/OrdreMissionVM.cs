using NoteDeFrais.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class OrdreMissionVM
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public StatusDocument Statut { get; set; }
        public int EmployeeIDConsumed  { get; set; }
      //  public virtual Employee Employee { get; set; }
        public int TypeMissionOrderId { get; set; }
        public int? PaysIdConsumed { get; set; }
        public string NumeroOM { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string EmployeeName { get; set; }

    }
}
