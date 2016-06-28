using Starter.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Starter.Domain.Interfaces.Repositories;

namespace Starter.Domain.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        internal protected IRepositoryBase<T> repository { get; }

        public ServiceBase(IRepositoryBase<T> repo)
        {
            repository = repo;
        }

        public void Add(T entity)
        {
            repository.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            repository.AddRange(entities);
        }

        public T Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null)
        {
            return repository.Get(predicate, includes);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            return repository.GetAll(includes, order, reverse, skip, take);
        }

        public IEnumerable<T> GetAll(ref long count, Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            return repository.GetAll(ref count, includes, order, reverse, skip, take);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            return repository.GetAll(predicate, includes, order, reverse, skip, take);
        }

        public IEnumerable<T> GetAll(ref long count, Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            return repository.GetAll(ref count, predicate, includes, order, reverse, skip, take);
        }

        public void Remove(T entity)
        {
            repository.Remove(entity);
        }

        public void Update(T entity)
        {
            repository.Update(entity);
        }
    }
}
