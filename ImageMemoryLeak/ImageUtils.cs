
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace ImageMemoryLeak
{
    public class ImageUtils
    {

        public static Rectangle FitRectangle(Rectangle rect, Rectangle proportion)
        {
            int width = Math.Min(rect.Width, (int)(rect.Height * proportion.Width / (double)proportion.Height));
            int height = Math.Min(rect.Height, (int)(rect.Width * proportion.Height / (double)proportion.Width));
            int x = (rect.Width - width) / 2;
            int y = (rect.Height - height) / 2;
            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Encaja el rectángulo B dentro del rectángulo A
        /// </summary>
        /// <param name="aWidth">Ancho del rectángulo A</param>
        /// <param name="aHeight">Alto del rectángulo A</param>
        /// <param name="bWidth">Ancho del rectángulo B</param>
        /// <param name="bHeight">Alto del rectángulo B</param>
        /// <returns></returns>
        public static Rectangle FitRectangle(double aWidth, double aHeight, double bWidth, double bHeight)
        {
            double width = Math.Min(aWidth, (int)(aHeight * bWidth / bHeight));
            double height = Math.Min(aHeight, (int)(aWidth * bHeight / bWidth));
            double x = (aWidth - width) / 2;
            double y = (aHeight - height) / 2;
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
        

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Convierte un Bitmap en un BitmapSource.
        /// Se debe usar Dispose() con el source, este método no lo hace.
        /// </summary>
        /// <param name="source">Bitmap a convertir</param>
        /// <returns>Bitmap convertido en source</returns>
        public static BitmapSource SourceFromBitmap(Bitmap bmp)
        {
            var hbm = bmp.GetHbitmap();
            try
            {
                BitmapSource result = Imaging.CreateBitmapSourceFromHBitmap(hbm, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                return result;
            }
            finally { DeleteObject(hbm); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmapsource"></param>
        /// <returns></returns>
        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }

        
    }
}
