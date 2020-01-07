namespace ProductsScanner.Services.SettingsService
{
    using ProductsScanner.Enums.Services;

    public interface ISettingsService
    {
        void SetSettingValue<TValue>(SettingType settingType, TValue value);

        bool OpenURLsDirectlySetting { get; }
        bool SaveToHistorySetting { get; }
    }
}