using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MvcEfSample.Models;

namespace MvcEfSample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<WebsiteDbContext>(
                        options => options.UseSqlite("Data Source=./mvcefsample.sqlite"));
                        
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
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            InitializeDatabase(app);
        }
        
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<WebsiteDbContext>();

                if (db.Database.EnsureCreated())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var article = new Article {
                            Title = string.Format("Article {0}",  i + 1),
                            Content = string.Format("Article {0} content blabla blabla",  i + 1),
                            CreatedTime = DateTime.Now,
                            UpdatedTime = DateTime.Now
                        };
                        
                        db.Articles.Add(article);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}