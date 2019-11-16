using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.Application.Games.Commands.CreateGame;
using System.Threading.Tasks;

namespace Ofx.Battleship.WebAPI.Controllers
{
    public class GamesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> Create()
        {
            var game = await Mediator.Send(new CreateGameCommand());

            return Ok(game);
        }
    }
}
