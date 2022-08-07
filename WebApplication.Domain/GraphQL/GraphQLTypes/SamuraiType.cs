using GraphQL.Types;

namespace LearnEFCore.Domain.GraphQL.GraphQLTypes
{
    public class SamuraiType : ObjectGraphType<Samurai>
    {
        public SamuraiType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Name).Description("Name property from the owner object.");
        }
    }
}
