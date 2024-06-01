using BarberiAppNegocio.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiAppNegocio.Models
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
        public virtual DbSet<Servicio>? Servicio { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("Cita");
                entity.Property(e => e.CitaID).HasColumnName("Id");
                entity.Property(e => e.Fecha).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.Hora).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.Estado).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.ClienteId).HasColumnName("Cliente_id").HasMaxLength(5).IsUnicode(false);
                entity.Property(e => e.BarberiaId).HasColumnName("Barberia_id").HasMaxLength(5).IsUnicode(false);
            });

            modelBuilder.Entity<Barberia>(entity =>
            {
                entity.ToTable("Barberia");
                entity.Property(e => e.BarberiaID).HasColumnName("Id");
                entity.Property(e => e.Nombre).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Direccion).HasMaxLength(255).IsUnicode(false);
                entity.Property(e => e.Telefono).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.ServicioBarberiaId).HasColumnName("servicio_barberia_id");
                entity.Property(e => e.ProductoBarberiaId).HasColumnName("producto_barberia_id").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.UsuarioAdminNegocio).HasColumnName("usuario_admin_negocio").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.EmpBarberiaId).HasColumnName("emp_barberia_id").HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.AdminPlatId).HasColumnName("admin_plat_id").HasMaxLength(4).IsUnicode(false);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.ToTable("Servicio");
                entity.Property(e => e.ServicioID).HasColumnName("Id");
                entity.Property(e => e.Fecha).HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.Estado).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Tipo).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Estado).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Precio).HasMaxLength(20).IsUnicode(false);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}