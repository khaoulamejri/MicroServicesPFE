using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class DepenseGroupeVM
    {
        public string Intitule { get; set; }
        public int GroupeFraisID { get; set; }
        public int TypeDepenseID { get; set; }
        public float Plafond { get; set; }
        public float Forfait { get; set; }
    }
}
