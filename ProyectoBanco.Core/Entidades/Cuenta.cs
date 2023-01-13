namespace ProyectoBanco.Core.Entidades;

public partial class Cuenta : EntidadBase
{
    public string CueNumero { get; set; } = null!;

    public int CliId { get; set; }

    public bool CueActiva { get; set; }

    public DateTime CueFechaCreacion { get; set; }

    public string CueUsuarioCreacion { get; set; } = null!;

    public decimal CueSaldoActual { get; set; }

    public virtual Cliente Cli { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
}
