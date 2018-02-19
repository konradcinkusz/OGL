using Microsoft.AspNet.Identity.EntityFramework;
using Repozytorium.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repozytorium.Logic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IdentityDbContext _entities;

        public IdentityDbContext _context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        
        public Repository(OglContext context)
        {
            _context = context;
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) => _context.Set<T>().Where(predicate);
        public virtual void Add(T obj) => _context.Set<T>().Add(obj);
        public virtual void Remove(T obj) => _context.Set<T>().Remove(obj);
        public virtual void Save() => _context.SaveChanges();
    }
}
