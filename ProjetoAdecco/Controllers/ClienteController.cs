using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteAdecco.Models;
using TesteAdecco.Repositorios.Interfaces;

namespace TesteAdecco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet("/cliente/listar")]
        public async Task<ActionResult<List<ClienteModel>>> BuscarTodosCliente()
        {
            List<ClienteModel> clientes = await _clienteRepositorio.BuscarTodosClientes();
            
            if (clientes.Count == 0)
            {
                return BadRequest("Nenhum cliente foi encontrado!");
            }
            return Ok(clientes);
        }       

        [HttpGet("/cliente/buscar")]
        public async Task<ActionResult<List<ClienteModel>>> BuscarClientesComFiltros(string nomeOuEmailOuCpf = null)
        {
            var clientes = await _clienteRepositorio.BuscarClientes(nomeOuEmailOuCpf);

            if (clientes == null || clientes.Count == 0)
            {
                return NotFound("Nenhum cliente encontrado com os filtros fornecidos.");
            }

            return Ok(clientes);
        }

        [HttpPost("/cliente/criar")]
        public async Task<ActionResult<ClienteModel>> Cadastrar([FromBody] ClienteModel clienteModel)
        {
            ClienteModel cliente = await _clienteRepositorio.Adicionar(clienteModel);   
            return Ok(cliente);
        }

        [HttpPut("/cliente/atualizar/{id}")]
        public async Task<ActionResult<List<ClienteModel>>> Atualizar([FromBody] ClienteModel clienteModel, int id)
        {
            clienteModel.Id = id;
            ClienteModel cliente = await _clienteRepositorio.Atualizar(clienteModel, id);           
            return Ok(cliente);
        }

        [HttpDelete("/cliente/remover/{id}")]
        public async Task<ActionResult<List<ClienteModel>>> Apagar(int id)
        {
            await _clienteRepositorio.Apagar(id);
            return NotFound("Cliente foi apagado!");
        }
    }
}
