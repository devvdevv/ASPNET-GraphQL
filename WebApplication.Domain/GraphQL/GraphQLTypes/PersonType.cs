using GraphQL.Types;
using WebApplication.Domain.Models;

namespace WebApplication.Domain.GraphQL.GraphQLTypes
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Name).Description("Name property from the owner object.");
            Field(x => x.VersionUpdateGuid);
        }
    }
}
