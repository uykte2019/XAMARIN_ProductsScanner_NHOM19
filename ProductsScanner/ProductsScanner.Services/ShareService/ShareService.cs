namespace ProductsScanner.Services.ShareService
{
    using System;
    using System.Threading.Tasks;
    using Plugin.Share;
    using Plugin.Share.Abstractions;
    using ProductsScanner.Services.Helpers;

    public class ShareService : IShareService
    {
        public bool SupportsClipboard => CrossShare.Current.SupportsClipboard;

        public bool CanOpenUrl(string url)
        {
            return CrossShare.Current.CanOpenUrl(url);
        }

        public Task<bool> OpenBrowser(string url)
        {
            if (CrossShare.Current.CanOpenUrl(url))
            {
                return CrossShare.Current.OpenBrowser(url, new BrowserOptions());
            }

            return Task.FromResult(false);
        }

        public Task<bool> SetClipboardText(string text, string label = null)
        {
            return CrossShare.Current.SetClipboardText(text, label);
        }

        public Task<bool> Share(string message)
        {
            var shareMessage = new ShareMessage();
            var shareOptions = new ShareOptions();

            message += 
                $"{Environment.NewLine}-------------------{Environment.NewLine}Shared by ProductsScanner";

            shareMessage.Text = message;
            shareMessage.Title = "ProductsScanner";

            return CrossShare.Current.Share(shareMessage, shareOptions);
        }
        
        public Task<bool> ShareApp()
        {
            var message = "App is shared!";
            var shareMessage = new ShareMessage { Text = message };

            return CrossShare.Current.Share(shareMessage, new ShareOptions());
        }
    }
}