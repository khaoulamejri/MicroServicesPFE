using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class Composant : BaseModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Module { get; set; }
        public string RedirectURL { get; set; }
        [JsonIgnore]
        public ICollection<ProfileComposant> profileComposants { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PowerBIUrl { get; set; }

        public string Action { get; set; }

        public string Request { get; set; }
    }
}
