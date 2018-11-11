using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imobiliaria.Models
{
    public interface IOperations<TEntity> where TEntity : class
    {
        int Save(TEntity objeto);

        int Update(TEntity objeto);

        int Delete(int Id);

        TEntity FindOne(int Id);

        List<TEntity> FindAll();

        List<TEntity> FindAllById(int Id);
    }
}
