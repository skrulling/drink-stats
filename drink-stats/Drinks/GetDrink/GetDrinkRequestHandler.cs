using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace drink_stats.Drinks.GetDrink
{
    public class GetDrinkRequestHandler : IRequestHandler<GetDrinkRequest, Func<ControllerBase, IActionResult>>
    {
        private readonly DrinkStatDbContext context;

        private readonly IMapper mapper;


        public GetDrinkRequestHandler(
            IEnumerable<IValidator<GetDrinkRequest>> validators, DrinkStatDbContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Func<ControllerBase, IActionResult>> Handle(GetDrinkRequest message, CancellationToken cancellationToken)
        {
            var drink = await context.Drinks
                .Where(d => d.Id == message.Id)
                .ProjectTo<GetDrinkResponse>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            if (drink == null)
            {
                return controller => controller.NotFound();
            }

            return controller => controller.Ok(drink);
        }
    }
}
