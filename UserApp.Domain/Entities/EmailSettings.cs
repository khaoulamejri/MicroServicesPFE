using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class EmailSettings
    {
        public String host { get; set; }

        public int PrimaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }

        public String FromEmail { get; set; }

        public String ToEmail { get; set; }

        public String CcEmail { get; set; }
        public bool defaultCredentials { get; set; }
        public bool enableSsl { get; set; } = false;
    }
}
