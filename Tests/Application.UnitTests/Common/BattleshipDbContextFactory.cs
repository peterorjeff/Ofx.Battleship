using Microsoft.EntityFrameworkCore;
using Ofx.Battleship.Domain.Entities;
using Ofx.Battleship.Persistence;
using System;

namespace Ofx.Battleship.Application.UnitTests.Common
{
    public class BattleshipDbContextFactory
    {
        public static BattleshipDbContext Create()
        {
            var options = new DbContextOptionsBuilder<BattleshipDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new BattleshipDbContext(options);

            context.Database.EnsureCreated();

            context.Games.Add(new Game { GameId = 1 });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(BattleshipDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
