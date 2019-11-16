using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ofx.Battleship.Application.Common.Interfaces;
using System;

namespace Ofx.Battleship.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<BattleshipDbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IBattleshipDbContext>(provider => provider.GetService<BattleshipDbContext>());

            return services;
        }
    }
}
