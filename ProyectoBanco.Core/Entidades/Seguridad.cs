using ProyectoBanco.Core.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoBanco.Core.Entidades;

public partial class Seguridad : EntidadBase
{
    public string SegUsuario { get; set; } = null!;

    public string SegNombreUsuario { get; set; } = null!;

    public string SegContrasena { get; set; } = null!;

    public RolEspecifico Rol { get; set; }
    public int ClienteID { get; set; }
    public Cliente Cliente { get; set; } = null!;
}
