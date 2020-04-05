using System.Diagnostics;
using System.Net;
using System.Text;

namespace FoodStore.Infrastructure.Online
{
    public static class WebWorker
    {
        public static string DownloadPage(string url)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");
            client.Encoding = Encoding.UTF8;

            Debug.WriteLine($"Download {url}");

            var response = client.DownloadString(url);

            return response;
        }
    }
}