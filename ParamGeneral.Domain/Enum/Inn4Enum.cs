using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Enum
{
    public enum RhGendar
    {
        [Display(Description = "Woman", ResourceType = typeof(Properties.Resources))]
        Female,
        [Display(Description = "Man", ResourceType = typeof(Properties.Resources))]
        Male
    }
    public enum RhMaritalStatus
    {
        [Display(Description = "Single", ResourceType = typeof(Properties.Resources))]
        Célibataire,
        [Display(Description = "Married", ResourceType = typeof(Properties.Resources))]
        Marié,
        [Display(Description = "Divorced", ResourceType = typeof(Properties.Resources))]
        Divorcé,
        [Display(Description = "Widower", ResourceType = typeof(Properties.Resources))]
        Veuve
    }
    public enum RhDays
    {
        [Display(Description = "Sunday", ResourceType = typeof(Properties.Resources))]
        Dimanche,
        [Display(Description = "Monday", ResourceType = typeof(Properties.Resources))]
        Lundi,
        [Display(Description = "Tuesday", ResourceType = typeof(Properties.Resources))]
        Mardi,
        [Display(Description = "Wednesday", ResourceType = typeof(Properties.Resources))]
        Mercredi,
        [Display(Description = "Thursday", ResourceType = typeof(Properties.Resources))]
        Jeudi,
        [Display(Description = "Friday", ResourceType = typeof(Properties.Resources))]
        Vendredi,
        [Display(Description = "Saturday", ResourceType = typeof(Properties.Resources))]
        Samedi
    }
}
