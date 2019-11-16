using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.Application.Common.Exceptions;
using Ofx.Battleship.Application.Common.Interfaces;
using Ofx.Battleship.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.Application.Ships.Commands
{
    public class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, ShipViewModel>
    {
        private readonly IBattleshipDbContext _context;
        private readonly IMapper _mapper;

        public CreateShipCommandHandler(IBattleshipDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShipViewModel> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync(request.BoardId);
            if (board == null)
            {
                throw new NotFoundException(nameof(Board), request.BoardId);
            }
            
            // Check ship within board dimensions.
            // Validation rule ensures X & Y are > 0, so only need to verify X & Y < board dimensions.
            foreach(var part in request.ShipParts)
            {
                if (part.X > board.DimensionX)
                {
                    throw new ShipOutOfBoundsException(nameof(part.X), part.X, board.DimensionX);
                }
                if (part.Y > board.DimensionY)
                {
                    throw new ShipOutOfBoundsException(nameof(part.Y), part.Y, board.DimensionX);
                }
            }

            // Check collision with existing ships.
            // Distinct the request lists as one of them will always be entirely duplicates.
            var xList = request.ShipParts.Select(x => x.X).Distinct();
            var yList = request.ShipParts.Select(x => x.Y).Distinct();

            // Project the X,Y and compare to xList && yList. This works as one of request X or Y is always a single integer.
            var collisions = _context.Ships
                .Include(ship => ship.ShipParts)
                .Where(ship => ship.Board == board)
                .SelectMany(ship => ship.ShipParts, (ship, parts) => new
                {
                    parts.X,
                    parts.Y
                })
                .Where(part => xList.Contains(part.X) && yList.Contains(part.Y))
                .AsNoTracking();

            if (collisions.Any())
            {
                throw new ShipCollisionException(collisions.Select(x => $"[{x.X},{x.Y}]").ToList());
            }
            
            // Safe to insert the new Ship
            var ship = new Ship
            {
                Board = board
            };
            _context.Ships.Add(ship);

            _context.ShipParts.AddRange(request.ShipParts
                .Select(parts => new ShipPart 
                {
                    Ship = ship,
                    X = parts.X,
                    Y = parts.Y
                })
            );

            await _context.SaveChangesAsync(cancellationToken);

            var viewModel = _mapper.Map<ShipViewModel>(ship);

            return viewModel;
        }
    }
}
