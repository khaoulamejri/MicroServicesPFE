using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class DocumentsDepenses
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public int DepensesId { get; set; }
        public Depenses Depenses { get; set; }
    }
}
