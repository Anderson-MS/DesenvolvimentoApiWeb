using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAdecco.Models;

namespace TesteAdecco.Data.Map
{
    public class EnderecoMap : IEntityTypeConfiguration<EnderecoMdel>
    {
        public void Configure(EntityTypeBuilder<EnderecoMdel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TipoEndereco).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CEP).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Logradouro).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Numero).IsRequired().HasMaxLength(10);
            builder.Property(x => x.Bairro).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Complemento).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Cidade).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Estado).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Referencia).IsRequired().HasMaxLength(50);
        }
    }
}
