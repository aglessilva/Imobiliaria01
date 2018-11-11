using Imobiliaria.Models;
using System.Data.Entity.ModelConfiguration;

namespace Imobiliaria.FluentApi
{
    public class EstadoFluentApi : EntityTypeConfiguration<Estado>
    {
        public EstadoFluentApi()
        {
            ToTable("Estados")
                .HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(p => p.Nome)
                .HasMaxLength(50)
                .IsRequired();

            Property(p => p.Sigla)
              .HasMaxLength(50)
              .IsRequired();

            Property(p => p.Ativo);

        }
    }
}