using MediatR;
using Ofx.Battleship.Application.Common.Interfaces;
using Ofx.Battleship.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.Application.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IBattleshipDbContext _context;

        public CreateGameCommandHandler(IBattleshipDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var entity = new Game();

            _context.Games.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.GameId;
        }
    }
}
