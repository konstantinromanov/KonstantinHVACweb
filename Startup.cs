﻿using KonstantinHVACweb.BusinessLogic.Services;
using KonstantinHVACweb.BusinessLogic.Services.Interface;
using KonstantinHVACweb.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Rewrite;


namespace KonstantinHVACweb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMemoryCache();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            {
                app.UseRewriter(new RewriteOptions()
                    .AddRedirectToWwwPermanent()
                    );
            }

            app.UseStatusCodePagesWithRedirects("/Home/Error?errorCode={0}");

            app.UseHttpsRedirection();

            var cachePeriod = env.IsDevelopment() ? "30" : "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx => ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}")
            });

            app.UseRouting();

            app.UseMiddleware(typeof(CookieManagementMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Lv}/{action=Index}/{id?}");
            });
        }
    }
}
