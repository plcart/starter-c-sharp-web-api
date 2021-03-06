﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Starter.Domain.Interfaces.Services
{
    public interface IServiceBase<T> where T:class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null);
        IEnumerable<T> GetAll(Expression<Func<T, object>>[] includes = null, string order = "", bool reverse = false,
            int skip = 0, int take = 0);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null,
            string order = "", bool reverse = false, int skip = 0, int take = 0);
        IEnumerable<T> GetAll(ref long count, Expression<Func<T, object>>[] includes = null, string order = "",
            bool reverse = false, int skip = 0, int take = 0);
        IEnumerable<T> GetAll(ref long count, Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[] includes = null,
            string order = "", bool reverse = false, int skip = 0, int take = 0);
    }
}
