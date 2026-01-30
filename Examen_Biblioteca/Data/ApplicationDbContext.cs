using Examen_Biblioteca.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examen_Biblioteca.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Libros> Libros { get; set; }

        public virtual DbSet<Miembros> Miembros { get; set; }

        public virtual DbSet<Prestamos> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Libros>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Libros__3214EC070DBBFF34");

                entity.Property(e => e.AnioPublicacion).HasColumnName("Anio_Publicacion");
                entity.Property(e => e.Autor).HasMaxLength(150);
                entity.Property(e => e.Genero).HasMaxLength(100);
                entity.Property(e => e.Titulo).HasMaxLength(200);
            });

            modelBuilder.Entity<Miembros>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Miembros__3214EC07270215ED");

                entity.Property(e => e.Apellido).HasMaxLength(150);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.FechaSuscripcion).HasColumnName("Fecha_Suscripcion");
                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<Prestamos>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Prestamo__3214EC07EC505DDC");

                entity.Property(e => e.FechaDevolucion).HasColumnName("Fecha_Devolucion");
                entity.Property(e => e.FechaPrestamo).HasColumnName("Fecha_Prestamo");

                entity.HasOne(d => d.Libro).WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.LibroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prestamos_Libros");

                entity.HasOne(d => d.Miembro).WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.MiembroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prestamos_Miembros");
            });

        }
    }

}
