using Starter.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Starter.Infra.Data.Context;
using System.Data.Entity;

namespace Starter.Infra.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected readonly StarterContext Connection;

        Func<IQueryable<T>, Expression<Func<T, object>>[], string, bool, int, int,
            IQueryable<T>> defaultScope = (query, includes, order, reverse, skip, take) =>
        {
            query = query.ApplyIncludes(includes);
            if (!string.IsNullOrEmpty(order))
                query = reverse ? query.OrderByDescending(order) : query.OrderBy(order);
            return query.Skip(skip).Take(take);
        };

        public RepositoryBase()
        {
            Connection = new StarterContext();
        }

        public void Add(T entity)
        {
            Connection.Set<T>().Add(entity);
            Connection.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Connection.Set<T>().AddRange(entities);
            Connection.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null)
        {
            return Connection.Set<T>().AsQueryable()
                .ApplyIncludes(includes).FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            var query = Connection.Set<T>().AsQueryable();
            return defaultScope(query, includes, order, reverse, skip, take).ToList();
        }

        public IEnumerable<T> GetAll(ref long count, Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            var query = Connection.Set<T>().AsQueryable();
            count = query.Count();
            return defaultScope(query, includes, order, reverse, skip, take).ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            var query = Connection.Set<T>().AsQueryable().Where(predicate);
            return defaultScope(query, includes, order, reverse, skip, take).ToList();
        }

        public IEnumerable<T> GetAll(ref long count, Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false, int skip = 0, int take = 0)
        {
            var query = Connection.Set<T>().AsQueryable().Where(predicate);
            count = query.Count();
            return defaultScope(query, includes, order, reverse, skip, take).ToList();
        }

        public void Remove(T entity)
        {
            Connection.Entry(entity).State = EntityState.Deleted;
            Connection.Set<T>().Remove(entity);
            Connection.SaveChanges();
        }

        public void RemoveRange(Expression<Func<T, bool>> predicate = null)
        {
            var query = Connection.Set<T>().AsQueryable();
            query = predicate == null ? query : query.Where(predicate);
            Connection.Set<T>().RemoveRange(query);
            Connection.SaveChanges();
        }

        public void Update(T entity)
        {
            Connection.Entry(entity).State = EntityState.Modified;
            Connection.SaveChanges();
        }
    }

    public static class RepositoryBaseExtension
    {

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName)
        {
            var entityType = typeof(T);

            var propertyInfo = entityType.GetProperty(propertyName);
            var arg = Expression.Parameter(entityType, "x");
            var selector = Expression.Lambda(Expression.Property(arg, propertyName),
               new ParameterExpression[] { arg });
            var method = typeof(Queryable).GetMethods()
                 .First(m => m.IsGenericMethodDefinition && m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            return genericMethod.Invoke(genericMethod, new object[] { query, selector }) as IOrderedQueryable<T>;

        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName)
        {
            var entityType = typeof(T);

            var propertyInfo = entityType.GetProperty(propertyName);
            var arg = Expression.Parameter(entityType, "x");
            var selector = Expression.Lambda(Expression.Property(arg, propertyName),
                new ParameterExpression[] { arg });
            var method = typeof(Queryable).GetMethods()
                 .First(m => m.IsGenericMethodDefinition && m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            return genericMethod.Invoke(genericMethod, new object[] { query, selector }) as IOrderedQueryable<T>;

        }

        public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query,
            Expression<Func<T, object>>[] includes = null)
        {
            for (int i = 0; i < includes?.Count(); i++)
                query = query.Include(includes[i]);
            return query;
        }
    }
}
