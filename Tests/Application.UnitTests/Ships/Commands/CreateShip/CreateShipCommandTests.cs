using AutoMapper;
using FluentAssertions;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using Ofx.Battleship.Application.UnitTests.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Ofx.Battleship.Application.Ships.Commands.CreateShip.CreateShipCommand;

namespace Ofx.Battleship.Application.UnitTests.Ships.Commands.CreateShip
{
    public class CreateShipCommandTests : CommandTestBase, IClassFixture<MappingTestFixture>
    {
        private readonly IMapper _mapper;

        public CreateShipCommandTests(MappingTestFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Handle_GivenUnknownBoardId_ShouldThrowNotFoundException()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 100,
                ShipParts = new List<ShipPartDto>
                {
                    new ShipPartDto { X = 1, Y = 1 },
                    new ShipPartDto { X = 1, Y = 2 }
                }
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnNewShipId()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                ShipParts = new List<ShipPartDto>
                {
                    new ShipPartDto { X = 2, Y = 1 },
                    new ShipPartDto { X = 2, Y = 2 }
                }
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.ShipId.Should().BePositive();
        }

        [Fact]
        public async void Handle_GivenKnownBoardId_ShouldReturnShipViewModel()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                ShipParts = new List<ShipPartDto>
                {
                    new ShipPartDto { X = 3, Y = 1 },
                    new ShipPartDto { X = 3, Y = 2 }
                }
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ShipViewModel>();
        }

        [Fact]
        public async void Handle_GivenTooLargeDimeneion_ShouldThrowShipOutOfBoundsException()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                ShipParts = new List<ShipPartDto>
                {
                    new ShipPartDto { X = 4, Y = 10 },
                    new ShipPartDto { X = 4, Y = 11 }
                }
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<ShipOutOfBoundsException>();
        }

        [Fact]
        public async void Handle_GivenCollidingShip_ShouldThrowShipCollisionException()
        {
            // Arrange
            var command = new CreateShipCommand
            {
                BoardId = 1,
                ShipParts = new List<ShipPartDto>
                {
                    new ShipPartDto { X = 1, Y = 1 },
                    new ShipPartDto { X = 1, Y = 2 }
                }
            };
            var handler = new CreateShipCommandHandler(_context, _mapper);

            // Act
            Func<Task> response = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            await response.Should().ThrowAsync<ShipCollisionException>();
        }
    }
}
