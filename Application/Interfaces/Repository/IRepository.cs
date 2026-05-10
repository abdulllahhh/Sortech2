using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public void Create(TEntity entity);
        public IEnumerable<TEntity> GetAll();
        public TEntity GetByID(int id);
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null);
        public void Delete(TEntity entityToDelete);
        public void Update(TEntity entityToUpdate);
    }
}
