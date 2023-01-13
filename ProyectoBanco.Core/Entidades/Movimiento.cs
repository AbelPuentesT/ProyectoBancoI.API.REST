using ProyectoBanco.Core.Enumeraciones;

namespace ProyectoBanco.Core.Entidades;

public partial class Movimiento : EntidadBase
{
    public DateTime MovFecha { get; set; }

    public string MovOrigen { get; set; } = null!;

    public decimal MovValor { get; set; }

    public TipoMovimiento MovTipo { get; set; }

    public int CliId { get; set; }

    public int CueId { get; set; }

    public virtual Cliente Cli { get; set; } = null!;

    public virtual Cuenta Cue { get; set; } = null!;
}
