using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Enum
{
    public enum StatusDocument
    {
        preparer, soumetre, valider, refuser, annuler, renvoyer, comptabiliser, abondonner
    }
    public enum eWfAction { Attente, Approbation, Refus, Renvoi, Annulation }
    public enum TypeDemande
    {
        DemandeConge,
        NotesFrais,
        OrdreMission,
        TitreConge
    }
}
