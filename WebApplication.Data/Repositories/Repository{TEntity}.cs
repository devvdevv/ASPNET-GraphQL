using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;
using WebApplication.Domain.Abstract;
using WebApplication.Domain.Models;

namespace WebApplication.Data.Repositories
{
    /// <summary>
    /// Ref:
    /// https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly WebApplicationDbContext _context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(WebApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public Type ElementType => ((IQueryable)dbSet).ElementType;

        public Expression Expression => ((IQueryable)dbSet).Expression;

        public IQueryProvider Provider => ((IQueryable)dbSet).Provider;

        public Task Add(TEntity entity, CancellationToken cancellationToken)
        {
            dbSet.Add(entity);
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            dbSet.AddRange(entities);
            return Task.CompletedTask;
        }

        public IEnumerator<TEntity> GetEnumerator() => ((IEnumerable<TEntity>)dbSet).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)dbSet).GetEnumerator();
    }
}
