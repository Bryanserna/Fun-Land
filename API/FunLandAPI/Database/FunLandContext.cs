using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FunLandAPI.Database;

public partial class FunLandContext : DbContext
{
    public FunLandContext()
    {
    }

    public FunLandContext(DbContextOptions<FunLandContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Clasificacion> Clasificacions { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=FunLand;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Carrito");

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany()
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Id_Producto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Id_Usuario");
        });

        modelBuilder.Entity<Clasificacion>(entity =>
        {
            entity.HasKey(e => e.IdClasificacion);

            entity.ToTable("Clasificacion");

            entity.Property(e => e.IdClasificacion).HasColumnName("Id_Clasificacion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero);

            entity.ToTable("Genero");

            entity.Property(e => e.IdGenero).HasColumnName("Id_Genero");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaLanzamiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Lanzamiento");
            entity.Property(e => e.IdClasificacion).HasColumnName("Id_Clasificacion");
            entity.Property(e => e.IdGenero).HasColumnName("Id_Genero");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Producto");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClasificacionNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdClasificacion)
                .HasConstraintName("FK_Id_Clasificacion");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK_Id_Genero");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview);

            entity.ToTable("Review");

            entity.Property(e => e.IdReview).HasColumnName("Id_review");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Review1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Review");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Id_ProductoR");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Id_UsuarioR");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Usuario");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta);

            entity.Property(e => e.IdVenta).HasColumnName("Id_Venta");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdProducto).HasColumnName("Id_Producto");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.PrecioU).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Id_ProductoV");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Id_UsuarioV");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
