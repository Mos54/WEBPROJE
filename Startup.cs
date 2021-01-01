using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
=======
using Microsoft.AspNetCore.Routing;
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD
using Microsoft.Extensions.Options;
=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1

namespace OnlineShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

<<<<<<< HEAD
        

=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

<<<<<<< HEAD


=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                //options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

<<<<<<< HEAD





=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(options => options.EnableEndpointRouting = false);
<<<<<<< HEAD
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)

                 .AddDefaultTokenProviders()
                  .AddDefaultUI()
                 .AddEntityFrameworkStores<ApplicationDbContext>();
=======
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false) // for email comfirm
                .AddDefaultTokenProviders()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;

                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });

<<<<<<< HEAD

          
            services.AddControllersWithViews();
          
            services.AddRazorPages();
            services.AddAuthentication();




        }
   
=======
            services.AddControllersWithViews();
            
               
               
            services.AddRazorPages();
            services.AddAuthentication();
        }
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
<<<<<<< HEAD

          

=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

<<<<<<< HEAD

=======
>>>>>>> 96e8ef7b184206a77ae8974e838b62c45a9380e1
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
                );
            });



        }
    }
}
