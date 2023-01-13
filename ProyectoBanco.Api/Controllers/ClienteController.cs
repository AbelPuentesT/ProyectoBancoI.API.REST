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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IMapper _mapper;
        private readonly IUriServicio _uriService;
        public ClienteController(IClienteServicio clienteServicio, IMapper mapper, IUriServicio uriService)
        {
            _clienteServicio = clienteServicio;
            _mapper = mapper;
            _uriService = uriService;

        }

        // GET: api/Cliente
        //[Authorize]
        [HttpGet(Name = nameof(ConsultarTodosLosClientes))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ClienteDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<ClienteDTO>>))]
        public async Task<IActionResult> ConsultarTodosLosClientes([FromQuery] FiltroDinamicoCliente filtros)
        {
            var clientes = _clienteServicio.ConsultarTodosLosClientes(filtros);
            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
            var metadata = Metadata.Crear(
                clientes.PaginaActual,
                clientes.TotalPaginas,
                clientes.TamanoPagina,
                clientes.TotalElementos,
                clientes.TienePaginaPrevia,
                clientes.TienePaginaSiguiente,
                _uriService.ClientePaginacionUri(filtros, Url.RouteUrl(nameof(ConsultarTodosLosClientes))).ToString(),
                _uriService.ClientePaginacionUri(filtros, Url.RouteUrl(nameof(ConsultarTodosLosClientes))).ToString()
            );
            var respuesta = ApiResponse<IEnumerable<ClienteDTO>>.Create(clientesDTO, metadata);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(respuesta);
        }

        // GET: api/Cliente/int
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarCliente(int id)
        {
            var cliente = await _clienteServicio.ConsultarCliente(id);
            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            var respuesta = ApiResponse<ClienteDTO>.Create(clienteDTO);
            return Ok(respuesta);
        }

        // POST: api/Cliente
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpPost]
        public async Task<IActionResult> CrearCliente(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            await _clienteServicio.CrearCliente(cliente);
            var clienteDto = _mapper.Map<ClienteDTO>(cliente);
            var respuesta = ApiResponse<ClienteDTO>.Create(clienteDto);
            return Ok(respuesta);

        }

        // PUT: api/Cliente/int
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpPut]
        public async Task<IActionResult> ModificarCliente(int id, ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;
            var resultado = await _clienteServicio.ModificarCliente(cliente);
            var respuesta = ApiResponse<bool>.Create(resultado);
            return Ok(respuesta);
        }


        // DELETE: api/Cliente/int
        //[Authorize(Roles = nameof(RolEspecifico.Administrador))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var resultado = await _clienteServicio.EliminarCliente(id);
            var respuesta = ApiResponse<bool>.Create(resultado);
            return Ok(respuesta);
        }

    }
}