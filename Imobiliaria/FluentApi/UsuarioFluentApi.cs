using System.Data.Entity.ModelConfiguration;
using Imobiliaria.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imobiliaria.FluentApi
{
    public class UsuarioFluentApi : EntityTypeConfiguration<Usuario>
    {
        public UsuarioFluentApi()
        {
            ToTable("Usuarios");
            HasKey(p => p.Id);

            Property(p => p.Id)
             .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(20);

            Property(p => p.Cpf)
                .IsRequired()
                .HasMaxLength(15);

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Senha)
                .IsRequired()
                .HasMaxLength(100);

            Property(p => p.Nome)
                  .IsRequired()
                  .HasMaxLength(50);

            Property(p => p.Ativo);

            Property(p => p.DataCriacao);

           
                
        }
    }
}