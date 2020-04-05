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
            client.Encoding = Encoding.UTF8;

            Debug.WriteLine($"Download {url}");

            var response = client.DownloadString(url);

            return response;
        }
    }
}