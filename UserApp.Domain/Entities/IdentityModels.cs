using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserApp.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        [JsonIgnore]
        public ICollection<JwtKeys> jwtKeys { get; set; }
        [JsonIgnore]
        public ICollection<UserProfile> UserProfile { get; set; }

        public string Password { get; set; }

        public string Language { get; set; }
        public bool IsActif { get; set; }
        public int CompanyID { get; set; }
        [NotMapped]
        public string employe { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
    public class JwtKeys
    {
        public int Id { get; set; }
        public string jwtKey { get; set; }
        public string refreshToken { get; set; }
        public string userId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUserRoleCompanies
    {
        public int companyId { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }
}