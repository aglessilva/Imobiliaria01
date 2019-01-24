using System.Data.Entity.ModelConfiguration;
using Imobiliaria.Models;

namespace Imobiliaria.FluentApi
{
    public class TipoImovelFluentApi : EntityTypeConfiguration<TipoImovel>
    {
        public TipoImovelFluentApi()
        {
            ToTable("TipoImoveis");
            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);

           // Property(p => p.Ativo);
        }
    }
}