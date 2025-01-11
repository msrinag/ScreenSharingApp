using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSharingApp
{
    public class ScreenServer
    {
        private readonly string _url;
        private HttpListener _httpListener;
        private CancellationTokenSource _cancellationTokenSource;

        public ScreenServer(string url)
        {
            _url = url;
            _httpListener = new HttpListener();
        }

        public void Start()
        {
            // Start the HTTP server
            _httpListener.Prefixes.Add(_url);
            _httpListener.Start();
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => HandleRequests(_cancellationTokenSource.Token));
        }

        public void Stop()
        {
            // Stop the HTTP server
            _httpListener.Stop();
            _cancellationTokenSource.Cancel();
        }

        private async Task HandleRequests(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var context = await _httpListener.GetContextAsync();
                    var response = context.Response;

                    // Set the content type for streaming images
                    response.ContentType = "multipart/x-mixed-replace; boundary=frame";
                    var outputStream = response.OutputStream;

                    // Stream the screen capture continuously
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        byte[] screenData = CaptureScreen();
                        var header = $"--frame\r\nContent-Type: image/png\r\nContent-Length: {screenData.Length}\r\n\r\n";
                        var headerBytes = System.Text.Encoding.UTF8.GetBytes(header);

                        // Send the header and image frame
                        await outputStream.WriteAsync(headerBytes, 0, headerBytes.Length);
                        await outputStream.WriteAsync(screenData, 0, screenData.Length);
                        await outputStream.FlushAsync();

                        // Wait before capturing the next frame (adjust for desired frame rate)
                        await Task.Delay(100); // 10 FPS
                    }

                    outputStream.Close();
                }
                catch (Exception ex)
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }

        private byte[] CaptureScreen()
        {
            // Capture the screen as a byte array
            using (var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}
