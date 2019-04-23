using Microsoft.Extensions.Options;
using System.Linq;

namespace WebApiToken.Services
{
    public class AppsService : IAppsService
    {
        private readonly IOptions<AppsSetting> _appsSettings;

        public AppsService(IOptions<AppsSetting> appsSettings)
        {
            _appsSettings = appsSettings;
        }

        public string GetAppKeyByAppName(string appName) =>
            GetAppKeySettings().FirstOrDefault(x => x.AppName == appName)?.AppKey;

        public AppKeySetting GetAppKeySettingByAppName(string appName) =>
            GetAppKeySettings().FirstOrDefault(x => x.AppName == appName);

        public AppKeySetting[] GetAppKeySettings() => 
            _appsSettings.Value.AppKeys;
    }

    public class AppsSetting
    {
        public AppKeySetting[] AppKeys { get; set; }
    }

    public class AppKeySetting
    {
        public string AppName { get; set; }
        public string AppKey { get; set; }
    }
}
