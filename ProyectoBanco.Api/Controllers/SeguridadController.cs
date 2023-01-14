using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoBanco.Core.DTOs;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumerations;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Infrastructure.Interfaces;

namespace ProyectoBanco.Api.Controllers
{
    //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly ISeguridadServicio _repositorioSeguridad;
        private readonly IMapper _mapper;
        private readonly IContrasenaServicio _contrasenaServicio;

        public SeguridadController(ISeguridadServicio repositorioSeguridad, IMapper mapper, IContrasenaServicio contrasenaServicio)
        {
            _repositorioSeguridad = repositorioSeguridad;
            _mapper = mapper;
            _contrasenaServicio = contrasenaServicio;

        }

        // POST: api/Seguridad
        [HttpPost]
        public async Task<IActionResult> PostSucurity(SeguridadDTO seguridadDTO)
        {
            var seguridad = _mapper.Map<Seguridad>(seguridadDTO);
            seguridad.SegContrasena = _contrasenaServicio.Hash(seguridad.SegContrasena);
            await _repositorioSeguridad.RegistarCliente(seguridad);
            return Ok();
        }
    }
}