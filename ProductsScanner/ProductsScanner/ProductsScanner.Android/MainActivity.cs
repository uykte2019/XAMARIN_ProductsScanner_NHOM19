namespace ProductsScanner.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Xamarin.Forms;
    using ProductsScanner.Services.ScannerService;
    using Xamarin.Forms.Platform.Android;
    using ZXing.Mobile;
    using ProductsScanner.Services.DataService;
    using System.IO;
    using Android.Runtime;
    using ProductsScanner.Services.ShareService;
    using Android.Gms.Ads;
    using ZXing.Net.Mobile.Android;
    using ProductsScanner.Services.SettingsService;

    [Activity(
        Label = "Products Scanner",
        Icon = "@drawable/appIcon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            Forms.Init(this, bundle);

            RegisterServices();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(
            int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void RegisterServices()
        {
            //MobileAds.Initialize(ApplicationContext, "YOUR APP ID HERE FROM AdMob, has a ~ in it");
            MobileBarcodeScanner.Initialize(Application);
            this.DeployLocalDatabase();

            DependencyService.Register<IScannerService, ScannerService>();
            DependencyService.Register<IShareService, ShareService>();
            DependencyService.Register<IDataService, DataService>();
            DependencyService.Register<ISettingsService, SettingsService>();
        }

        public void DeployLocalDatabase()
        {
            var sqliteFilename = "scannerdb.db3";
            string personalFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(personalFolder, sqliteFilename);
            
            if (!File.Exists(path))
            {
                using (var reader = new BinaryReader(Android.App.Application.Context.Assets.Open(sqliteFilename)))
                {
                    using (var writer = new BinaryWriter(new FileStream(path, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            writer.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}