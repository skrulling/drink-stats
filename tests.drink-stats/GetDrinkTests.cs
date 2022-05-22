using drink_stats;
using drink_stats.Drinks.GetDrink;
using drink_stats.tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace tests.drink_stats
{
    public class GetDrinkTests : TestBase
    {
        private const string DrinkUrl = "/api/drink";

        [Test]
        public async Task Get_drink_response_returns_not_found_when_drink_does_not_exist()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();
                var resp = await client.GetAsync(DrinkUrl + "/100000");
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [Test]
        public async Task Get_drink_response_returns_correct_data()
        {
            using (var server = CreateTestServer())
            {
                var context = server.Host.Services.GetService<DrinkStatDbContext>();
                var drink = context.Drinks.First();

                var client = server.CreateClient();
                var requestUri = new Uri($"{DrinkUrl}/{drink.Id}", UriKind.Relative);

                var resp = await client.GetAsync(requestUri);
                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var json = await resp.Content.ReadFromJsonAsync<GetDrinkResponse>();
                Assert.That(json.Name, Is.EqualTo(drink.Name));
                Assert.That(json.Percentage, Is.EqualTo(drink.Percentage));
            }
        }

    }
}
