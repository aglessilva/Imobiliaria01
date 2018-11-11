using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using Imobiliaria.Models;

namespace Imobiliaria.FluentApi
{
    public class EnderecoFluentApi : EntityTypeConfiguration<Endereco>
    {
        public EnderecoFluentApi()
        {
            ToTable("Enderecos");
            HasKey(p => p.Id);
            Property(p => p.Id)
               .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasRequired(p => p.AnuncioEndereco).WithMany(p => p.Enderecos).HasForeignKey(p => p.AnundioId);

            Property(p => p.BairroId)
                .IsRequired();

            Property(p => p.AnundioId)
                .IsRequired();

            Property(p => p.Cep)
                .IsRequired()
                .HasMaxLength(9);

            Property(p => p.CidadeId)
                .IsRequired();

            Property(p => p.Complemento)
                .HasMaxLength(200);

            Property(p => p.EstadoId)
                .IsRequired();

            Property(p => p.Logradouro)
                .IsRequired()
                .HasMaxLength(150);

            Property(p => p.Numero)
                .IsRequired();

            Ignore(p => p.Composto);

        }
    }
}