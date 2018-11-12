using System.ComponentModel.DataAnnotations;


namespace Imobiliaria.Models
{
    public class Email
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "E-Mail")]
        public string From { get; set; }

        public string To { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "O Assunto deve ter de 5 a 50 caracteres",MinimumLength = 5)]
        [Display(Name = "Assunto")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(1000, ErrorMessage = "O corpo deve ter de 20 a 1000 caracteres", MinimumLength = 20)]
        [Display(Name ="Menssagem")]
        public string Body { get; set; }

        public int CodigoAnuncio { get; set; }

    }
}