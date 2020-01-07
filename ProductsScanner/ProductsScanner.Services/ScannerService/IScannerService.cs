namespace ProductsScanner.Services.ScannerService
{
    using System;
    using Xamarin.Forms;
    using ZXing;
    using ZXing.Net.Mobile.Forms;

    public interface IScannerService
    {
        event EventHandler<Result> OnDataReceived;

        ZXingScannerPage GetScannerPage(View customOverlay);
        void ToggleTorch();

    }
}