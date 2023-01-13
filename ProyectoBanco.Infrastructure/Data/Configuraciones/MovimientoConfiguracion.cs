using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumeraciones;

namespace ProyectoBanco.Infrastructure.Data.Configuraciones
{
    public class MovimientoConfiguracion : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimientos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Mov_Id");

            builder.Property(e => e.CliId).HasColumnName("Cli_Id");

            builder.Property(e => e.CueId).HasColumnName("Cue_Id");

            builder.Property(e => e.MovFecha)
                .HasColumnType("datetime")
                .HasColumnName("Mov_Fecha");

            builder.Property(e => e.MovOrigen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Mov_Origen");

            builder.Property(e => e.MovTipo)
                .HasColumnName("Mov_Tipo")
                .IsRequired()
                .HasMaxLength(15)
                .HasConversion(
                x => x.ToString(),
                x => (TipoMovimiento)Enum.Parse(typeof(TipoMovimiento), x)
                ); 

            builder.Property(e => e.MovValor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Mov_Valor");

            builder.HasOne(d => d.Cli).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CliId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Clientes");

            builder.HasOne(d => d.Cue).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Cuentas");

        }
    }
}
