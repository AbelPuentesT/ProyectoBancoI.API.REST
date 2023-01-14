using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
using System.Data;
using System.Net;

namespace ProyectoBanco.Api.Controllers
{
    [Authorize(Roles = nameof(RolEspecifico.Administrador))]
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaServicio _cuentaServicio;
        private readonly IMapper _mapper;
        private readonly IUriServicio _uriService;
        public CuentaController(ICuentaServicio cuentaServicio, IMapper mapper, IUriServicio uriService)
        {
            _cuentaServicio = cuentaServicio;
            _mapper = mapper;
            _uriService = uriService;

        }

        // GET: api/Cuenta
        [HttpGet(Name = nameof(ConsultarTodasLasCuentas))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CuentaDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CuentaDTO>>))]
        public async Task<IActionResult> ConsultarTodasLasCuentas([FromQuery] FiltroDinamicoCuenta filtros)
        {
            var cuentas = _cuentaServicio.ConsultarTodasLasCuentas(filtros);
            var cuentasDTO = _mapper.Map<IEnumerable<CuentaDTO>>(cuentas);
            var metadata = Metadata.Crear(
                cuentas.PaginaActual,
                cuentas.TotalPaginas,
                cuentas.TamanoPagina,
                cuentas.TotalElementos,
                cuentas.TienePaginaPrevia,
                cuentas.TienePaginaSiguiente,
                _uriService.CuentaPaginacionUri(filtros, Url.RouteUrl(nameof(ConsultarTodasLasCuentas))).ToString(),
                _uriService.CuentaPaginacionUri(filtros, Url.RouteUrl(nameof(ConsultarTodasLasCuentas))).ToString()
            );
            var respuesta = ApiResponse<IEnumerable<CuentaDTO>>.Create(cuentasDTO, metadata);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(respuesta);
        }

        // GET: api/Cuenta/int
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarCuenta(int id)
        {
            var cuenta = await _cuentaServicio.ConsultarCuenta(id);
            var cuentaDTO = _mapper.Map<CuentaDTO>(cuenta);
            var respuesta = ApiResponse<CuentaDTO>.Create(cuentaDTO);
            return Ok(respuesta);
        }

        // POST: api/Cuenta
        [HttpPost]
        public async Task<IActionResult> CrearCuenta(CuentaDTO cuentaDTO)
        {
            var cuenta = _mapper.Map<Cuenta>(cuentaDTO);
            await _cuentaServicio.CrearCuenta(cuenta);
            var cuentaDto = _mapper.Map<CuentaDTO>(cuenta);
            var respuesta = ApiResponse<CuentaDTO>.Create(cuentaDto);
            return Ok(respuesta);

        }

        // PUT: api/Cuenta/int
        [HttpPut]
        public async Task<IActionResult> ModificarCuenta(int id, CuentaDTO cuentaDTO)
        {
            var cuenta = _mapper.Map<Cuenta>(cuentaDTO);
            cuenta.Id = id;
            var resultado = await _cuentaServicio.ModificarCuenta(cuenta);
            var respuesta = ApiResponse<bool>.Create(resultado);
            return Ok(respuesta);
        }


        // DELETE: api/Cuenta/int
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCuenta(int id)
        {
            var resultado = await _cuentaServicio.EliminarCuenta(id);
            var respuesta = ApiResponse<bool>.Create(resultado);
            return Ok(respuesta);
        }

    }
}