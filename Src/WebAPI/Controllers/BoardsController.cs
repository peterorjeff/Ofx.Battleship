using Microsoft.AspNetCore.Mvc;
using Ofx.Battleship.Application.Boards.Commands.CreateBoard;
using System.Threading.Tasks;

namespace Ofx.Battleship.WebAPI.Controllers
{
    public class BoardsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<BoardViewModel>> Create([FromBody] CreateBoardCommand command)
        {
            var board = await Mediator.Send(command);

            return Ok(board);
        }
    }
}
