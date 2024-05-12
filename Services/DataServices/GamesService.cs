using Gaming.RealmObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices
{
    public class GamesService(RealmService realmService, ConsoleService consoleService)
    {


        public async Task SaveGame(Game game, bool isUpdate)
        {
            if (realmService.Realm is null) await realmService.InitializeAsync();
            
            await realmService.Realm!.WriteAsync(() =>
            {                
                realmService.Realm.Add(game, isUpdate);
            });

            if (!isUpdate)
            {
                await realmService.Realm!.WriteAsync(() =>
                {
                    var console = realmService.Realm!.All<ConsoleType>()
                    .Where(record => record.Id == game.ConsoleId!.Id)
                    .FirstOrDefault();

                    var consoleGameCount = realmService.Realm!.All<Game>()
                    .Where(record => record.ConsoleId == game.ConsoleId)
                    .Count();

                    console!.GameCount = consoleGameCount;
                    realmService.Realm.Add(console, true);
                });
            }
            
        }

        public async Task<Game?> GetGameById(ObjectId? gameId)
        {
            if (realmService.Realm is null) await realmService.InitializeAsync();

            if(gameId is null) { return null; }

            return realmService.Realm!.All<Game>()
                .FirstOrDefault(record => record.Id == gameId);

        }

        public async Task<IQueryable<Game>> GetGames(ObjectId? consoleId)
        {
            if (realmService.Realm is null) await realmService.InitializeAsync();

            var console = await consoleService.GetConsoleById(consoleId);

            var allGames = realmService.Realm!.All<Game>();

            if(console is not null)
            {
                allGames = allGames
                    .Where(record => record.ConsoleId != null && record.ConsoleId == console);
            }

            return allGames
                .OrderBy(record => record.Name);
        }

    }
}
