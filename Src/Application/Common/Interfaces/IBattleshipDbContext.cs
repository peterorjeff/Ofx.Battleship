using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Ofx.Battleship.Application.Common.Interfaces
{
    public interface IBattleshipDbContext
    {
        DbSet<Game> Games { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
