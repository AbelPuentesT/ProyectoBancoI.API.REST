using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoBanco.Core.ConsultasDinamicas;
using ProyectoBanco.Core.DTOs;
using ProyectoBanco.Core.Entidades;
using ProyectoBanco.Core.Enumerations;
using ProyectoBanco.Core.Interfaces;
using ProyectoBanco.Core.OpcionesEntidades;
using ProyectoBanco.Infrastructure.Interfaces;
using ProyectoBanco.Respuestas;
using System.Net;

namespace ProyectoBanco.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoServicio _movimientoServicio;
        private readonly IMapper _mapper;
        private readonly IUriServicio _uriService;
        public MovimientoController(IMovimientoServicio movimientoServicio, IMapper mapper, IUriServicio uriService)
        {
            _movimientoServicio = movimientoServicio;
            _mapper = mapper;
            _uriService = uriService;

        }

        // GET: api/Movimiento
        //[Authorize]
        [HttpGet(Name = nameof(ConsultarTodosLosMovimientos))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<MovimientoDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<MovimientoDTO>>))]
        public async Task<IActionResult> ConsultarTodosLosMovimientos([FromQuery] FiltroDinamicoMovimiento filtros)
        {
            var movimientos = _movimientoServicio.ConsultarTodosLosMovimientos(filtros);
            var movimientosDTO = _mapper.Map<IEnumerable<MovimientoDTO>>(movimientos);
            var metadata = Metadata.Crear(
                movimientos.PaginaActual,
                movimientos.TotalPaginas,
                movimientos.TamanoPagina,
                movimientos.TotalElementos,
                movimientos.TienePaginaPrevia,
                movimientos.TienePaginaSiguiente,
                _uriService.MovimientoPaginacionUri(filtros, Url.RouteUrl(nameof(ConsultarTodosLosMovimientos))).ToString(),
                _uriService.MovimientoPaginacionUri(filtros, Url.RouteUrl(nameof(ConsultarTodosLosMovimientos))).ToString()
            );
            var respuesta = ApiResponse<IEnumerable<MovimientoDTO>>.Create(movimientosDTO, metadata);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(respuesta);
        }

        // GET: api/Movimiento/int
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultaMovimiento(int id)
        {
            var movimiento = await _movimientoServicio.ConsultaMovimiento(id);
            var movimientoDTO = _mapper.Map<MovimientoDTO>(movimiento);
            var respuesta = ApiResponse<MovimientoDTO>.Create(movimientoDTO);
            return Ok(respuesta);
        }

        // POST: api/Movimiento
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpPost]
        public async Task<IActionResult> ModificarMovimiento(MovimientoDTO movimientoDTO)
        {
            var movimiento = _mapper.Map<Movimiento>(movimientoDTO);
            await _movimientoServicio.ModificarMovimiento(movimiento);
            var movimientoDto = _mapper.Map<MovimientoDTO>(movimiento);
            var respuesta = ApiResponse<MovimientoDTO>.Create(movimientoDto);
            return Ok(respuesta);

        }

        // PUT: api/Movimiento/int
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpPut]
        public async Task<IActionResult> ModificarMovimiento(int id, MovimientoDTO movimientoDTO)
        {
            var movimiento = _mapper.Map<Movimiento>(movimientoDTO);
            movimiento.Id = id;
            var resultado = await _movimientoServicio.ModificarMovimiento(movimiento);
            var respuesta = ApiResponse<bool>.Create(resultado);
            return Ok(respuesta);
        }


        // DELETE: api/Movimiento/int
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMovimiento(int id)
        {
            var resultado = await _movimientoServicio.EliminarMovimiento(id);
            var respuesta = ApiResponse<bool>.Create(resultado);
            return Ok(respuesta);
        }

    }
}