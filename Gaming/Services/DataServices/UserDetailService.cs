using Gaming.RealmObjects;
using System.Xml.Linq;

namespace Services.DataServices
{
    public class UserDetailService
    {

        internal RealmService _realmService { get; }

        public UserDetailService(RealmService realmService)
        {
            _realmService = realmService;
        }

        public async Task<UserDetails?> GetUserDetails()
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();

            return _realmService.Realm!.All<UserDetails>().FirstOrDefault();
        }

        public async Task Save(UserDetails userDetails)
        {
            if (_realmService.Realm is null) await _realmService.InitializeAsync();

            var originalDetails = await GetUserDetails();

            await _realmService.Realm!.WriteAsync(() =>
            {
                _realmService.Realm.Add(userDetails, originalDetails != null);
            });
        }

    }
}
