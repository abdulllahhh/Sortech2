using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Models;
using System.Collections.Concurrent;
using System.Linq.Expressions;


namespace Application.Services.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly ConcurrentDictionary<int, TEntity> _keyValues;

        public Repository(ConcurrentDictionary<int, TEntity> concurrentDictionary)
        {
            _keyValues = concurrentDictionary;
        }

        public void Create(TEntity entity)
        {
            _keyValues.TryAdd(entity.Id, entity);
        }

        public void Delete(TEntity entityToDelete)
        {
            _keyValues.TryRemove(entityToDelete.Id, out _);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _keyValues.Values;
        }

        public TEntity GetByID(int id)
        {
            _keyValues.TryGetValue(id, out var entity);
            return entity;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null)
        {
            var query = _keyValues.Values.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public void Update(TEntity entityToUpdate)
        {
            _keyValues[entityToUpdate.Id] = entityToUpdate;
        }
    }
}
