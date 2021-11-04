using Conge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
     
        public DbSet<Anciente> anciente { get; set; }
        public DbSet<Delegation> delegation { get; set; }
        public DbSet<DemandeConge> demandeConge { get; set; }
        public DbSet<Details_DroitConge> details_DroitConge { get; set; }
        public DbSet<DocumentsConge> documentsConge { get; set; }
        public DbSet<DroitConge> droitConge { get; set; }
        public DbSet<JoursFeries> joursFeries { get; set; }
        public DbSet<MvtConge> mvtConge { get; set; }
        public DbSet<PlanDroitConge> planDroitConge { get; set; }
        public DbSet<SoldeConge> soldeConge { get; set; }
        public DbSet<TitreConge> titreConge { get; set; }
        public DbSet<TypeConge> typeConge { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Seuils> seuils { get; set; }

    }
}
