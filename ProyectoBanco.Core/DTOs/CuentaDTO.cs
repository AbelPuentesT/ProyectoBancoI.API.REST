using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Core.DTOs;

public partial class CuentaDTO : EntidadBase
{
    public string CueNumero { get; set; } = null!;

    public int CliId { get; set; }

    public bool CueActiva { get; set; }

    public DateTime CueFechaCreacion { get; set; }

    public string CueUsuarioCreacion { get; set; } = null!;

    public decimal CueSaldoActual { get; set; }

   
}
