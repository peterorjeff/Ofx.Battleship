using FluentAssertions;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using Ofx.Battleship.WebAPI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.Application.Ships.Commands.CreateShip.CreateShipCommand;
using static Ofx.Battleship.WebAPI.IntegrationTests.Common.Utilities;

namespace Ofx.Battleship.WebAPI.IntegrationTests.Controllers.Ships
{
    public class CreateShipTests : IClassFixture<IntegrationTestWebApplicationFactory<Startup>>
    {
        private readonly IntegrationTestWebApplicationFactory<Startup> _factory;

        public CreateShipTests(IntegrationTestWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateShip_ReturnsNewShip()
        {
            // Arrange
            var client = _factory.CreateClient();
            var command = new CreateShipCommand
            {
                BoardId = 1,
                ShipParts = new List<ShipPartDto>
                {
                    new ShipPartDto { X = 1, Y = 1 },
                    new ShipPartDto { X = 1, Y = 2 }
                }
            };
            var requestContent = GetRequestContent(command);

            // Act
            var response = await client.PostAsync("/api/ships", requestContent);

            response.EnsureSuccessStatusCode();

            var content = await GetResponseContent<ShipViewModel>(response);

            // Assert
            content.ShipId.Should().BePositive();
        }
    }
}
