using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Infrastructure.Data.Configuraciones
{
    public class CuentaConfiguracion : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.ToTable("Cuentas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Cue_Id");

            builder.Property(e => e.CliId).HasColumnName("Cli_Id");

            builder.Property(e => e.CueActiva).HasColumnName("Cue_Activa");

            builder.Property(e => e.CueFechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("Cue_FechaCreacion");

            builder.Property(e => e.CueNumero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cue_Numero");

            builder.Property(e => e.CueSaldoActual)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Cue_SaldoActual");

            builder.Property(e => e.CueUsuarioCreacion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cue_UsuarioCreacion");

            builder.HasOne(d => d.Cli).WithMany(p => p.Cuentas)
                .HasForeignKey(d => d.CliId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuentas_Clientes");

        }
    }
}
