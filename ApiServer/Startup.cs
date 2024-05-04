using ApiServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ApiServer.TokenData;
using Microsoft.AspNetCore.Identity;
using EntityGraphQL.AspNet;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;
using GraphQL.Types;
using GraphQL;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using EntityGraphQL.Schema;
using ApiServer.Controllers;

namespace ApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<olympicsContext>();

            // This registers a SchemaProvider<DemoContext> and uses reflection to build the schema with default options
            services.AddGraphQLSchema<olympicsContext>();

            var schema = SchemaBuilder.FromObject<olympicsContext>();
            //
            schema.AddMutationsFrom<CityMutations>();
            schema.AddMutationsFrom<NocRegionMutations>();
            schema.AddMutationsFrom<SportMutations>();
            schema.AddMutationsFrom<PersonMutations>();
            schema.AddMutationsFrom<GameMutations>();
            schema.AddMutationsFrom<EventMutations>();
            //
            services.AddSingleton(schema);

            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("authorized", policy => policy.RequireAuthenticatedUser());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseGraphQLPlayground();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL<olympicsContext>();
            });
        }
    }
}


