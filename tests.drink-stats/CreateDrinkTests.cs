using drink_stats;
using drink_stats.Drinks.GetDrink;
using drink_stats.tests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace tests.drink_stats
{
    public class CreateDrinkTests : TestBase
    {
        private const string DrinkUrl = "/api/drink";

        [Test]
        public async Task Create_drink_blank_request_returns_bad_request()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();

                var resp = await client.PostAsync(
                        DrinkUrl,
                    new StringContent(
                        "{}",
                        Encoding.UTF8,
                        "application/json"));

                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                var content = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            }
        }

        [Test]
        public async Task Create_drink_request_with_no_name_returns_bad_request()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();

                var resp = await client.PostAsJsonAsync(
                    DrinkUrl, new
                    {
                        Percentage = 4.7,
                        VolumeInMillilitres = 500
                    });

                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

                var contentJson = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
                Assert.That(contentJson, Is.Not.Null);
                Assert.That(contentJson.Errors.ContainsKey("Name"), Is.True);
            }
        }

        [Test]
        public async Task Drink_with_more_than_one_houndred_percent_will_fail()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();

                var resp = await client.PostAsJsonAsync(
                    DrinkUrl, new
                    {
                        Name = "Vodka",
                        Percentage = 150,
                        VolumeInMillilitres = 500
                    });

                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

                var contentJson = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
                Assert.That(contentJson, Is.Not.Null);
                Assert.That(contentJson.Errors.ContainsKey("Percentage"), Is.True);
            }
        }


        [Test]
        public async Task Create_drink_correct_request_returns_ok_with_id()
        {
            using (var server = CreateTestServer())
            {
                var client = server.CreateClient();

                var resp = await client.PostAsJsonAsync(
                    DrinkUrl,
                    new
                    {
                        Name = "Rignes",
                        Percentage = 4.7,
                        VolumeInMillilitres = 500
                    });

                Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var contentJson = (await resp.Content.ReadFromJsonAsync<JsonElement>())
                    .EnumerateObject()
                    .Single(s => s.Name == "id");

                Assert.AreNotEqual(contentJson.Value.GetInt32(), 0);
            }
        }

    }
}
