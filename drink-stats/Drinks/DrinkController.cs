using drink_stats.Drinks.GetDrink;
using Microsoft.AspNetCore.Mvc;

namespace drink_stats.Drinks
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : DrinkStatsController
    {
        [HttpGet("{id}")]
        public Task<IActionResult> GetJob([FromRoute] GetDrinkRequest request)
        {
            return HandleRequestAsync(request);
        }

        [HttpPost]
        public Task<IActionResult> CreateJob([FromBody] CreateDrinkRequest request)
        {
            return HandleRequestAsync(request);
        }

    }
}
