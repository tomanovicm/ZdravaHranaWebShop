﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;
using ZdravaHranaWebShop.Models;

namespace ZdravaHranaWebShop.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T,bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
            );

        Task<IPagedList<T>> GetPagedList(
            RequestParams requestedParams,
            List<string> includes = null
            );

        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task Insert(T entity);

        Task InsertRange(IEnumerable<T> entities);

        Task Delete(Guid id);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}