using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Checkers.ComputerPlayer.Services;
using Checkers.HumanPlayer.Services;
using Checkers.Rules.Services;
using Checkers.Web.Factories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Checkers
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
            services.AddControllersWithViews();
            services.AddResponseCaching();

            var assemblies = new[]
            {
                typeof(HumanPlayerService).Assembly,
                typeof(ComputerPlayerService).Assembly,
                typeof(ValidateRulesService).Assembly,
            };

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(t => t.Name.EndsWith("Service") && !t.IsInterface))
                {
                    services.AddTransient(type);

                    foreach (var typeInterface in type.GetInterfaces().Where(t => t.Name.StartsWith("I") && t.Name.EndsWith("Service")))
                    {
                        services.AddTransient(typeInterface, type);
                    }
                }
            }

            services.AddTransient<IBoardStateDtoFactory, BoardStateDtoFactory>();

            services.AddSwaggerGen(SwaggerConfig);
        }

        public void SwaggerConfig(SwaggerGenOptions options)
        {
            options.CustomSchemaIds(GetSchemaName);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
            });


            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));            
        }

        public string GetSchemaName(Type t)
        {
            return t.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? t.Name;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });
        }
    }
}
