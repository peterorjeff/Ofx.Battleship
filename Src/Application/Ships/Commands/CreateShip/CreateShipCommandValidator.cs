using FluentValidation;

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        public CreateShipCommandValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0).NotEmpty();

            RuleFor(x => x.ShipParts.Count)
                .GreaterThanOrEqualTo(2)
                .WithMessage("Minimum length of Ship is 2.")
                .LessThanOrEqualTo(4)
                .WithMessage("Maximum length of Ship is 4.");

            RuleForEach(x => x.ShipParts).ChildRules(parts =>
            {
                parts.RuleFor(x => x.X)
                    .GreaterThan(0)
                    .WithMessage("Ship dimensions must be positive.");

                parts.RuleFor(x => x.Y)
                    .GreaterThan(0)
                    .WithMessage("Ship dimensions must be positive.");
            });
        }
    }
}
