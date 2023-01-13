using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProyectoBanco.Core.DTOs;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Infrastructure.Interfaces;
using ProyectoBanco.Infrastructure.Repositorios;

namespace ProyectoBanco.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly ISeguridadServicio _repositorioSeguridad;
        private readonly IMapper _mapper;
        private readonly IContrasenaServicio _passwordService;

        public SeguridadController(ISeguridadServicio repositorioSeguridad, IMapper mapper, IContrasenaServicio passwordService)
        {
            _repositorioSeguridad = repositorioSeguridad;
            _mapper = mapper;
            _passwordService = passwordService;

        }

        // POST: api/Seguridad
        [HttpPost]
        public async Task<IActionResult> PostSucurity(SeguridadDTO seguridadDTO)
        {
            var seguridad = _mapper.Map<Seguridad>(seguridadDTO);
            seguridad.SegContrasena = _passwordService.Hash(seguridad.SegContrasena);
            await _repositorioSeguridad.RegistarCliente(seguridad);
            return Ok();
        }
    }
}