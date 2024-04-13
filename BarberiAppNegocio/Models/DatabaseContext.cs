using BarberiApp.WebApi.Models;
using BarberiAppNegocio.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiApp.WebApi.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cita>? Cita { get; set; }
        public virtual DbSet<Barberia> Barberia { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("Cita");
                entity.Property(e => e.CitaID).HasColumnName("Id");
                entity.Property(e => e.Fecha).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.Hora).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.Estado).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.ClienteId).HasColumnName("cliente_id").HasMaxLength(5).IsUnicode(false);
            });

            modelBuilder.Entity<Barberia>(entity =>
            {
                entity.ToTable("Barberia");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Nombre).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Direccion).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Telefono).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.StockProducto).HasColumnName("StockProducto");
                entity.Property(e => e.CitaId).HasColumnName("CitaId").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.ServicioId).HasColumnName("ServicioId").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.ProductoId).HasColumnName("ProductoId").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.AdministradorNegocioId).HasColumnName("AdministradorNegocioId").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.EmpleadoBarberiaId).HasColumnName("EmpleadoBarberiaId").HasMaxLength(4).IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}