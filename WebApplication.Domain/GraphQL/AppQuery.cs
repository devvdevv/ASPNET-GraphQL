using GraphQL.Types;
using WebApplication.Domain.Abstract;
using WebApplication.Domain.GraphQL.GraphQLTypes;
using WebApplication.Domain.Models;

namespace WebApplication.Domain.GraphQL
{
    public class AppQuery : ObjectGraphType
    {
        private readonly IRepository<Person> _people;

        public AppQuery(IRepository<Person> people)
        {
            _people = people;
            GetPeople();
        }

        public void GetPeople()
        {
            Field<ListGraphType<PersonType>>(
                "people",
                resolve: context => _people.ToList()
            );
        }
    }
}
