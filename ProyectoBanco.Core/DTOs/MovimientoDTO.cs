using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumeraciones;

namespace ProyectoBanco.Core.DTOs;

public partial class MovimientoDTO : EntidadBase
{
    public DateTime MovFecha { get; set; }

    public string MovOrigen { get; set; } = null!;

    public decimal MovValor { get; set; }

    public TipoMovimiento MovTipo { get; set; } 

    public int CliId { get; set; }

    public int CueId { get; set; }

    
}
