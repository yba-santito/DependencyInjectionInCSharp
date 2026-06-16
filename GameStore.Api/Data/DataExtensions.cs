using GameStore.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

            dbContext.Database.Migrate();
        }

        public static void AddGameStoreDb(this WebApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetConnectionString("GameStore");
            //    DBCOntext has a scoped service lifetime because:
            //    1. It ensurs that a new instance of DbCntext is created per request
            //    2. DB Connnections are a limited and expensive resources
            //    3. dbcontext is not thread-safe. Scope avoids to concurrency issues
            //    4. Makes it easier to manage transactions andd ensure data consistency
            //    5. Reusing a DbContext instance can lead to increased memory usage

            builder.Services.AddSqlite<GameStoreContext>(
                connString,
                optionsAction: options =>
                    options.UseSeeding(
                        (context, _) =>
                        {
                            if (!context.Set<Genre>().Any())
                            {
                                context
                                    .Set<Genre>()
                                    .AddRange(
                                        new Genre { Name = "Fighting" },
                                        new Genre { Name = "Shooter" },
                                        new Genre { Name = "RPG" },
                                        new Genre { Name = "Platformer" },
                                        new Genre { Name = "Racing" },
                                        new Genre { Name = "Sports" }
                                    );

                                context.SaveChanges();
                            }
                        }
                    )
            );
        }
    }
};
