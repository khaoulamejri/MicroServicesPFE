using Microsoft.EntityFrameworkCore;
using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Pays>(entity =>
        //    {
        //        entity.ToTable(name: "pay"); ;
        //entity.HasKey(t => new { t.Id});
        //  //entity.Property(e => e.Id).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //  dataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    });
        //}

        public DbSet<DemandeConge> DemandeConge { get; set; }
        public DbSet<CompteComptable> CompteComptable { get; set; }
        public DbSet<Depenses> Depenses { get; set; }
        public DbSet<Devise> Devise { get; set; }
        public DbSet<DocumentsDepenses> DocumentsDepenses { get; set; }
        public DbSet<DocumentsNoteFrais> DocumentsNoteFrais { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeGroupe> EmployeeGroupe { get; set; }
        public DbSet<FraisKilometriques> FraisKilometriques { get; set; }
        public DbSet<GroupeFrais> GroupeFrais { get; set; }
        public DbSet<GroupeFraisDepense> GroupeFraisDepense { get; set; }
        public DbSet<MoyenPaiement> MoyenPaiement { get; set; }
        public DbSet<NotesFrais> NotesFrais { get; set; }
        public DbSet<OrdreMission> OrdreMission { get; set; }
        public DbSet<Pays> pay { get; set; }
        //  public DbSet<Pays> Pays { get; set; }
        public DbSet<TypeDepense> TypeDepense { get; set; }
        public DbSet<TypeOrdreMission> TypeOrdreMission { get; set; }
        public DbSet<WFDocument> WFDocument { get; set; }
    }
}