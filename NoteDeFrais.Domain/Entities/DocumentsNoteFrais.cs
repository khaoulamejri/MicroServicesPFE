using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
    public class DocumentsNoteFrais
    {

        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
        public int NotesFraisId { get; set; }
        public NotesFrais NotesFrais { get; set; }

    }
}
