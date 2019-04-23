namespace WebApiToken.Services
{
    public interface IAppsService
    {
        AppKeySetting[] GetAppKeySettings();
        AppKeySetting GetAppKeySettingByAppName(string appName);
        string GetAppKeyByAppName(string appName);
    }
}