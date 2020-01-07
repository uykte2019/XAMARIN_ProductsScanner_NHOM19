namespace ProductsScanner.ViewModels
{
    using ProductsScanner.Services.DataService;
    using ProductsScanner.Services.ShareService;
    using Xamarin.Forms;
    using ZXing;

    public class ScannedDataViewModel : BaseViewModel
    {
        private readonly Result result;

        public ScannedDataViewModel(Result result)
        {
            this.result = result;
            Initialize();
        }

        public string ScannedData { get; set; }

        public Command SaveToHistoryCommand { get; set; }
        public Command ReturnToScannerCommand { get; set; }
        public Command ShareCommand { get; set; }
        public Command GoogleSearchCommand { get; set; }
        public Command OpenInBrowserCommand { get; set; }

        /// <summary>
        /// Initialize ViewModel's binding components
        /// </summary>
        private void Initialize()
        {
            this.ScannedData = this.result.Text ?? string.Empty;
            
            this.SaveToHistoryCommand = new Command(() => ExecuteSaveToHistoryCommand());
            this.ReturnToScannerCommand = new Command(() => ExecuteReturnToScannerCommand());
            this.ShareCommand = new Command(() => ExecuteShareCommand());
            this.GoogleSearchCommand = new Command(() => ExecuteGoogleSearchCommand());
            this.OpenInBrowserCommand = new Command(() => ExecuteOpenInBrowserCommand());
        }

        private void ExecuteOpenInBrowserCommand()
        {
            var shareService = DependencyService.Get<IShareService>();
            shareService.OpenBrowser(this.ScannedData);
        }

        private void ExecuteGoogleSearchCommand()
        {
            var googleSearchLink = "https://www.google.com/search?q=";
            var searchData = $"{googleSearchLink}{this.ScannedData}";
            var shareService = DependencyService.Get<IShareService>();
            shareService.OpenBrowser(searchData);
        }

        /// <summary>
        /// Open share options to share a scanned data
        /// </summary>
        private void ExecuteShareCommand()
        {
            var shareService = DependencyService.Get<IShareService>();
            shareService.Share(this.ScannedData);
        }

        /// <summary>
        /// Saves scanned data to the local database
        /// </summary>
        private void ExecuteSaveToHistoryCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var dataService = DependencyService.Get<IDataService>();
                dataService.SaveHistory(this.ScannedData);
            }
            catch { }
            finally
            {
                IsBusy = false;
                ExecuteReturnToScannerCommand();
            }
        }

        /// <summary>
        /// Passes a message to run the scanner
        /// </summary>
        private void ExecuteReturnToScannerCommand() =>
            MessagingCenter.Send(this, "RunScanner");
    }
}
