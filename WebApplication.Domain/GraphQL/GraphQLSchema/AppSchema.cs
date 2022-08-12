using GraphQL.Types;
using GraphQL.Utilities;

namespace WebApplication.Domain.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
        }
    }
}
