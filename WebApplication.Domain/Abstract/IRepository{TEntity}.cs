using WebApplication.Domain.Models;

namespace WebApplication.Domain.Abstract
{
    public interface IRepository<TEntity> : IQueryable<TEntity>
        where TEntity : Entity
    {
    }
}
