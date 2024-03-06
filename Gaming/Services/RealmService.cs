using Gaming.RealmObjects;
using Realms.Sync;
using Services.Config;
using System.Reflection;
using System.Text.Json;

namespace Services
{
    public class RealmService(byte[] encryptionKey)
    {

        private Realms.Realm _realm { get; set; }

        public Realms.Realm Realm
        {
            get { return _realm; }
        }

        internal Guid? _userId;

        public void SetUserId(Guid userId)
        {
            _userId = userId;
        }

        public async Task InitializeAsync()
        {
            if (Realm != null) return;

            var assembly = Assembly.GetExecutingAssembly();
            var appSettingsStream = assembly.GetManifestResourceStream("Services.appsettings.json");
            var appSettings = await JsonSerializer.DeserializeAsync<AppSettings>(appSettingsStream!);

            var app = Realms.Sync.App.Create(new AppConfiguration(appSettings!.RealmDetails.AppId));

            var usernamePasswordCreds = Credentials.EmailPassword(appSettings!.RealmDetails.EmailAddress, appSettings!.RealmDetails.Password);

            var user = await app.LogInAsync(usernamePasswordCreds);

            var config = new FlexibleSyncConfiguration(app.CurrentUser!);
            config.Schema = new[] { typeof(ConsoleType), typeof(Game), typeof(GamePrice), typeof(GamePriceCheck), typeof(ItemStore), typeof(CeXGame), typeof(CeXGame_SellPrices), typeof(UserDetails), typeof(CeXWishlist) };

            config.EncryptionKey = encryptionKey;

            _realm = Realms.Realm.GetInstance(config);

            var path = _realm.Config.DatabasePath;

            _realm.Subscriptions.Update(() =>
            {
                _realm.Subscriptions.Add(_realm.All<ConsoleType>(), new() { Name = "Console Types" });
                _realm.Subscriptions.Add(_realm.All<Game>().Where(record => record.UserId == _userId), new() { Name = "Games" });
                _realm.Subscriptions.Add(_realm.All<GamePriceCheck>().Where(record => record.UserId == _userId), new() { Name = "Game Price Checks" });
                _realm.Subscriptions.Add(_realm.All<CeXGame>(), new() { Name = "CeX Game listings" });
                _realm.Subscriptions.Add(_realm.All<UserDetails>().Where(record => record.UserId == _userId), new() { Name = "User Details" });
                _realm.Subscriptions.Add(_realm.All<CeXWishlist>().Where(record => record.UserId == _userId), new() { Name = "CeX Wishlist" });
            });

            try
            {
                await _realm.Subscriptions.WaitForSynchronizationAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }

    }
}
