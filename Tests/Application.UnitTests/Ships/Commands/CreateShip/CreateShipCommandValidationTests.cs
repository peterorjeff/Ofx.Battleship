using FluentValidation.TestHelper;
using Ofx.Battleship.Application.Ships.Commands;
using Xunit;

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
            var boardId = -1;

            // Act & Assert
            _validator.ShouldHaveValidationErrorFor(x => x.BoardId, boardId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenInvalidNumberOfShipParts_ShouldHaveValidationError(int count)
        {
            // Arrange, Act & Assert
            _validator.ShouldHaveValidationErrorFor(x => x.ShipParts.Count, count);
        }

    }
}
