using System.ComponentModel.DataAnnotations;

namespace Imobiliaria.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        public int AnundioId { get; set; }
        public int EstadoId { get; set; }
        public int BairroId { get; set; }
        public int CidadeId { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(150,ErrorMessage = "Digite 5 a 150 caracteres",MinimumLength = 5)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(8, ErrorMessage = "Digite 8 caracteres")]
        public string Cep { get; set; }

        public string Complemento { get; set; }
        public CompostoEndereco Composto { get; set; }

        public virtual Anuncio AnuncioEndereco { get; set; }
    }

    public class CompostoEndereco
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(3, ErrorMessage = "minimo de 3 caracteres")]
        public string TextBairro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(3, ErrorMessage = "minimo de 3 caracteres")]
        public string TextCidade { get; set; }
        public string TextEstado { get; set; }
    }
}