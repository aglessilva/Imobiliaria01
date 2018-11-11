using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Imobiliaria.Models
{
    // [Table("Bairro")]
    public class Bairro : IOperations<Bairro>
    {
        DBConn db = null;
       // [Key]
        public int Id { get; set; }

        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Digite entre 3 a 30 caractéres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo é obrigatorio")]
        [Display(Name = "Bairro")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        [Display(Name = "Cidade")]
        public int  CidadeId { get; set; }

        public CompostoEndereco Composto { get; set; }

        public int Save(Bairro objeto)
        {
            throw new NotImplementedException();
        }

        public int Update(Bairro objeto)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Bairro FindOne(int Id)
        {
            throw new NotImplementedException();
        }

        public List<SelectListItem> ListItemCidades(List<Cidade> lst)
        {
            List<SelectListItem> lstSelectItem = new List<SelectListItem>();
            lst.ForEach(item => {
                lstSelectItem.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Nome });
            });

            if (lstSelectItem.Count == 0)
                return new List<SelectListItem>();

            return lstSelectItem;
        }

        public List<Bairro> FindAll()
        {
            using (var db = new DBConn())
            {
                return db.Bairros.ToList().OrderBy(x => x.Nome).ToList();
            }
        }

        public List<Bairro> FindAllById(int Id)
        {
           db = new DBConn();
            return db.Bairros.Where(b => b.CidadeId == Id).OrderBy(x => x.Nome).ToList();
        }
    }
}