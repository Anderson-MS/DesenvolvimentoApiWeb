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
            return Ok(clientes);
        }

        [HttpPost("/cliente/criar")]
        public async Task<ActionResult<ClienteModel>> Cadastrar([FromBody] ClienteModel clienteModel)
        {
            ClienteModel cliente = await _clienteRepositorio.Adicionar(clienteModel);

            return Ok("Cliente Cadastrado com sucesso!");
        }

        [HttpPut("/cliente/atualizar/{id}")]
        public async Task<ActionResult<List<ClienteModel>>> Atualizar([FromBody] ClienteModel clienteModel, int id)
        {
            clienteModel.Id = id;
            ClienteModel cliente = await _clienteRepositorio.Atualizar(clienteModel, id);
            return Ok("Cliente Atualizado sucesso!");
        }

        [HttpDelete("/cliente/remover/{id}")]
        public async Task<ActionResult<List<ClienteModel>>> Apagar(int id)
        {
            try
            {
                await _clienteRepositorio.Apagar(id);
                return NotFound("Cliente foi apagado!");
            }
            catch (Exception ex)
            {               
                return BadRequest(ex.Message);
            }
        }

        
    }
}
