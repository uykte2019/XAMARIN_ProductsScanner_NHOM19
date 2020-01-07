using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProductsScanner.Views;
using ProductsScanner.Services.ScannerService;
using ZXing.Net.Mobile.Forms;
using ProductsScanner.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProductsScanner
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
