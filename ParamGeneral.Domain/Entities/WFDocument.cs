using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Domain.Entities
{
    public class WFDocument : BaseModel
    {
        public int Step { get; set; }
        public int Cycle { get; set; }

        public int Ordre { get; set; }
        public int Level { get; set; }
        public bool Finished { get; set; }

        public string Intitule { get; set; }

        public int RequestorId { get; set; }
        public Employee Requestor { get; set; }

        public int AffectedToId { get; set; }
        public Employee AffectedTo { get; set; }
        public int? RemplacantId { get; set; }
        public virtual Employee Remplacant { get; set; }
        public DateTime AffectedDate { get; set; } = DateTime.Today;
        //public int WorkflowID { get; set; }
        //[ForeignKey("WorkflowID")]
        //public virtual Workflow Workflow { get; set; }
        public string TypeDocument { get; set; }
        public int DocumentId { get; set; }
        public eWfAction Action { get; set; }
     //   [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ActionDate { get; set; }
        public string ActionComment { get; set; }

        public override string ToString()
        {
            return Intitule;
        }
        public enum eWfAction { Attente, Approbation, Refus, Renvoi, Annulation }
    }
}