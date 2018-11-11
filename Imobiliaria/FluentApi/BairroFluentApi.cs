using System.Data.Entity.ModelConfiguration;
using Imobiliaria.Models;

namespace Imobiliaria.FluentApi
{
    public class BairroFluentApi : EntityTypeConfiguration<Bairro>
    {
        public BairroFluentApi()
        {
            ToTable("Bairros");
            HasKey(p => p.Id);
            Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(p => p.Nome).IsRequired().HasMaxLength(50);
            Ignore(p => p.Composto);
        }
    }
}