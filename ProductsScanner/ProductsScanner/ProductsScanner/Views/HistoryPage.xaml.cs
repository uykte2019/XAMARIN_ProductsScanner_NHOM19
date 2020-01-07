namespace ProductsScanner.Views
{
    using ProductsScanner.Services.ShareService;
    using ProductsScanner.ViewModels;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel viewModel;

        public HistoryPage()
        {
            InitializeComponent();
            LoadBindingContext();
            LoadEvents();
            Title = "Saved History";
        }

        private void LoadEvents()
        {
            Appearing += HistoryPage_Appearing;
        }

        private void LoadBindingContext()
        {
            this.viewModel = new HistoryViewModel();
            BindingContext = this.viewModel;
        }

        private void HistoryPage_Appearing(object sender, EventArgs e) =>
            this.viewModel.LoadHistoryCommand.Execute(default(string));

        private async void BtnOpenBrowser_Clicked(object sender, EventArgs e)
        {
            var url = (sender as Button)?.CommandParameter.ToString();

            var isWellFormated = Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            if (!isWellFormated)
            {
                await DisplayAlert(
                    "Invalid URL",
                    "It's not a well formated URL address!",
                    "OK");

                return;
            }

            var wantToOpen = await DisplayAlert(
                "Are you sure want to open the link?",
                url,
                "Yes",
                "No");

            if (wantToOpen)
            {
                this.viewModel.OpenBrowserCommand.Execute(url);
            }
        }

        private async void BtnRemove_Clicked(object sender, EventArgs e)
        {
            var data = (sender as Button)?.CommandParameter.ToString();

            var wantToRemove = await DisplayAlert(
                "Are you sure want to remove?",
                data,
                "Yes",
                "No");

            if (wantToRemove)
            {
                this.viewModel.RemoveCommand.Execute(data);
            }
        }

        private void SetButtonColor(Button button, bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                button.BackgroundColor = Color.FromHex("#F07929");
                button.TextColor = Color.White;
            }
            else
            {
                button.BackgroundColor = Color.Transparent;
                button.TextColor = Color.FromHex("#F07929");
            }

        }

        private void BtnOpenBrowser_Pressed(object sender, EventArgs e)
        {
            SetButtonColor(sender as Button, true);
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            SetButtonColor(sender as Button, true);
        }

        private void BtnRemove_Pressed(object sender, EventArgs e)
        {
            SetButtonColor(sender as Button, true);
        }

        private void BtnRemove_Released(object sender, EventArgs e)
        {
            SetButtonColor(sender as Button, false);
        }

        private void Button_Released(object sender, EventArgs e)
        {
            SetButtonColor(sender as Button, false);
        }

        private void BtnOpenBrowser_Released(object sender, EventArgs e)
        {
            SetButtonColor(sender as Button, false);
        }
    }
}