
using Microsoft.EntityFrameworkCore;
using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Data
{
 public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasMany(s => s.WFDocumentReq).WithOne(a => a.Requestor).HasForeignKey(a => a.RequestorId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>().HasMany(s => s.WFDocumentRemplacant).WithOne(a => a.Remplacant).HasForeignKey(a => a.RemplacantId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>().HasMany(s => s.WFDocument).WithOne(a => a.AffectedTo).HasForeignKey(a => a.AffectedToId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Departement>().HasMany(s => s.Position).WithOne(a => a.Departement).IsRequired().OnDelete(DeleteBehavior.Restrict);

        }

        public DbSet<AffectationEmployee> affectationEmployee { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Position> position { get; set; }
        public DbSet<Departement> departements { get; set; }
        public DbSet<Emploi> emploi { get; set; }
        public DbSet<HierarchyPosition> hierarchyPosition { get; set; }
        public DbSet<ParamGeneraux> paramGeneraux { get; set; }
        public DbSet<RegimeTravail> regimeTravail { get; set; }
        public DbSet<TypeHierarchyPosition> typeHierarchyPosition { get; set; }
        public DbSet<Unite> unite { get; set; }
        public DbSet<Devise> devise { get; set; }
        public DbSet<Pays> pay { get; set; }
        public DbSet<Document> document { get; set; }
        public DbSet<EmployeeVehicule> employeeVehicules { get; set; }
        public DbSet<WFDocument> wFDocuments { get; set; }
    }
}
