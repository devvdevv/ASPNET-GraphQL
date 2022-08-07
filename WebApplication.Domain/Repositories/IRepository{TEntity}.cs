namespace WebApplication.Domain.Repositories
{
    public interface IRepository<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity> where TEntity : class
    {
    }
}
