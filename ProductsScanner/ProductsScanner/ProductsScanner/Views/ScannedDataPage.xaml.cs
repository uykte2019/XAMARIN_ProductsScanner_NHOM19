namespace ProductsScanner.Views
{
    using ProductsScanner.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ZXing;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedDataPage : ContentPage
    {
        private ScannedDataViewModel viewModel;

        public ScannedDataPage(Result result)
        {
            InitializeComponent();
            
            this.viewModel = new ScannedDataViewModel(result);
            BindingContext = viewModel;
        }
        
        protected override bool OnBackButtonPressed()
        {
            viewModel.ReturnToScannerCommand.Execute(null);
            return true;
        }
    }
}