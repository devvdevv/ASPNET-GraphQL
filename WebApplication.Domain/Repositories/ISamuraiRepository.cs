namespace LearnEFCore.Domain.Repositories
{
    public interface ISamuraiRepository
    {
        IEnumerable<Samurai> GetAll();
    }
}
