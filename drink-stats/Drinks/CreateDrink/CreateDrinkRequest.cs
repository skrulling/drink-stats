using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace drink_stats.Drinks.GetDrink
{
    public class CreateDrinkRequest : IRequest<Func<ControllerBase, IActionResult>>
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
        public double VolumeInMillilitres { get; set; }
    }
}
