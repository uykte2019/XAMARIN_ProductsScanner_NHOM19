namespace ProductsScanner.Services.ShareService
{
    using System.Threading.Tasks;

    public interface IShareService
    {
        bool SupportsClipboard { get; }
        bool CanOpenUrl(string url);
        Task<bool> OpenBrowser(string url);
        Task<bool> SetClipboardText(string text, string label = null);
        Task<bool> Share(string message);
        Task<bool> ShareApp();
    }
}