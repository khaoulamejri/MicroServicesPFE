using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event
{
    public enum RhGendar
    {
        [Display(Description = "Woman")]
        Female,
        [Display(Description = "Man")]
        Male
    }
    public enum RhMaritalStatus
    {
        [Display(Description = "Single")]
        Célibataire,
        [Display(Description = "Married")]
        Marié,
        [Display(Description = "Divorced")]
        Divorcé,
        [Display(Description = "Widower")]
        Veuve
    }
}
