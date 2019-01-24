using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Imobiliaria.Models
{
    //[Table("Imoveis")]
    public class TipoImovel: IOperations<TipoImovel>
    {

        private DBConn db = null;

      //  [Key]
        public int Id { get; set; }

        //[StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Tipo de imóvel deve ter entre e e 30 caractéres")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo é obrigatorio")]
        //[Display(Name = "Tipo de Imovel")]
        public string Nome { get; set; }
        //public bool Ativo { get; set; }

        public int Save(TipoImovel objeto)
        {
            throw new NotImplementedException();
        }

        public int Update(TipoImovel objeto)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public TipoImovel FindOne(int Id)
        {
            throw new NotImplementedException();
        }

        public List<TipoImovel> FindAll()
        {
            db = new DBConn();
            var lst = db.TipoImoveis.ToList();
            db.Database.Connection.Close();

            return lst;
        }

        public List<TipoImovel> FindAllById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}