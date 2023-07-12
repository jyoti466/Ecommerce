using Ecommerce.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Startup
    {
        private readonly IConfiguration _config;
       

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(

               Options => Options.UseSqlServer(_config.GetConnectionString("OnlineShoppingDBConnection")));

            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();



            //services.AddAuthentication().addGoogle(options =>
            //{
            //    options.ClientId = "1031258642084-qfeh3aahd5k2hp8uc28ft8uds573uoc8.apps.googleusercontent.com";
            //    options.ClientSecret = "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";
            //});

            services.AddScoped<Iproductrepository, SQLProductrepository>();
            

            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(30);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
               } );
            

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
              {
                  options.Password.RequiredLength = 10;
                  options.Password.RequiredUniqueChars = 3;
                  options.Password.RequireNonAlphanumeric = false;
              }).AddEntityFrameworkStores<AppDbContext>();
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

           app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=start}/{id?}");
            });


        }
    }
}
