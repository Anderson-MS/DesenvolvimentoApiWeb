using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAdecco.Models;

namespace TesteAdecco.Data.Map
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteModel>
    {
        public void Configure(EntityTypeBuilder<ClienteModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.CPF).IsRequired().HasMaxLength(15);
            builder.Property(x => x.RG).IsRequired().HasMaxLength(12);

        }
    }
}
