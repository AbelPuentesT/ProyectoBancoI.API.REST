using ProyectoBanco.Core.Enumerations;

namespace ProyectoBanco.Core.Entidades;

public partial class Seguridad : EntidadBase
{
    public string SegUsuario { get; set; } = null!;

    public string SegNombreUsuario { get; set; } = null!;

    public string SegContrasena { get; set; } = null!;

    public RolEspecifico Rol { get; set; }
    public int ClienteID { get; set; }
    public virtual Cliente Cliente { get; set; } = null!;
}
