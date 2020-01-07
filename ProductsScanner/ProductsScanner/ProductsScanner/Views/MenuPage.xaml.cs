namespace ProductsScanner.Views
{
    using ProductsScanner.Models;
    using ProductsScanner.Services.ShareService;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        private MainPage RootPage => Application.Current.MainPage as MainPage;
        private List<Models.MenuItem> menuItems;

        public string MenuHeader { get; set; }

        public MenuPage()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            menuItems = new List<Models.MenuItem>
            {
                new Models.MenuItem {Id = MenuItemType.Scanner, Title="Scanner" },
                new Models.MenuItem {Id = MenuItemType.History, Title="History" },
                new Models.MenuItem {Id = MenuItemType.Settings, Title="Settings" },
                new Models.MenuItem {Id = MenuItemType.Share, Title="Share" }
            };

            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];
            
            ListViewMenu.ItemTapped += async (s, e) =>
            {
                await ListViewMenu_ItemTapped(s, e);
            };
        }

        private async Task ListViewMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            
            var id = (int)(e.Item as Models.MenuItem)?.Id;
            if (id == (int)MenuItemType.Share)
            {
                await DependencyService.Get<IShareService>().ShareApp();
                return;
            }

            await RootPage.NavigateFromMenu(id);
        }
    }
}