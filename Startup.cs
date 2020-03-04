using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;
using crud_chat.Services;

namespace crud_chat
{
    public enum ServiceType { SPHERE, ROOM, MESSAGE };

    public delegate IBLLService ServiceResolver(ServiceType key);

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
            services.AddDbContext<CrudChatContext>(opt => opt.UseSqlite("Data Source=crud_chat.db"));
            services.AddTransient<SphereService>();
            services.AddTransient<RoomService>();
            services.AddTransient<MessageService>();

            services.AddTransient<ServiceResolver>(serviceProvider => key =>
            {
                switch(key)
                {
                    case ServiceType.SPHERE:
                        return serviceProvider.GetService<SphereService>();
                    case ServiceType.ROOM:
                        return serviceProvider.GetService<RoomService>();
                    case ServiceType.MESSAGE:
                        return serviceProvider.GetService<MessageService>();
                    default:
                        throw new KeyNotFoundException();
                }
            });

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    // spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
