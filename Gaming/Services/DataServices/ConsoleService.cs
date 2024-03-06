using Gaming.RealmObjects;
using MongoDB.Bson;

namespace Services.DataServices
{
    public class ConsoleService
    {

        internal RealmService _realmService { get; }

        public ConsoleService(RealmService realmService) { 
            _realmService = realmService;
        } 

        public async Task Update(ConsoleType console)
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();
            
            await _realmService.Realm!.WriteAsync(() =>
            {
                _realmService.Realm.Add(console, true);
            });
        }

        public async Task<ConsoleType?> GetConsoleById(ObjectId? consoleId)
        {
            if (consoleId == null) return null;
            if (_realmService.Realm is null) await _realmService.InitializeAsync();
            return _realmService.Realm!.All<ConsoleType>()
                .FirstOrDefault(record  => record.Id == consoleId);
        }

        public async Task<IQueryable<ConsoleType>> GetConsoles()
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();
            return _realmService.Realm!.All<ConsoleType>()
                .OrderBy(record => record.Name);
        }

        public async Task<IQueryable<ConsoleType>> GetActiveConsoles()
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();
            return _realmService.Realm!.All<ConsoleType>()
                .Where(record => record.Enabled)
                .OrderBy(record => record.Name);
        }

    }
}
