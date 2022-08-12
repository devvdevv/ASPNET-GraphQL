using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Data.Repositories;
using WebApplication.Domain.Abstract;
using WebApplication.Domain.GraphQL.GraphQLSchema;

namespace WebApplication.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<WebApplicationDbContext>(opt =>
                opt.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=WebApplicationData"));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<AppSchema>();
            services.AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped);

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseGraphQL<AppSchema>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed data
            dbContext.Seed();
        }
    }
}
