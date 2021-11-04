using Conge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.ViewsModels
{
  public  class TypeCongeViewModel : BaseModel
    {
        public string Code { get; set; }
        public string Intitule { get; set; }
        public bool CongeAnnuel { get; set; }
        public float? NombreJours { get; set; }
        public int? NombreFois { get; set; }
        public int? Period { get; set; }
        public bool soldeNegatif { get; set; }
        public bool IsRemplacement { get; set; }
        public bool notification { get; set; }
        public string messageNotification { get; set; }
     
    }
}
