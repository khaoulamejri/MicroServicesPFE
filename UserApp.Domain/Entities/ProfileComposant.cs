using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class ProfileComposant : BaseModel
    {
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int ComposantId { get; set; }
        public virtual Composant Composant { get; set; }
    }
}
