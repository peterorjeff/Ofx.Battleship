using FluentAssertions;
using Ofx.Battleship.Application.Games.Commands.CreateGame;
using Ofx.Battleship.Application.UnitTests.Common;
using System.Threading;
using Xunit;

namespace Ofx.Battleship.Application.UnitTests.Games.Commands.CreateGame
{
    public class CreateGameCommandTests : CommandTestBase
    {
        [Fact]
        public async void Handle_GivenValidRequest_ShouldReturnGameId()
        {
            // Arrange
            var command = new CreateGameCommandHandler(_context);

            // Act
            var gameId = await command.Handle(new CreateGameCommand(), CancellationToken.None);

            // Assert
            gameId.Should().BePositive();
        }
    }
}
