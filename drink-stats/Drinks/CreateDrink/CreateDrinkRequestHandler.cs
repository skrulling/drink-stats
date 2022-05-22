using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace drink_stats.Drinks.GetDrink
{
    public class CreateDrinkRequestHandler : IRequestHandler<CreateDrinkRequest, Func<ControllerBase, IActionResult>>
    {
        private readonly DrinkStatDbContext context;

        private readonly IMapper mapper;


        public CreateDrinkRequestHandler(
            IEnumerable<IValidator<GetDrinkRequest>> validators, DrinkStatDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Func<ControllerBase, IActionResult>> Handle(CreateDrinkRequest message, CancellationToken cancellationToken)
        {
            var newDrink = mapper.Map<Drink>(message);
            context.Drinks.Add(newDrink);
            await context.SaveChangesAsync(cancellationToken);
            return controller => controller.Ok(new CreateDrinkResponse(newDrink.Id));
        }
    }
}
