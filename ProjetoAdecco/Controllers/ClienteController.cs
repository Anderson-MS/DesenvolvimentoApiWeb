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

        [HttpGet]
        public async Task<ActionResult<List<ClienteModel>>> BuscarTodosCliente()
        {
            List<ClienteModel> clientes = await _clienteRepositorio.BuscarTodosClientes();
            return Ok(clientes);
        }
                        

        [HttpPost]
        public async Task<ActionResult<ClienteModel>> Cadastrar([FromBody] ClienteModel clienteModel)
        {
            ClienteModel cliente = await _clienteRepositorio.Adicionar(clienteModel);

            return Ok("Cliente Cadastrado com sucesso!");
        }         

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ClienteModel>>> Apagar(int id)
        {
            try
            {
                await _clienteRepositorio.Apagar(id);
                return NotFound("Cliente foi apagado!"); 
            }
            catch (Exception ex)
            {
                // Trate exceções, como cliente não encontrado, aqui
                return BadRequest(ex.Message);
            }
        }
             
        [HttpPut]
        public async Task<ActionResult<List<ClienteModel>>> Atualizar([FromBody] ClienteModel clienteModel, int id)
        {
            clienteModel.Id = id;
            ClienteModel cliente = await _clienteRepositorio.Atualizar(clienteModel, id);
            // return Ok(cliente); 
            return Ok("Cliente Atualizado sucesso!");

        }
    }
}
