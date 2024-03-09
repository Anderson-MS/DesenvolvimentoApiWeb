using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAdecco.Models;

namespace TesteAdecco.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Tipo).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DDD).IsRequired().HasMaxLength(2);
            builder.Property(x => x.Telefone).IsRequired().HasMaxLength(20);
        }
    }
}
