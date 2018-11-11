using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Imobiliaria.Models
{
    public class Cidade: IOperations<Cidade>
    {
        DBConn db = null;

        public Cidade()
        {
            this.BairrosCidades = new  HashSet<Bairro>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public int EstadoId { get; set; }
        public virtual ICollection<Bairro> BairrosCidades { get; set; }

        public int Save(Cidade objeto)
        {
            throw new NotImplementedException();
        }

        public int Update(Cidade objeto)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Cidade FindOne(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Cidade> FindAll()
        {
            db = new DBConn();
            return db.Cidades.OrderBy(x => x.Nome).ToList();
        }

        public List<Cidade> FindAllById(int Id)
        {
            db = new DBConn();
            return db.Cidades.Where(x => x.EstadoId == Id && x.Ativo == true).OrderBy(x =>x.Nome).ToList();
        }
    }
}