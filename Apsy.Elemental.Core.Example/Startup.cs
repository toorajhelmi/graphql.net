using Apsy.Elemental.Core.ApiDoc;
using Apsy.Elemental.Core.Identity;
using Apsy.Elemental.Example.Admin.Data;
using Apsy.Elemental.Example.Web.Api;
using Apsy.Elemental.Example.Web.Services;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Voyager;
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

namespace Apsy.Elemental.Example.Web
{
    public class Startup
    {
        private IWebHostEnvironment environment;

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

            services.AddSingleton<SingltonDataContextService>();
            services.AddSingleton<IAuthService, FirebaseAuthService>();
            services.AddSingleton<OrderService>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<SearchHistoryService>();

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
            app.UseGraphiQLServer(new GraphiQLOptions
            {
                Path = "/ui/graphiql",
                GraphQLEndPoint = "/api"
            });
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions());

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
