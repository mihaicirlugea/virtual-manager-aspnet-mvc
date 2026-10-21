using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAW_PROJECT.Models;

namespace PAW_PROJECT.Data
{
    // CORECTAT: Moștenirea folosește acum ApplicationUser în loc de IdentityUser
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ecosistem> Ecosisteme { get; set; }
        public DbSet<Activitate> Activitati { get; set; }
        public DbSet<FluxCapital> FluxuriCapitale { get; set; }
        public DbSet<ObiectivIntermediar> ObiectiveIntermediare { get; set; }
        public DbSet<Risipa> Risipe { get; set; }
        public DbSet<Eticheta> Etichete { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Obligatoriu primul apel pentru configurarea tabelelor Identity
            base.OnModelCreating(modelBuilder);

            // Mapează explicit clasa extinsă pe tabela de utilizatori
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            // Configurare precizie decimal
            modelBuilder.Entity<Activitate>().Property(a => a.EfortOre).HasPrecision(18, 2);
            modelBuilder.Entity<FluxCapital>().Property(f => f.SumaRON).HasPrecision(18, 2);
            modelBuilder.Entity<ObiectivIntermediar>().Property(o => o.TintaAbsoluta).HasPrecision(18, 2);
            modelBuilder.Entity<ObiectivIntermediar>().Property(o => o.ProgresCurent).HasPrecision(18, 2);
            modelBuilder.Entity<Risipa>().Property(r => r.CostPierdere).HasPrecision(18, 2);
        }
    }
}