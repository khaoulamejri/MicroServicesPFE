using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities.ViewMpdel
{
public    class CompanyModel
    {
        [Key, Required]
        public int Id { get; set; }
        //public string UserCreat { get; set; }
        //public string UserModif { get; set; }
        //public DateTime DateCreat { get; set; }
        //public DateTime? DateModif { get; set; }
        //public string Display { get { return ToString(); } }
        [Required(ErrorMessageResourceType = typeof(Properties.Resources), ErrorMessageResourceName = "FieldRequired")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public string LegalStatus { get; set; }
        public string FiscalNumber { get; set; }
        public string TradeRegister { get; set; }

        public string Numero { get; set; }

        public string CodePostal { get; set; }

        public string Ville { get; set; }

        public string ComplementAdresse { get; set; }

    }
}
