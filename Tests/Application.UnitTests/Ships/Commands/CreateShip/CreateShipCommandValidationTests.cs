using FluentValidation.TestHelper;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using System.Collections.Generic;
using Xunit;
using static Ofx.Battleship.Application.Ships.Commands.CreateShip.CreateShipCommand;

namespace Ofx.Battleship.Application.UnitTests.Ships.Commands.CreateShip
{
    public class CreateShipCommandValidationTests
    {
        private readonly CreateShipCommandValidator _validator;

        public CreateShipCommandValidationTests()
        {
            _validator = new CreateShipCommandValidator();
        }

        [Fact]
        public void GivenInvalidBoardId_ShouldHaveValidationError()
        {
            // Arrange
            var ship = new CreateShipCommand { BoardId = -1 };

            // Act
            var result = _validator.TestValidate(ship);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BoardId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenInvalidNumberOfShipParts_ShouldHaveValidationError(int count)
        {
            // Arrange
            var ship = new CreateShipCommand { ShipParts = new List<ShipPartDto>() }; 
            for(int i = 0; i < count; i++)
            {
                ship.ShipParts.Add(new ShipPartDto { X = 1, Y = i });
            };

            // Act
            var result = _validator.TestValidate(ship);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ShipParts);
        }
    }
}
