using FluentValidation;

namespace drink_stats.Drinks.GetDrink
{
    public class CreateDrinkRequestValidator : AbstractValidator<CreateDrinkRequest>
    {
        public CreateDrinkRequestValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("Please enter a name.");
            RuleFor(d => d.Percentage).NotEmpty().WithMessage("A drink must have a alcohol percentage.");
            RuleFor(d => d.Percentage).ExclusiveBetween(0, 100).WithMessage("Alcohol content must be between 0 and 100 percent");
            RuleFor(d => d.VolumeInMillilitres).NotEmpty().WithMessage("A drink must have a volume.");
            RuleFor(d => d.VolumeInMillilitres).ExclusiveBetween(0, 10000).WithMessage("Volume must be between 0ml and 10 000ml");
        }
    }
}
