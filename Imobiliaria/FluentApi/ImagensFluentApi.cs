using Imobiliaria.Models;
using System.Data.Entity.ModelConfiguration;

namespace Imobiliaria.FluentApi
{
    public class ImagensFluentApi :  EntityTypeConfiguration<ImagensAnuncio>
    {
        public ImagensFluentApi()
        {
            ToTable("Imagens");
            HasRequired(p => p.AnuncioCasa).WithMany(m => m.Imagens).HasForeignKey(k => k.AnuncioId);
            
            HasKey(p => p.Id);
            Property(p => p.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(p => p.AnuncioId).IsRequired();
            Property(p => p.Path).IsRequired();
            Property(p => p.Descricao).HasMaxLength(200);
            Property(p => p.Ativo);
        }
    }
}