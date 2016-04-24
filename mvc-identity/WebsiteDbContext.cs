
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcIdentitySample.Models;

namespace MvcIdentitySample
{
    public class WebsiteDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
            : base(options)
        {

        }

        //public DbSet<Article> Articles { get; set; }
    }
}