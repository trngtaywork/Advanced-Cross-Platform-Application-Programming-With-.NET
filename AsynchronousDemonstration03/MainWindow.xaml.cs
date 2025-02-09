using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsynchronousDemonstration03
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        private readonly IEnumerable<string> UrlList = new string[]
        {
            "https://docs.microsoft.com",
            "https://docs.microsoft.com/azure",
            "https://docs.microsoft.com/powershell",
            "https://docs.microsoft.com/dotnet",
            "https://docs.microsoft.com/aspnet/core",
            "https://docs.microsoft.com/windows"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            txtResults.Clear();
            await SumPageSizeAsync();
            txtResults.AppendText($"\nControl returned to {nameof(btnStart_Click)}.");
            btnStart.IsEnabled = true;
        }

        private async Task SumPageSizeAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            int total = 0;
            foreach (string url in UrlList)
            {
                try
                {
                    int contentLength = await ProcessUrlAsync(url);
                    total += contentLength;
                }
                catch (Exception ex)
                {
                    // Handle exceptions and display them in the UI
                    Dispatcher.Invoke(() =>
                    {
                        txtResults.AppendText($"\nError retrieving {url}: {ex.Message}");
                    });
                }
            }
            stopwatch.Stop();
            Dispatcher.Invoke(() =>
            {
                txtResults.AppendText($"\nTotal bytes returned: {total:#,#}");
                txtResults.AppendText($"\nElapsed time:         {stopwatch.Elapsed}\n");
            });
        }

        private async Task<int> ProcessUrlAsync(string url)
        {
            byte[] content = await client.GetByteArrayAsync(url);
            DisplayResults(url, content);
            return content.Length;
        }

        private void DisplayResults(string url, byte[] content)
        {
            Dispatcher.Invoke(() =>
            {
                txtResults.AppendText($"{url,-60} {content.Length,10:#,#}\n");
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            client.Dispose();
            base.OnClosed(e);
        }
    }
}
