using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
  ApplicationUserRole, IdentityUserLogin<string>,
  IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
            //  Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, EF6Console.Migrations.Configuration>());
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
               entity.ToTable(name: "TUser");
                entity.HasKey(t => new { t.UserName });
                entity.Property(e => e.Id).HasColumnName("UserId");
                // entity.Property(e => e.ProfileId).HasColumnName("ProfileId");

                entity.HasMany(u => u.jwtKeys)
                .WithOne(k => k.User)
                .HasForeignKey(jw => jw.userId);
            });

            modelBuilder.Entity<RolesPrivileges>().ToTable("Inn4RolesPrivileges").HasKey(t => new { t.IdRole, t.Privilege });


            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserRoleCompanies>(entity =>
            {
                entity.ToTable(name: "Inn4UserRoleCompanies");
                entity.HasKey(t => new { t.UserId, t.RoleId, t.companyId });
                entity.Property(p => p.UserId).HasColumnName("UserId");

            });



        }

        public DbSet<RolesPrivileges> rolesPrivileges { get; set; }
        public DbSet<ApplicationUserRoleCompanies> UserRoleCompanies { get; set; }
       public new DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRole { get; set; }
   //   public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileComposant> ProfileComposant { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Composant> Composant { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Devise> devise { get; set; }
        public DbSet<Pays> pay { get; set; }


    }
}
