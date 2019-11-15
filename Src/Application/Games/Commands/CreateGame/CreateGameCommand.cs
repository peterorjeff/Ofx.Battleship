using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ofx.Battleship.Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<int>
    {
    }
}
