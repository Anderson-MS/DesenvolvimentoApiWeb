using Microsoft.EntityFrameworkCore;
using TesteAdecco.Models;

namespace TesteAdecco.Data
{
    public class SistemaClienteDBContex : DbContext
    {
        public SistemaClienteDBContex(DbContextOptions<SistemaClienteDBContex> options)
            : base(options)
        {                
        }

        public DbSet<ClienteModel> Clientes { get; set; }    
    }
}
