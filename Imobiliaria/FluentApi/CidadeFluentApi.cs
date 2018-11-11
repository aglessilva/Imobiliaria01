using System.Data.Entity.ModelConfiguration;
using Imobiliaria.Models;

namespace Imobiliaria.FluentApi
{
    public class CidadeFluentApi : EntityTypeConfiguration<Cidade>
    {
        public CidadeFluentApi()
        {
            ToTable("Cidades")
            .HasKey(p => p.Id);
            Property(p => p.Id)
               .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasMany(P => P.BairrosCidades);
            Property(p => p.Ativo);
            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);
            
        }
    }
}