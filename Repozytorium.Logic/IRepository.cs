using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Repozytorium.Logic
{
    public interface IRepository<T> where T : class
    {
        void Add(T obj);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Remove(T obj);
        void Save();
    }
}