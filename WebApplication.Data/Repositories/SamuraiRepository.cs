using LearnEFCore.Domain;
using LearnEFCore.Domain.Repositories;
using WebApplication.Data;

namespace LearnEFCore.Data.Repositories
{
    public class SamuraiRepository : ISamuraiRepository
    {
        private readonly LearnEFCoreContext _context;

        public SamuraiRepository(LearnEFCoreContext context)
        {
            _context = context;
        }
        public IEnumerable<Samurai> GetAll() => _context.Samurai.ToList();
    }
}
