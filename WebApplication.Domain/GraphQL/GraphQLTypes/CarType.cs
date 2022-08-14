using GraphQL.Types;
using WebApplication.Domain.Models;

namespace WebApplication.Domain.GraphQL.GraphQLTypes
{
    public sealed class CarType : ObjectGraphType<Car>
    {
        public CarType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Name).Description("Name property from the owner object.");
            Field(x => x.Brand);
            Field(x => x.VersionGuid);
            Field(x => x.Vin);
        }
    }
}
