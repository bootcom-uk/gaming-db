using Gaming.RealmObjects;
using MongoDB.Bson;

namespace Services.DataServices
{
    public class CeXService
    {

        internal RealmService _realmService { get; }

        public CeXService(RealmService realmService)
        {
            _realmService = realmService;
        }

        public async Task<CeXGame?> SaveCeXBoxInformation(string? boxId, int igdbId, string name)
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();

            if (string.IsNullOrWhiteSpace(boxId))
            {
                return null;
            }

            var cexBoxId = await _realmService.Realm!.SyncSession.User.Functions.CallAsync<ObjectId>("CreateCexBoxDetailIfNotExists", new[] { boxId });

           var cexBox = _realmService.Realm!.All<CeXGame>()
                .FirstOrDefault(record => record.BoxId == boxId);

            return cexBox;
        }

    }
}
