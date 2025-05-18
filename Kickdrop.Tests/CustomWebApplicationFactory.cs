using Kickdrop.Api;
using Kickdrop.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove all KickdropContext and DbContextOptions registrations
            var descriptors = services
                .Where(d =>
                    d.ServiceType == typeof(KickdropContext) ||
                    d.ServiceType == typeof(DbContextOptions<KickdropContext>))
                .ToList();

            foreach (var descriptor in descriptors)
            {
                services.Remove(descriptor);
            }

            // Add InMemory DbContext for testing
            services.AddDbContext<KickdropContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Ensure database is created
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<KickdropContext>();
                db.Database.EnsureCreated();
            }
        });
    }
}