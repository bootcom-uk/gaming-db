using Mobile.Common.Config;
using System.Reflection;
using System.Text.Json;

namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var appSettingsStream = null as Stream;

#if DEBUG
            appSettingsStream = assembly.GetManifestResourceStream("Mobile.appsettings.development.json");

#else
            appSettingsStream = assembly.GetManifestResourceStream("Mobile.appsettings.json");
#endif

            var appSettings = JsonSerializer.Deserialize<AppSettings>(appSettingsStream!);

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(appSettings.Syncfusion.LicenceKey);
        }
    }
}
