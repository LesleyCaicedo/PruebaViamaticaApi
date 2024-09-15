using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CapaEntidades.Modelos;

namespace CapaRepositorio.Database;

public partial class PruebaViamaticaContext : DbContext
{
    public PruebaViamaticaContext()
    {
    }

    public PruebaViamaticaContext(DbContextOptions<PruebaViamaticaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Opcione> Opciones { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolOpcione> RolOpciones { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<Sesione> Sesiones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PruebaViamatica;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Opcione>(entity =>
        {
            entity.HasKey(e => e.IdOpcion).HasName("PK__Opciones__A914DF352C19CBB7");

            entity.Property(e => e.IdOpcion).HasColumnName("idOpcion");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__A478814165649902");

            entity.ToTable("Persona");

            // AGREGADO
            //entity.HasOne(p => p.Usuario).WithOne(u => u.Persona);
            //

            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F761407DAC8");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
        });

        modelBuilder.Entity<RolOpcione>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkIdOpcion).HasColumnName("FK_idOpcion");
            entity.Property(e => e.FkIdRol).HasColumnName("FK_idRol");

            entity.HasOne(d => d.FkIdOpcionNavigation).WithMany()
                .HasForeignKey(d => d.FkIdOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolOpcion__FK_id__571DF1D5");

            entity.HasOne(d => d.FkIdRolNavigation).WithMany()
                .HasForeignKey(d => d.FkIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolOpcion__FK_id__5629CD9C");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkIdRol).HasColumnName("FK_idRol");
            entity.Property(e => e.FkIdUsuario).HasColumnName("FK_idUsuario");

            entity.HasOne(d => d.FkIdRolNavigation).WithMany()
                .HasForeignKey(d => d.FkIdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolUsuari__FK_id__5165187F");

            entity.HasOne(d => d.FkIdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.FkIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolUsuari__FK_id__52593CB8");
        });

        modelBuilder.Entity<Sesione>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FkIdUsuario).HasColumnName("FK_idUsuario");

            entity.HasOne(d => d.FkIdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.FkIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sesiones__FK_idU__4D94879B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A62798B5AB");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.FkIdPersona).HasColumnName("FK_idPersona");
            entity.Property(e => e.SesionActiva)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Usuario1).HasColumnName("Usuario");

            entity.HasOne(d => d.FkIdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkIdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__FK_idP__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
