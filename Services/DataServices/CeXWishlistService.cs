using Gaming.RealmObjects;

namespace Services.DataServices
{
    public class CeXWishlistService
    {
        internal RealmService _realmService { get; }

        public CeXWishlistService(RealmService realmService) { 
            _realmService = realmService;
        }

        public async Task<IQueryable<CeXWishlist>> GetWishlistItems()
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();

            return _realmService.Realm!.All<CeXWishlist>()
                .OrderBy(record => record.BoxId!.Name);
        }

    }
}
