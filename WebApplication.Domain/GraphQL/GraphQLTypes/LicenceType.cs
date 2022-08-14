using GraphQL.Types;
using WebApplication.Domain.Models;

namespace WebApplication.Domain.GraphQL.GraphQLTypes
{
    public sealed class LicenceType : ObjectGraphType<Licence>
    {
        public LicenceType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Description);
            Field(x => x.Active);
            Field(x => x.Level);
            Field(x => x.PublicId);
        }
    }
}
