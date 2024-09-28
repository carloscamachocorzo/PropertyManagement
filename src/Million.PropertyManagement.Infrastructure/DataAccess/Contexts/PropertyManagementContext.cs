using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Infrastructure.DataAccess.Contexts
{
    public partial class PropertyManagementContext : DbContext
    {
        public PropertyManagementContext()
        {
        }

        public PropertyManagementContext(DbContextOptions<PropertyManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Owner> Owner { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<PropertyImage> PropertyImage { get; set; }
        public virtual DbSet<PropertyTrace> PropertyTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.IdOwner);

                entity.Property(e => e.IdOwner).HasComment("Identificador del registro de propietarios");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Direccion del propietario");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasComment("Fecha de cumpleaños del propietario");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Nombre Completo del propietario");

                entity.Property(e => e.Photo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Foto del propietario");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.IdProperty);

                entity.Property(e => e.IdProperty).HasComment("Identificador de la propiedad");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Direccion de la propiedad");

                entity.Property(e => e.CodeInternal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Codigo interno de la propiedad");

                entity.Property(e => e.IdOwner).HasComment("Id del propietario de la propiedad");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Nombre de la propiedad");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("Precio de la propiedad");

                entity.Property(e => e.Year).HasComment("Año de la propiedad");

                entity.HasOne(d => d.IdOwnerNavigation)
                    .WithMany(p => p.Property)
                    .HasForeignKey(d => d.IdOwner)
                    .HasConstraintName("FK_Property_Owner");
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.HasKey(e => e.IdPropertyImage);

                entity.Property(e => e.IdPropertyImage).HasComment("Identificador de la imagen de la propiedad");

                entity.Property(e => e.Enabled).HasComment("estado Habilitado de la propiedad");

                entity.Property(e => e.File).HasComment("Url de la imagen del archivo de la propiedad");

                entity.Property(e => e.IdProperty).HasComment("Id de la propiedad relacionada");

                entity.HasOne(d => d.IdPropertyNavigation)
                    .WithMany(p => p.PropertyImage)
                    .HasForeignKey(d => d.IdProperty)
                    .HasConstraintName("FK_PropertyImage_Property");
            });

            modelBuilder.Entity<PropertyTrace>(entity =>
            {
                entity.HasKey(e => e.IdPropertyTrace);

                entity.Property(e => e.IdPropertyTrace)
                    .ValueGeneratedOnAdd()
                    .HasComment("Identificador del Registro");

                entity.Property(e => e.DateSale)
                    .HasColumnType("date")
                    .HasComment("Fecha de Venta");

                entity.Property(e => e.IdProperty).HasComment("Identificador de la propiedad");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasComment("Nombre");

                entity.Property(e => e.Tax)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("Valor de impuesto");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("Valor");

                entity.HasOne(d => d.IdPropertyTraceNavigation)
                    .WithOne(p => p.PropertyTrace)
                    .HasForeignKey<PropertyTrace>(d => d.IdPropertyTrace)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyTrace_Property");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
