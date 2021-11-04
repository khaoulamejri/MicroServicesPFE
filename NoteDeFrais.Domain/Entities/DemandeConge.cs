using NoteDeFrais.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Entities
{
  public class DemandeConge
    {
        [Key, Required]
         public int Id { get; set; }
        [Required]
         public DateTime DateDebutConge { get; set; } = DateTime.Today;
        [Required]
         public DateTime DateRepriseConge { get; set; } = DateTime.Today;
         public StatusDocument Statut { get; set; }
         public int EmployeeIDConsumed { get; set; }
    }
}
