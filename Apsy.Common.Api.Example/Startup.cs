using Apsy.Common.Api.Auth;
using Apsy.Common.Api.Core.ApiDoc;
using Apsy.Common.Api.Core.Graph;
using Apsy.Elemental.Example.Admin.Data;
using Apsy.Example.Api;
using Apsy.Example.Mutations;
using Apsy.Example.Queries;
using Apsy.Example.Services;
using Apsy.Example.Subscriptions;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Apsy.Example
{
    public class Startup
    {
        private IWebHostEnvironment environment;
        private bool usePlayground = false;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddDbContext<DataContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddMvc(option => option.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddControllersWithViews();

            services.AddSingleton<SingltonDataContextService<DataContext>>();
            services.AddSingleton<IAuthService, FirebaseAuthService>();
            services.AddSingleton<PostService>();

            services.AddSingleton<Query>();
            services.AddSingleton<Mutation>();
            services.AddSingleton<Subscription>();
            services.AddSingleton<Schema>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = environment.IsDevelopment();
            })
            .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
            .AddWebSockets()
            .AddDataLoader()
            .AddGraphTypes(typeof(Schema));

            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/apsy-Exampled";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/apsy-Example",
                    ValidateAudience = true,
                    ValidAudience = "apsy-Example",
                    ValidateLifetime = true
                };
            });

            services.AddSwaggerGen(s =>
                s.SchemaFilter<SwaggerIgnoreFilter>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSockets();

            app.UseGraphQLWebSockets<Schema>("/api");
            app.UseGraphQL<Schema>("/api");

            if (!usePlayground)
            {
                app.UseGraphiQLServer(new GraphiQLOptions
                {
                    Path = "/ui/graphiql",
                    GraphQLEndPoint = "/api"
                });
            }
            else
            {
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
                {
                    GraphQLEndPoint = "/api",
                    Path = "/ui/pg",
                });
            }

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
