using GraphQL.Types;
using LearnEFCore.Domain.GraphQL.GraphQLTypes;
using WebApplication.Domain.Repositories;

namespace LearnEFCore.Domain.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        private readonly IRepository<Samurai> samuraiRepo;

        public AppQuery(IRepository<Samurai> samuraiRepo)
        {
            this.samuraiRepo = samuraiRepo;
            GetSamurais();
        }

        public void GetSamurais()
        {
            Field<ListGraphType<SamuraiType>>(
                "samurais",
                resolve: context => samuraiRepo.ToList()
            );
        }
    }
}
