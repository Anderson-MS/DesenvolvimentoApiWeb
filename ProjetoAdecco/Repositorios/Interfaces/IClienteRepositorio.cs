using TesteAdecco.Models;

namespace TesteAdecco.Repositorios.Interfaces
{
    public interface IClienteRepositorio
    {        
        Task<List<ClienteModel>> BuscarTodosClientes();        
        Task<ClienteModel> Adicionar(ClienteModel cliente);           
        Task<ClienteModel> Atualizar(ClienteModel cliente, int id);       
        Task<ClienteModel> Apagar(int id);
        Task<ClienteModel> AtualizarPorId(int id, ClienteModel cliente);       
        Task<List<ClienteModel>> BuscarClientes(string nomeOuEmailOuCpf);
    }
}
