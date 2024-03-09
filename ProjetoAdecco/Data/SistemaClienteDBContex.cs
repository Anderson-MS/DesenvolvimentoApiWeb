using Microsoft.EntityFrameworkCore;
using TesteAdecco.Data.Map;
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
        //public DbSet<ContatoModel> Contatos { get; set; }
        //public DbSet<EnderecoMdel> Endereco { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new ClienteMap());
        //    modelBuilder.ApplyConfiguration(new ContatoMap());
        //    modelBuilder.ApplyConfiguration(new EnderecoMap());
                        
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
