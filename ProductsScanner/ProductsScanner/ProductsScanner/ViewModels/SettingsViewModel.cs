namespace ProductsScanner.ViewModels
{
    using ProductsScanner.Enums.Services;
    using ProductsScanner.Services.SettingsService;
    using Xamarin.Forms;

    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            InitializeSettings();
            InitializeCommands();
        }
        
        public bool OpenURLsDirectly { get; set; }
        public bool SaveToHistory { get; set; }

        public Command<bool> SettingOpenURLsDirectlyCommand { get; set; }
        public Command<bool> SettingSaveToHistoryCommand { get; set; }

        private void InitializeSettings()
        {
            this.OpenURLsDirectly =
                DependencyService.Get<ISettingsService>().OpenURLsDirectlySetting;

            this.SaveToHistory =
                DependencyService.Get<ISettingsService>().SaveToHistorySetting;
        }

        private void InitializeCommands()
        {
            this.SettingOpenURLsDirectlyCommand =
                new Command<bool>(ExecuteSettingOpenURLsDirectly);

            this.SettingSaveToHistoryCommand =
                new Command<bool>(ExecuteSettingSaveToHistory);
        }

        private void ExecuteSettingOpenURLsDirectly(bool value)
        {
            DependencyService.Get<ISettingsService>().SetSettingValue(SettingType.OpenURLsDirectly, value);
        }

        private void ExecuteSettingSaveToHistory(bool value)
        {
            DependencyService.Get<ISettingsService>().SetSettingValue(SettingType.SaveToHistory, value);
        }
    }
}