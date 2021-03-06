using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Checkers.BL.Services;
using Ckeckers.DAL.Repositories;

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

            foreach (var type in typeof(GetFiguresService).Assembly.GetTypes().Where(t=>t.Name.EndsWith("Service") && !t.IsInterface))
            {
                services.AddTransient(type);

                foreach (var typeInterface in type.GetInterfaces().Where(t=>t.Name.StartsWith("I") && t.Name.EndsWith("Service")))
                {
                    services.AddTransient(typeInterface, type);
                }
            }
       
         
            services.AddSingleton<IBoardRepository, BoardRepository>();

            services.AddTransient<IRegistrationIdGeneratorRepository, RegistrationIdGeneratorRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
