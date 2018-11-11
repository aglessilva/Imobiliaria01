using System.Data.Entity.ModelConfiguration;
using Imobiliaria.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imobiliaria.FluentApi
{
    public class AnuncioFluentApi : EntityTypeConfiguration<Anuncio>
    {
        public AnuncioFluentApi()
        {
            ToTable("Anuncios");

            HasMany(p => p.Imagens).WithRequired(p => p.AnuncioCasa).WillCascadeOnDelete(true);
            HasMany(p => p.Enderecos).WithRequired(p => p.AnuncioEndereco).WillCascadeOnDelete(true);

            Property(p => p.Id)
                    .HasColumnName("Id")
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasKey(p => p.Id);

            Property(p => p.Area)
                    .IsRequired()
                    .HasMaxLength(50);

            Property(p => p.Ativo);

            Property(p => p.DataAnuncio);

            Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            Property(p => p.Dorms)
                .IsRequired();

            Property(p => p.Suite)
                .IsRequired();

            Property(p => p.Tipo)
                .IsRequired();

            Property(p => p.TipoAnuncio).HasColumnName("TipoAnuncio")
                .IsRequired();

            Property(p => p.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Vagas);

            Property(p => p.Valor)
                .IsRequired();

            Property(p => p.IsLancamento);

        }
    }
}