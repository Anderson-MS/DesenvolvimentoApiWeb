using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TesteAdecco.Data;
using TesteAdecco.Models;
using TesteAdecco.Repositorios.Interfaces;

namespace TesteAdecco.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly SistemaClienteDBContex _dbContext;
        public ClienteRepositorio(SistemaClienteDBContex sistemaClienteDBContex)
        {
            _dbContext = sistemaClienteDBContex;
        }
           
        public async Task<List<ClienteModel>> BuscarTodosClientes()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();

            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
            string query = "SELECT * FROM [TesteDB].[dbo].[Cliente]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(); 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync()) 
                            {
                                int clienteId = reader.GetInt32(0); 
                                string nomeCliente = reader.GetString(1); 
                                string EmailCliente = reader.GetString(2); 
                                string CPF = reader.GetString(3); 
                                string RG = reader.GetString(4);
                          
                                ClienteModel cliente = new ClienteModel
                                {
                                      Id = clienteId,
                                      Nome = nomeCliente,
                                      Email = EmailCliente,
                                      CPF = CPF,
                                      RG = RG,                                     
                                };

                                clientes.Add(cliente); 
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum cliente encontrado.");
                        }
                    }
                }
            }

            return clientes; 
        }

        public async Task<ClienteModel> Adicionar(ClienteModel novoCliente)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
            string queryCliente = @"
                INSERT INTO Cliente (Nome, Email, CPF, RG) 
                VALUES (@Nome, @Email, @CPF, @RG);
                SELECT SCOPE_IDENTITY();
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand commandCliente = new SqlCommand(queryCliente, connection))
                {
                    commandCliente.Parameters.AddWithValue("@Nome", novoCliente.Nome);
                    commandCliente.Parameters.AddWithValue("@Email", novoCliente.Email);
                    commandCliente.Parameters.AddWithValue("@CPF", novoCliente.CPF);
                    commandCliente.Parameters.AddWithValue("@RG", novoCliente.RG);
                    int novoClienteId = Convert.ToInt32(await commandCliente.ExecuteScalarAsync());
                    novoCliente.Id = novoClienteId;
                    string queryContato = @"
                        INSERT INTO Contato (Tipo, DDD, Telefone) 
                        VALUES (@Tipo, @DDD, @Telefone);
                        SELECT SCOPE_IDENTITY();
                    ";

                    using (SqlCommand commandContato = new SqlCommand(queryContato, connection))
                    {
                        commandContato.Parameters.AddWithValue("@Tipo", novoCliente.Tipo);
                        commandContato.Parameters.AddWithValue("@DDD", novoCliente.DDD);
                        commandContato.Parameters.AddWithValue("@Telefone", novoCliente.Telefone);
                        int novoContatoId = Convert.ToInt32(await commandContato.ExecuteScalarAsync());
                        novoCliente.Id = novoContatoId;
                    }


                    string queryEndereco = @"
                        INSERT INTO Endereco (Tipo, CEP, Logradouro, Numero, Bairro, Complemento, Cidade, Estado, Referencia) 
                        VALUES (@Tipo, @CEP, @Logradouro, @Numero, @Bairro, @Complemento, @Cidade, @Estado, @Referencia);
                        SELECT SCOPE_IDENTITY();
                    ";


                    using (SqlCommand commandEndereco = new SqlCommand(queryEndereco, connection))
                    {
                        commandEndereco.Parameters.AddWithValue("@Tipo", novoCliente.TipoEndereco);
                        commandEndereco.Parameters.AddWithValue("@CEP", novoCliente.CEP);
                        commandEndereco.Parameters.AddWithValue("@Logradouro", novoCliente.Logradouro);
                        commandEndereco.Parameters.AddWithValue("@Numero", novoCliente.Numero);
                        commandEndereco.Parameters.AddWithValue("@Bairro", novoCliente.Bairro);
                        commandEndereco.Parameters.AddWithValue("@Complemento", novoCliente.Complemento);
                        commandEndereco.Parameters.AddWithValue("@Cidade", novoCliente.Cidade);
                        commandEndereco.Parameters.AddWithValue("@Estado", novoCliente.Estado);
                        commandEndereco.Parameters.AddWithValue("@Referencia", novoCliente.Referencia);
                        int novoEnderecoId = Convert.ToInt32(await commandEndereco.ExecuteScalarAsync());
                        novoCliente.Id = novoEnderecoId;
                    }
                    return novoCliente;
                }
            }
        }
                
        public async Task<ClienteModel> Apagar(int id)
        {
            ClienteModel cliente = null;

            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
                        
            string query = @"
                    SELECT c.Id AS Id, c.Nome, c.Email AS EmailCliente, c.CPF, c.RG,
                           co.Id AS Id, co.Tipo AS TipoContato, co.DDD, co.Telefone,
                           e.Id AS Id, e.Tipo AS TipoEndereco, e.CEP, e.Logradouro, e.Numero, e.Bairro, e.Complemento, e.Cidade, e.Estado, e.Referencia
                    FROM Cliente c
                    LEFT JOIN Contato co ON c.Id = co.Id
                    LEFT JOIN Endereco e ON c.Id = e.Id
                    WHERE c.Id = @Id;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {                                
                                int clienteId = reader.GetInt32(reader.GetOrdinal("Id"));
                                string nomeCliente = reader.GetString(reader.GetOrdinal("Nome"));
                                string emailCliente = reader.GetString(reader.GetOrdinal("EmailCliente"));
                                string cpfCliente = reader.GetString(reader.GetOrdinal("CPF"));
                                string rgCliente = reader.GetString(reader.GetOrdinal("RG"));

                                cliente = new ClienteModel
                                {
                                    Id = clienteId,
                                    Nome = nomeCliente,
                                    Email = emailCliente,
                                    CPF = cpfCliente,
                                    RG = rgCliente
                                };

                                
                                int contatoId = reader.GetInt32(reader.GetOrdinal("Id"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                                {
                                    await ApagarPorId("Contato", contatoId);
                                }

                                
                                int enderecoId = reader.GetInt32(reader.GetOrdinal("Id"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                                {
                                    await ApagarPorId("Endereco", enderecoId);
                                }

                                
                                await ApagarPorId("Cliente", clienteId);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum cliente encontrado.");
                        }
                    }
                }
            }

            return cliente;
        }

        private async Task ApagarPorId(string tabela, int id)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
            string deleteQuery = $"DELETE FROM {tabela} WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
                      
        public async Task<ClienteModel> AtualizarPorId(int id , ClienteModel cliente)
        {        
            ClienteModel clientes = null;

            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";

            string query = @"
                    SELECT c.Id AS Id, c.Nome, c.Email AS EmailCliente, c.CPF, c.RG,
                           co.Id AS Id, co.Tipo AS TipoContato, co.DDD, co.Telefone,
                           e.Id AS Id, e.Tipo AS TipoEndereco, e.CEP, e.Logradouro, e.Numero, e.Bairro, e.Complemento, e.Cidade, e.Estado, e.Referencia
                    FROM Cliente c
                    LEFT JOIN Contato co ON c.Id = co.Id
                    LEFT JOIN Endereco e ON c.Id = e.Id
                    WHERE c.Id = @Id;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {                                
                                int clienteId = reader.GetInt32(reader.GetOrdinal("Id"));
                                string nomeCliente = reader.GetString(reader.GetOrdinal("Nome"));
                                string emailCliente = reader.GetString(reader.GetOrdinal("EmailCliente"));
                                string cpfCliente = reader.GetString(reader.GetOrdinal("CPF"));
                                string rgCliente = reader.GetString(reader.GetOrdinal("RG"));

                                clientes = new ClienteModel
                                {
                                    Id = clienteId,
                                    Nome = nomeCliente,
                                    Email = emailCliente,
                                    CPF = cpfCliente,
                                    RG = rgCliente
                                };

                                
                                int contatoId = reader.GetInt32(reader.GetOrdinal("Id"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                                {    
                                    await AtualizarContato(contatoId, cliente);
                                }

                                
                                int enderecoId = reader.GetInt32(reader.GetOrdinal("Id"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                                {
                                    await AtualizarEndereco(enderecoId, cliente);
                                }

                                
                                await AtualizarCliente(clienteId, cliente);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhum cliente encontrado.");
                        }
                    }
                }
            }

            return cliente;
        }

        private async Task AtualizarContato(int contatoId, ClienteModel contatoAtualizado)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
            string updateQuery = @"
                    UPDATE Contato
                    SET Tipo = @Tipo, DDD = @DDD, Telefone = @Telefone
                    WHERE Id = @Id;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", contatoId);
                    command.Parameters.AddWithValue("@Tipo", contatoAtualizado.Tipo);
                    command.Parameters.AddWithValue("@DDD", contatoAtualizado.DDD);
                    command.Parameters.AddWithValue("@Telefone", contatoAtualizado.Telefone);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task AtualizarEndereco(int enderecoId, ClienteModel enderecoAtualizado)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
            string updateQuery = @"
                UPDATE Endereco
                SET Tipo = @Tipo, CEP = @CEP, Logradouro = @Logradouro, Numero = @Numero,
                    Bairro = @Bairro, Complemento = @Complemento, Cidade = @Cidade, Estado = @Estado, Referencia = @Referencia
                WHERE Id = @Id;
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", enderecoId);
                    command.Parameters.AddWithValue("@Tipo", enderecoAtualizado.TipoEndereco);
                    command.Parameters.AddWithValue("@CEP", enderecoAtualizado.CEP);
                    command.Parameters.AddWithValue("@Logradouro", enderecoAtualizado.Logradouro);
                    command.Parameters.AddWithValue("@Numero", enderecoAtualizado.Numero);
                    command.Parameters.AddWithValue("@Bairro", enderecoAtualizado.Bairro);
                    command.Parameters.AddWithValue("@Complemento", enderecoAtualizado.Complemento);
                    command.Parameters.AddWithValue("@Cidade", enderecoAtualizado.Cidade);
                    command.Parameters.AddWithValue("@Estado", enderecoAtualizado.Estado);
                    command.Parameters.AddWithValue("@Referencia", enderecoAtualizado.Referencia);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task AtualizarCliente(int clienteId, ClienteModel clienteAtualizado)
        {
            string connectionString = "Server=localhost\\SQLEXPRESS;Database=TesteDB;Trusted_Connection=True;";
            string updateQuery = @"
                    UPDATE Cliente
                    SET Nome = @Nome, Email = @Email, CPF = @CPF, RG = @RG
                    WHERE Id = @Id;
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", clienteId);
                    command.Parameters.AddWithValue("@Nome", clienteAtualizado.Nome);
                    command.Parameters.AddWithValue("@Email", clienteAtualizado.Email);
                    command.Parameters.AddWithValue("@CPF", clienteAtualizado.CPF);
                    command.Parameters.AddWithValue("@RG", clienteAtualizado.RG);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<ClienteModel> Atualizar(ClienteModel cliente, int id)
        {
            ClienteModel clienteModel = await AtualizarPorId(id , cliente);

            if (clienteModel == null)
            {
                throw new Exception($"Usuario {id} não encontrado");
            }
            clienteModel.Nome = clienteModel.Nome;
            
            return clienteModel;
        } 
    }
}
