using MediatR;
using System.Collections.Generic;

namespace Ofx.Battleship.Application.Ships.Commands.CreateShip
{
    public class CreateShipCommand : IRequest<ShipViewModel>
    {
        public CreateShipCommand()
        {
            ShipParts = new List<ShipPartDto>();
        }

        public int BoardId { get; set; }
        public IList<ShipPartDto> ShipParts { get; set; }

        public class ShipPartDto
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
