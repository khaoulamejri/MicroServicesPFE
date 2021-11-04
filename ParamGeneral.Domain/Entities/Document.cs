using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
