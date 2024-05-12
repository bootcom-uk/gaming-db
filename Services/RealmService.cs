using Gaming.RealmObjects;
using Realms.Sync;
using Services.Config;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Services
{
    public class RealmService(byte[] encryptionKey)
    {

        public Realms.Realm? Realm { get; private set; }

        internal Guid? _userId;

        public void SetUserId(Guid userId)
        {
            _userId = userId;
        }

        public async Task InitializeAsync()
        {
            if (Realm != null) return;

            var assembly = Assembly.GetExecutingAssembly();

            var appSettingsStream = null as Stream;

#if DEBUG
            appSettingsStream = assembly.GetManifestResourceStream("Services.appsettings.development.json");

#else
            appSettingsStream = assembly.GetManifestResourceStream("Services.appsettings.json");
#endif

            var appSettings = await JsonSerializer.DeserializeAsync<AppSettings>(appSettingsStream!);

            var app = Realms.Sync.App.Create(new AppConfiguration(appSettings!.RealmDetails.AppId));

            var usernamePasswordCreds = Credentials.EmailPassword(appSettings!.RealmDetails.EmailAddress, appSettings!.RealmDetails.Password);

            var user = await app.LogInAsync(usernamePasswordCreds);

            var config = new FlexibleSyncConfiguration(app.CurrentUser!);
            config.Schema = new[] { typeof(ConsoleType), typeof(Game), typeof(GamePrice), typeof(GamePriceCheck), typeof(ItemStore), typeof(CeXGame), typeof(CeXGame_SellPrices), typeof(UserDetails), typeof(CeXWishlist) };

            config.EncryptionKey = encryptionKey;

            Realm = Realms.Realm.GetInstance(config);

            var path = Realm.Config.DatabasePath;

            Realm.Subscriptions.Update(() =>
            {
                Realm.Subscriptions.Add(Realm.All<ConsoleType>(), new() { Name = "Console Types" });
                Realm.Subscriptions.Add(Realm.All<Game>().Where(record => record.UserId == _userId), new() { Name = "Games" });
                Realm.Subscriptions.Add(Realm.All<GamePriceCheck>().Where(record => record.UserId == _userId), new() { Name = "Game Price Checks" });
                Realm.Subscriptions.Add(Realm.All<CeXGame>(), new() { Name = "CeX Game listings" });
                Realm.Subscriptions.Add(Realm.All<UserDetails>().Where(record => record.UserId == _userId), new() { Name = "User Details" });
                Realm.Subscriptions.Add(Realm.All<CeXWishlist>().Where(record => record.UserId == _userId), new() { Name = "CeX Wishlist" });
            });

            try
            {
                await Realm.Subscriptions.WaitForSynchronizationAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }

    }
}
