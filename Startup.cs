using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using BarryCore1.Configuration;
//using AspNetCoreInjectConfigurationRazor.Configuration;
//using Microsoft.Extensions.Options.ConfigurationExtensions;

namespace BarryCore1
{
    public class Startup
    {
        //   public Startup(IConfiguration configuration)
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
           .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

    //    public IConfiguration Configuration { get; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<ApplicationConfigurations>(Configuration.GetSection("ApplicationConfigurations"));
       //     services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc();
            //  services.AddSingleton(Configuration.GetSection("myConfiguration").Get<MyConfiguration>());


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          //  Console.WriteLine env;
            if (env.IsDevelopment())
            {
      //          app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
