namespace ProductsScanner.Services.ScannerService
{
    using System;
    using Xamarin.Forms;
    using ZXing;
    using ZXing.Net.Mobile.Forms;

    public class ScannerService : IScannerService
    {
        private ZXingScannerPage scannerPage;
        
        public event EventHandler<Result> OnDataReceived;

        public ZXingScannerPage GetScannerPage(View customOverlay)
        {
            scannerPage = new ZXingScannerPage(customOverlay: customOverlay)
            {
                HasTorch = true
            };

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                OnDataReceived?.Invoke(this, result);
            };

            return scannerPage;
        }

        public void ToggleTorch() => 
            scannerPage.ToggleTorch();
    }
}