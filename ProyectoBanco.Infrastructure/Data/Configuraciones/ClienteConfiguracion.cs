using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Infrastructure.Data.Configuraciones
{
    public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Cli_Id");

            builder.Property(e => e.CliApellido1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Apellido1");

            builder.Property(e => e.CliApellido2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Apellido2");

            builder.Property(e => e.CliCelular)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Celular");

            builder.Property(e => e.CliCiudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Ciudad");

            builder.Property(e => e.CliDireccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Cli_Direccion");

            builder.Property(e => e.CliEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Email");

            builder.Property(e => e.CliIdentificacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Identificacion");

            builder.Property(e => e.CliNombre1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Nombre1");

            builder.Property(e => e.CliNombre2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cli_Nombre2");

            builder.HasOne(d => d.Seguridad)
                .WithOne(p => p.Cliente)
                .HasForeignKey<Seguridad>(d => d.ClienteID);

        }
    }
}
