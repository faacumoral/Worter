using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Worter.DAO.Models
{
    public partial class WorterContext : DbContext
    {
        public WorterContext()
        {
        }

        public WorterContext(DbContextOptions<WorterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioCreacionNavigation)
                    .WithMany(p => p.InverseIdUsuarioCreacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioCreacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioCreacion");

                entity.HasOne(d => d.IdUsuarioModificacionNavigation)
                    .WithMany(p => p.InverseIdUsuarioModificacionNavigation)
                    .HasForeignKey(d => d.IdUsuarioModificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioModificacion");
            });
        }
    }
}
