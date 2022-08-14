using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using WebApplication.Domain.Abstract;
using WebApplication.Domain.Models;

namespace WebApplication.Domain.GraphQL.GraphQLTypes
{
    public sealed class PersonType : ObjectGraphType<Person>
    {
        public PersonType(
            IRepository<Person> personRepo,
            IRepository<Car> carRepo,
            IRepository<Licence> licenceRepo)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Name).Description("Name property from the owner object.");
            Field(x => x.VersionUpdateGuid);
            Field<ListGraphType<CarType>>(
                "cars",
                resolve:
                    context => carRepo
                        .Where(x => x.Owner.Id == context.Source.Id)
                        .ToList());

            Field<ListGraphType<LicenceType>>(
                "licences",
                resolve:
                context => personRepo
                    .Include(x => x.Licences)
                    .Where(x => x.Id == context.Source.Id)
                    .Select(x => x.Licences)
                    .AsNoTracking()
                    .ToList());
        }
    }
}
