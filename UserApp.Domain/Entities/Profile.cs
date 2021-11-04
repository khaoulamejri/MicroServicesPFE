using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class Profile : BaseModel
    {
        public string Intitule { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<ProfileComposant> profileComposants { get; set; }
        [JsonIgnore]
        public ICollection<UserProfile> UserProfile { get; set; }
    }
}
