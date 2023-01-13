using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumerations;

namespace ProyectoBanco.Infrastructure.Data.Configuraciones
{
    public class SeguridadConfiguracion : IEntityTypeConfiguration<Seguridad>
    {
        public void Configure(EntityTypeBuilder<Seguridad> builder)
        {
            builder.ToTable("Seguridad");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Seg_Id");

            builder.Property(e => e.Rol)
                .HasColumnName("Seg_Rol")
                .IsRequired()
                .HasMaxLength(15)
                .HasConversion(
                x => x.ToString(),
                x => (RolEspecifico)Enum.Parse(typeof(RolEspecifico), x)
                );  

            builder.Property(e => e.SegContrasena)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Seg_Contrasena")
                .IsRequired();

            builder.Property(e => e.SegNombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Seg_NombreUsuario")
                .IsRequired();

            builder.Property(e => e.SegUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Seg_Usu")
                .IsRequired();

            builder.Property(e => e.ClienteID)
                .HasColumnName("Seg_CliId");
        }
    }
}
