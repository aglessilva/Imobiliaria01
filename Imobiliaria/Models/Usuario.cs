using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Imobiliaria.Models
{
    public static class SessionContext
    {
        //Aqui não da para usar o TempData o que não é legal
        /// <summary>
        /// Usuário logado na aplicação.
        /// </summary>
        public static string Login
        {
            get
            {
                return (string)HttpContext.Current.Session["Usuario"];
            }
            set
            {
               HttpContext.Current.Session["Usuario"] = (string)value;
            }
        }

        public static string IsAutenticado
        {
            get
            {
                return (string)HttpContext.Current.Session["isAutenticado"];
            }
            set
            {
                HttpContext.Current.Session["isAutenticado"] = (string)value;
            }
        }
    }

    public class Usuario
    {
        public int Id { get; set; }



        [Required(ErrorMessage = "Digite seu nome")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "permitido de 5 a 50 caracteres", MinimumLength = 5)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite um nome de usuario")]
        [Display(Name = "Login")]
        [StringLength(10, ErrorMessage = "permitido de 5 a 10 caracteres", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Digite uma senha")]
        [Display(Name = "Senha")] 
        [MinLength(8, ErrorMessage = "Mínimo de 8 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(50, ErrorMessage = "permitido de 10 a 50 caracteres", MinimumLength = 10)]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage ="E-Mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo CPF obrigatorio")]
        [MinLength(11, ErrorMessage = "Digite todos os números do cpf")]
        public string Cpf { get; set; }

        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}