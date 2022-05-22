using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace drink_stats.Drinks.GetDrink
{
    public class GetDrinkRequest : IRequest<Func<ControllerBase, IActionResult>>
    {
        public int Id { get; set; }
    }
}
