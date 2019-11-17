using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.Application.Ships.Commands.CreateShip;
using System.Threading.Tasks;

namespace Ofx.Battleship.WebAPI.Controllers
{
    public class ShipsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<ShipViewModel>> Create([FromBody] CreateShipCommand command)
        {
            var ship = await Mediator.Send(command);

            return Ok(ship);
        }

    }
}
