using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Domain.Entities
{
    public class UserRolesViewModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
