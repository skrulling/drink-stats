using drink_stats.Drinks;

namespace drink_stats.tests
{
    public abstract class TestBase
    {
        protected TestServer CreateTestServer()
        {
            var hostBuilder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<DrinkStatDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("test");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<DrinkStatDbContext>();

                        db.Database.EnsureCreated();

                        db.Drinks.Add(new Drink
                        {
                            Name = "Hansa",
                            Percentage = 4.7,
                            VolumeMilliLitre = 500
                        });

                        db.Drinks.Add(new Drink
                        {
                            Name = "Vodka",
                            Percentage = 40,
                            VolumeMilliLitre = 40
                        });

                        db.SaveChanges();
                    }
                })
                .UseStartup<Startup>();

            return new TestServer(hostBuilder);
        }
    }
}