using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Domain.Enum
{
    //class Inn4Enum
    //{
    //}
    public enum StatusDelegation
    {
        programmer, encours, annuler
    }
    public enum StatusDocument
    {
        preparer, soumetre, valider, refuser, annuler, renvoyer, comptabiliser, abondonner
    }
    public enum RhStatus
    {
        [Display(Description = "Prepared")]
        Preparer,
        [Display(Description = "Validated")]
        Valider
    }
    public enum RhSens
    {
        Droit, Titre
    }
    public enum TypeCongePeriod
    {
        [Display(Description = "Year")]
        Year,
        [Display(Description = "Month")]
        Month,
        [Display(Description = "Infinite")]
        Infinite
    }
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
