namespace ProductsScanner.Views
{
    using ProductsScanner.Models;
    using ProductsScanner.Services.DataService;
    using ProductsScanner.Services.ScannerService;
    using ProductsScanner.Services.SettingsService;
    using ProductsScanner.Services.ShareService;
    using ProductsScanner.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ZXing;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitNavigationPages();
        }

        public Dictionary<int, NavigationPage> MenuPages { get; private set; }
        
        private void InitNavigationPages()
        {
            this.MasterBehavior = MasterBehavior.Popover;

            this.MenuPages = new Dictionary<int, NavigationPage>
            {
                { (int)MenuItemType.Scanner, new NavigationPage(this.LoadScannerOverlay()) },
                { (int)MenuItemType.History, new NavigationPage(new HistoryPage()) },
                { (int)MenuItemType.Settings, new NavigationPage(new SettingsPage()) } 
            };

            this.RunScanner();
        }

        private Page LoadScannerOverlay()
        {
            var scannerService = DependencyService.Get<IScannerService>();
            var scannerPage = scannerService.GetScannerPage(new ScannerCustomOverlay());
            scannerService.OnDataReceived += OnDataReceived;

            MessagingCenter.Subscribe<ScannedDataViewModel>(this, "RunScanner", (dataPage) => RunScanner());

            return scannerPage;
        }

        private void RunScanner() =>
            this.Detail = this.MenuPages[(int)MenuItemType.Scanner];

        private void OnDataReceived(object sender, Result result)
        {
            if (DependencyService.Get<ISettingsService>().SaveToHistorySetting)
            {
                DependencyService.Get<IDataService>().SaveHistory(result.Text);
            }

            if (DependencyService.Get<ISettingsService>().OpenURLsDirectlySetting)
            {
                DependencyService.Get<IShareService>().OpenBrowser(result.Text);
            }
            
            this.Detail = new NavigationPage(new ScannedDataPage(result));
        }
        
        public async Task NavigateFromMenu(int id)
        {
            var newPage = MenuPages[id];

            if (newPage != null)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                this.IsPresented = false;
            }
        }
    }
}