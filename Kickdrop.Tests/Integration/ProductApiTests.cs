using System.Net;
using System.Threading.Tasks;
using Kickdrop.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Kickdrop.Tests.Integration
{
    public class ProductApiTests : IClassFixture<CustomWebApplicationFactory<Kickdrop.Api.Program>>
    {
        private readonly HttpClient _client;

        public ProductApiTests(CustomWebApplicationFactory<Kickdrop.Api.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllShoes_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/product");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetShoeById_ReturnsNotFound_ForInvalidId()
        {
            var response = await _client.GetAsync("/api/product/9999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetShoesByColor_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/product/color/Black");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/health");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("OK", content);
        }

        [Fact(Skip = "To be used for diagnostic purposes")]
        public async Task PrintErrorResponse()
        {
            var response = await _client.GetAsync("/api/product");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}, Content: {content}");
            Assert.True(false, $"Response: {content}");
        }
    }

    // Use SQL Server as in appsettings.json for integration tests
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<KickdropContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Use the connection string from appsettings.json
                var configuration = context.Configuration;
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<KickdropContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Optionally, ensure database is created for tests
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<KickdropContext>();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}