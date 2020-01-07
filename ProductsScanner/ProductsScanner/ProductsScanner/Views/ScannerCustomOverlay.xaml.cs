namespace ProductsScanner.Views
{
    using ProductsScanner.Services.ScannerService;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerCustomOverlay : ContentView
    {
        private bool isTorchOn;

        public ScannerCustomOverlay()
        {
            InitializeComponent();

            this.btnToggleTorch.Clicked +=
                (s, a) =>
                {
                    isTorchOn = !isTorchOn;
                    btnToggleTorch.Image = new FileImageSource
                    {
                        File = isTorchOn ? "torchOn.png" : "torchOff.png"
                    };
                    
                    DependencyService.Get<IScannerService>().ToggleTorch();
                };
        }
    }
}