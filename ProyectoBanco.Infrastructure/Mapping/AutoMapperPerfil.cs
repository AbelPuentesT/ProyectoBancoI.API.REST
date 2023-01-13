using AutoMapper;
using ProyectoBanco.Core.DTOs;
using ProyectoBanco.Core.Entidades;

namespace ProyectoBanco.Infrastructure.Mapping
{
    public class AutoMapperPerfil: Profile
    {
        public AutoMapperPerfil()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Movimiento, MovimientoDTO>().ReverseMap();
            CreateMap<Cuenta, CuentaDTO>().ReverseMap();
            CreateMap<Seguridad, SeguridadDTO>().ReverseMap();
        }
    }
}
