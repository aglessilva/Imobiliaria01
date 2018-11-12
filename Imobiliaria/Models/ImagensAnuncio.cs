using System.ComponentModel.DataAnnotations;

namespace Imobiliaria.Models
{
    public class ImagensAnuncio
    {
        public int Id { get; set; }
        [Display(Name = "Anúncio")]
        public int AnuncioId { get; set; }

        [Display(Name = "Descição")]
        [Required(ErrorMessage = "Insira uma descrição para foto")]
        [StringLength(50,ErrorMessage = "Digite entre 10 a 50 caracteres",MinimumLength = 10)]
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        [Display(Name = "Imagem")]
        public byte[] Path { get; set; }
        public virtual Anuncio AnuncioCasa { get; set; }

       
    }
}