using BarberiApp.WebApi.Models;
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}