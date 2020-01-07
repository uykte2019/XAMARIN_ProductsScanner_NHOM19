namespace ProductsScanner.Services.SettingsService
{
    using DataService;
    using ProductsScanner.Enums.Services;
    using ProductsScanner.Services.DataService.DbModels;
    using System;
    using Xamarin.Forms;

    public class SettingsService : ISettingsService
    {
        public bool OpenURLsDirectlySetting
        {
            get
            {
                var setting = GetSetting(SettingType.OpenURLsDirectly);
                if (setting == null)
                {
                    CreateSetting(SettingType.OpenURLsDirectly, false, SettingsDescriptions.OpenURLsDirectlyDescription);
                    return false;
                }

                return setting.SettingValue == true.ToString();
            }
        }

        public bool SaveToHistorySetting
        {
            get
            {
                var setting = GetSetting(SettingType.SaveToHistory);
                if (setting == null)
                {
                    CreateSetting(SettingType.SaveToHistory, false, SettingsDescriptions.SaveToHistoryDescription);
                    return false;
                }

                return setting.SettingValue == true.ToString();
            }
        }
        
        public void SetSettingValue<TValue>(SettingType settingType, TValue value)
        {
            DependencyService.Get<IDataService>().UpdateSettingToDb(settingType, value);            
        }

        private SettingModel GetSetting(SettingType settingType)
        {
            return DependencyService.Get<IDataService>().GetSettingFromDb(settingType);
        }

        private void CreateSetting<TValue>(SettingType settingType, TValue value, string description)
        {
            DependencyService.Get<IDataService>().InsertToDb(new SettingModel
            {
                SettingId = (int)settingType,
                Description = description,
                SettingValue = value.ToString(),
                LastModifiedDateTicks = DateTime.Now.Ticks
            });
        }
    }
}