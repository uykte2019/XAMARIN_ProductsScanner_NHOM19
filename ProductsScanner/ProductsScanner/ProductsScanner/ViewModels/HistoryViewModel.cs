namespace ProductsScanner.ViewModels
{
    using ProductsScanner.Services.DataService;
    using ProductsScanner.Services.DataService.DbModels;
    using ProductsScanner.Services.ShareService;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;

    internal class HistoryViewModel : BaseViewModel
    {
        private const string title = "History";
        private string searchText = string.Empty;

        public HistoryViewModel()
        {
            Initialize();
        }

        public ObservableCollection<HistoryModel> HistoryItems { get; set; }
        public Command<string> LoadHistoryCommand { get; set; }
        public Command<string> ShareCommand { get; set; }
        public Command<string> OpenBrowserCommand { get; set; }
        public Command<string> RemoveCommand { get; set; }

        public string SearchText
        {
            get { return searchText; }
            set
            {
                SetProperty(ref searchText, value);
                LoadHistoryCommand.Execute(value);
            }
        }

        private void Initialize()
        {
            this.Title = title;
            this.HistoryItems = new ObservableCollection<HistoryModel>();

            this.OpenBrowserCommand = new Command<string>(
                async (url) => await ExecuteOpenBrowserCommand(url));

            this.ShareCommand = new Command<string>(
                async (shareData) => await ExecuteShareCommand(shareData));

            this.RemoveCommand = new Command<string>(
                (content) => ExecuteRemoveCommand(content));

            this.LoadHistoryCommand = new Command<string>(
                async (searchFilter) => await this.ExecuteLoadHistoryItemsCommand(searchFilter));
        }

        private void ExecuteRemoveCommand(string content)
        {
            var dataService = DependencyService.Get<IDataService>();
            dataService.RemoveHistory(content);

            LoadHistoryCommand.Execute(this.SearchText);
        }

        private async Task ExecuteOpenBrowserCommand(string url)
        {
            var shareService = DependencyService.Get<IShareService>();
            await shareService.OpenBrowser(url);
        }

        /// <summary>
        /// Open share options to share a scanned data
        /// </summary>
        private async Task ExecuteShareCommand(string shareData)
        {
            var shareService = DependencyService.Get<IShareService>();
            await shareService.Share(shareData);
        }

        private async Task ExecuteLoadHistoryItemsCommand(string contentFilter)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                this.HistoryItems.Clear();

                var dataService = DependencyService.Get<IDataService>();
                var savedItems = await dataService.GetAllHistoryAsync();

                var filteredData = !string.IsNullOrWhiteSpace(contentFilter)
                    ? savedItems.Where(item => item.Content.Contains(contentFilter))
                    : savedItems;

                filteredData.ForEach(
                    item => this.HistoryItems.Add(item));
            }
            catch
            {
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}