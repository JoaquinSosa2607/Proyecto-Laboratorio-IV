using Microsoft.EntityFrameworkCore;
using SosD.Models;
using SosD.ViewModels;

namespace SosD.Repos
{
    public partial class SosDContext : DbContext
    {
        public SosDContext()
        {
        }

        public SosDContext(DbContextOptions<SosDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TipoTela> TipoTelas { get; set; }
        public virtual DbSet<TipoPrenda> TipoPrendas { get; set; }
        public virtual DbSet<Diseño> Diseño { get; set; }
        public virtual DbSet<Presupuesto> Presupuestos { get; set; }
        public virtual DbSet<MedioPago> MedioPagos { get; set; }
        public virtual DbSet<Promociones> Promociones { get; set; }
        public DbSet<PresupuestoViewModel> PresupuestoViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Presupuesto>()
                .HasOne(p => p.Diseño)
                .WithMany()
                .HasForeignKey(p => p.DiseñoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Presupuesto>()
                .HasOne(p => p.TipoPrenda)
                .WithMany()
                .HasForeignKey(p => p.TipoPrendaId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }




    }
}



