using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Entities
{
 public  class DocumentsConge
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public int DemandeCongeId { get; set; }
        public DemandeConge DemandeConge { get; set; }
    }
}
