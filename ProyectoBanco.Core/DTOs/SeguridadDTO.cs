using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumerations;

namespace ProyectoBanco.Core.DTOs;

public partial class SeguridadDTO : EntidadBase
{
    public string SegUsuario { get; set; } = null!;

    public string SegNombreUsuario { get; set; } = null!;

    public string SegContrasena { get; set; } = null!;

    public RolEspecifico Rol { get; set; }
    public int ClienteID { get; set; }
}
