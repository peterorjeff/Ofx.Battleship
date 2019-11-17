using FluentValidation.TestHelper;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using Xunit;
using static Ofx.Battleship.Application.Ships.Commands.CreateShip.CreateShipCommand;

namespace Ofx.Battleship.Application.UnitTests.Ships.Commands.CreateShip
{
    public class ShipPartsDtoValidationTests
    {
        private readonly ShipPartsDtoValidator _validator;

        public ShipPartsDtoValidationTests()
        {
            _validator = new ShipPartsDtoValidator();
        }

        [Fact]
        public void GivenInvalidShipPartX_ShouldHaveValidationError()
        {
            // Arrange
            var part = new ShipPartDto { X = 0, Y = 1 };

            // Act
            var result = _validator.TestValidate(part);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.X);
        }

        [Fact]
        public void GivenInvalidShipPartY_ShouldHaveValidationError()
        {
            // Arrange
            var part = new ShipPartDto { X = 1, Y = -1 };

            // Act
            var result = _validator.TestValidate(part);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Y);
        }
    }
}
