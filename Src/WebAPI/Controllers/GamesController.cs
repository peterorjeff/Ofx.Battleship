using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.Application.Games.Commands.CreateGame;
using System.Threading.Tasks;

namespace Ofx.Battleship.WebAPI.Controllers
{
    public class GamesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create()
        {
            var gameId = await Mediator.Send(new CreateGameCommand());

            return Ok(gameId);
        }
    }
}
