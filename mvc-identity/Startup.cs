using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MvcIdentitySample.Models;

namespace MvcIdentitySample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<WebsiteDbContext>(
                        options => options.UseSqlite("Data Source=./mvcidentitysample.sqlite"));
            
            // the options below are required to ASP.NET Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<WebsiteDbContext>()
                .AddDefaultTokenProviders();
            
            // MvcCore is not enough for ASP.NET Identity    
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            
            app.UseStaticFiles();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            
            // the authentication must be configured before mvc
            app.UseIdentity();
            /*
            app.UseFacebookAuthentication(new FacebookOptions
            {
                AppId = "901611409868059",
                AppSecret = "4aa3c530297b1dcebc8860334b39668b"
            })
            .UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = "514485782433-fr3ml6sq0imvhi8a7qir0nb46oumtgn9.apps.googleusercontent.com",
                ClientSecret = "V2nDD9SkFbvLTqAUBWBBxYAL"
            })
            .UseTwitterAuthentication(new TwitterOptions
            {
                ConsumerKey = "BSdJJ0CrDuvEhpkchnukXZBUv",
                ConsumerSecret = "xKUNuKhsRdHD03eLn67xhPAyE1wFFEndFo1X2UJaK2m1jdAxf4"
            });
            */
            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            // create the database if it doesn't exist
            InitializeDatabase(app);
        }
        
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<WebsiteDbContext>();
                db.Database.EnsureCreated();
            }
        }
    }
}