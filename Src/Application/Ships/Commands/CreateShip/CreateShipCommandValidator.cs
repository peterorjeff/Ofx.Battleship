using FluentValidation;
using static Ofx.Battleship.Application.Ships.Commands.CreateShip.CreateShipCommand;

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidator : AbstractValidator<CreateShipCommand>
    {
        public CreateShipCommandValidator()
        {
            RuleFor(x => x.BoardId).GreaterThan(0).NotEmpty();

            RuleFor(x => x.ShipParts)
                .Must(x => x.Count >= 2)
                .WithMessage("Minimum length of Ship is 2.")
                .Must(x => x.Count <= 4)
                .WithMessage("Maximum length of Ship is 4.");

            RuleForEach(x => x.ShipParts).SetValidator(new ShipPartsDtoValidator());
        }
    }

    public class ShipPartsDtoValidator : AbstractValidator<ShipPartDto>
    {
        public ShipPartsDtoValidator()
        {
            RuleFor(x => x.X)
                    .GreaterThan(0)
                    .WithMessage("Ship dimensions must be positive.");

            RuleFor(x => x.Y)
                .GreaterThan(0)
                .WithMessage("Ship dimensions must be positive.");
        }
    }
}
