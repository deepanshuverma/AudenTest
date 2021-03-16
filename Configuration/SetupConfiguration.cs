using Microsoft.Extensions.Configuration;

namespace AudenTest.Configuration
{
    public class SetupConfiguration
    {
        private static AppSettings _appSettings;
        public static AppSettings AppSettings { get; } = _appSettings ?? GetAppSettingsFromJson();

        private static AppSettings GetAppSettingsFromJson()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", false, false);
            var settings = builder.Build().GetSection("AppSettings").Get<AppSettings>();

            return settings;
        }


    }
}
