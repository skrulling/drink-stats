using FluentValidation;

namespace drink_stats.Drinks.GetDrink
{
    public class CreateDrinkRequestValidator : AbstractValidator<CreateDrinkRequest>
    {
        public CreateDrinkRequestValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("Please enter a name.");
            RuleFor(r => r.Percentage).NotEmpty().WithMessage("A drink must have a alcohol percentage.");
            RuleFor(r => r.VolumeInMillilitres).NotEmpty().WithMessage("A drink must have a volume.");
        }
    }
}
