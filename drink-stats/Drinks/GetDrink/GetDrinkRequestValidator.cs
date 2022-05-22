using FluentValidation;

namespace drink_stats.Drinks.GetDrink
{
    public class GetDrinkRequestValidator : AbstractValidator<GetDrinkRequest>
    {
        public GetDrinkRequestValidator()
        {
        }
    }
}
