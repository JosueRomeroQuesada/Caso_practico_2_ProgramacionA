using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TECHSTORE.Models;

public partial class TechstoreContext : DbContext
{
    public TechstoreContext()
    {
    }

    public TechstoreContext(DbContextOptions<TechstoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Vendedore> Vendedores { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.NombreCategoria).HasName("PK__Categori__01C83277DC95910A");

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_CATEGORIA");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__23A341307A4CF72F");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CORREO_ELECTRONICO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.NombreMarca).HasName("PK__Marcas__DF2EB891DE8AD128");

            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_MARCA");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.NombreProducto).HasName("PK__Producto__858319A5FCA19C3A");

            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PRODUCTO");
            entity.Property(e => e.IdCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID_CATEGORIA");
            entity.Property(e => e.IdMarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID_MARCA");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PRECIO");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__ID_CA__3D5E1FD2");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__ID_MA__3E52440B");
        });

        modelBuilder.Entity<Vendedore>(entity =>
        {
            entity.HasKey(e => e.NombreVendedor).HasName("PK__Vendedor__93A4D418923DE9FF");

            entity.Property(e => e.NombreVendedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_VENDEDOR");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__F3B6C1B4288AFFFE");

            entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("date")
                .HasColumnName("FECHA_VENTA");
            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PRODUCTO");
            entity.Property(e => e.NombreVendedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_VENDEDOR");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__ID_CLIEN__44FF419A");

            entity.HasOne(d => d.NombreProductoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.NombreProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__NOMBRE_P__440B1D61");

            entity.HasOne(d => d.NombreVendedorNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.NombreVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas__NOMBRE_V__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
