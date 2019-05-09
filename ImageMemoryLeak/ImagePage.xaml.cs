using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;

using ImageMemoryLeak.Interfaces;
using System.IO;

namespace ImageMemoryLeak
{
    /// <summary>
    /// Lógica de interacción para ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page
    {

        public IImpagePageListener Listener { get; set; }

        public ImagePage()
        {
            InitializeComponent();
        }

        private void OnCloseImagePageClick(object sender, RoutedEventArgs e)
        {
            if (Listener != null) Listener.OnCloseImagePage();
        }

        /// <summary>
        /// Load the images in the UI
        /// </summary>
        internal void LoadImage()
        {
            // Generate a blank image with the size of the Image Control
            Bitmap bi = new Bitmap((int)IMGControl.Width, (int)IMGControl.Height);
            Graphics g = Graphics.FromImage(bi);

            // Draw two images and hover a png
            PaintImage(g, new Rectangle(0, 0, bi.Width, bi.Height));
            // Set the new image as source of the Image Control 
            IMGControl.Source = ImageUtils.SourceFromBitmap(bi);
            
            // Dispose elments
            g.Dispose();
            bi.Dispose();
        }

        /// <summary>
        /// Draw images in a canvas
        /// </summary>
        /// <param name="g">Element to draw the images</param>
        /// <param name="imgrect">Size of the canvas</param>
        private void PaintImage(Graphics g, Rectangle imgrect)
        {
            string img1path = @"..\..\Resources\Image1.jpg";
            string img2path = @"..\..\Resources\Image2.jpg";
            string pngPath = @"..\..\Resources\ImageTransparent.png";

            System.Drawing.Image img1 = System.Drawing.Image.FromFile(img1path); 
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(img2path);
            System.Drawing.Image imgPng = System.Drawing.Image.FromFile(pngPath);

            Rectangle destImg1Rectangle = new Rectangle(0, 0, imgrect.Width / 2, imgrect.Height);
            Rectangle destImg2Rectangle = new Rectangle(imgrect.Width / 2, 0, imgrect.Width / 2, imgrect.Height);

            Rectangle source1 = ImageUtils.FitRectangle(new Rectangle(0, 0, img1.Width, img1.Height), destImg1Rectangle);
            Rectangle source2 = ImageUtils.FitRectangle(new Rectangle(0, 0, img2.Width, img2.Height), destImg1Rectangle);

            g.DrawImage(img1, destImg1Rectangle, source1, GraphicsUnit.Pixel);
            g.DrawImage(img2, destImg2Rectangle, source2, GraphicsUnit.Pixel);
            g.DrawImage(imgPng, imgrect, new Rectangle(0, 0, imgPng.Width, imgPng.Height), GraphicsUnit.Pixel);

            img1.Dispose();
            img2.Dispose();
            imgPng.Dispose();
        }
    }
}
