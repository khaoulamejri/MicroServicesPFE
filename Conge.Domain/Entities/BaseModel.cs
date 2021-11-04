using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
    public class BaseModel
    {
        [Key, Required]
        public int Id { get; set; }
        public string UserCreat { get; set; }
        public string UserModif { get; set; }
        public DateTime DateCreat { get; set; }
        public DateTime? DateModif { get; set; }
        public int companyID { get; set; }
        public string Display { get { return ToString(); } }
    }
}