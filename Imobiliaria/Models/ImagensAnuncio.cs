using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class ImagensAnuncio
    {
        public int Id { get; set; }
        public int AnuncioId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        [Display(Name = "Imagem")]
        public byte[] Path { get; set; }
        public virtual Anuncio AnuncioCasa { get; set; }

       
    }
}