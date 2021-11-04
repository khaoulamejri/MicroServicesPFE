using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class UserProfile : BaseModel
    {
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public string UserName { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
