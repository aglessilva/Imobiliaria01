using System;
using System.Collections.Generic;
using System.Linq;

namespace Imobiliaria.Models
{
    
    public class Estado : IOperations<Estado>
    {
        
        public int Id { get; set; }

        //[StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Este campo deve ter entre e e 30 caractéres")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Este Campo é obrigatorio")]
        //[Display(Name = "Estado")]
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public ICollection<Cidade> CidadesUF { get; set; }

        public int Save(Estado objeto)
        {
            throw new NotImplementedException();
        }

        public int Update(Estado objeto)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Estado FindOne(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Estado> FindAll()
        {
            using (var db = new DBConn())
            {
                return db.Estados.Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();
            }
        }

        public List<Estado> FindAllById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}