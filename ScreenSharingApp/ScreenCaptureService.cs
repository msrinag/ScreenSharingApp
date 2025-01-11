using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class ScreenCaptureService
{
    public byte[] CaptureScreen()
    {
        // Capture the screen dimensions
        var bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
        Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

        try
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Copy the screen to the bitmap
                graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }

            // Convert the bitmap to a byte array
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        finally
        {
            bitmap.Dispose();
        }
    }
}
