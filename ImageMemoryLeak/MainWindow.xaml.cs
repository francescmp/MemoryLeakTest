using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageMemoryLeak.Interfaces;
using System.Diagnostics;


namespace ImageMemoryLeak
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IBasicPageListener, IImpagePageListener
    {
        BasicPage BasicP = null;
        ImagePage ImageP = null;

        int Times = 0;
        long MaxMemory = 0;

        // --------------------------------------------------------------
        // CHANGE THE VALUE OF THIS BOOL TO CAUSE MEMORY LEAK OR AVOID IT
        bool CauseMemoryLeak = true;
        // --------------------------------------------------------------



        public MainWindow()
        {
            InitializeComponent();

            // Initially load the basic Page
            BasicP = new BasicPage();
            BasicP.Listener = this;
            FrameMainWindow.Content = BasicP;

            // Update the value of used memory
            LBLMemory.Content = GetUseOfMemory(); 
        }

        /// <summary>
        /// It's called when the ImagePage is Closed
        /// </summary>
        public void OnCloseImagePage()
        {
            FrameMainWindow.Content = BasicP;
        }

        /// <summary>
        /// It's calle when the button to open the ImagePage is Clicked
        /// </summary>
        public void OnOpenImagePage()
        {
            // To cause memory Leak we generate every time the Page
            if (CauseMemoryLeak)
            {
                ImagePage ip = new ImagePage();
                ip.Listener = this;
                ip.LoadImage();
                FrameMainWindow.Content = ip;
            }
            else // To avoid the memory leak we generate the page only Once
            {
                if (ImageP == null)
                {
                    ImageP = new ImagePage();
                    ImageP.Listener = this;
                }
                // Load the image
                ImageP.LoadImage();
                // Change de Frame content
                FrameMainWindow.Content = ImageP;
            }
           

            // Update the times we've opened the window
            Times++;
            LBLTimes.Content = "Times:" + Times;
            // Update the value of used memory
            LBLMemory.Content = GetUseOfMemory();
        }

        private string GetUseOfMemory()
        {
            //return "Use of memory: " + (GC.GetTotalMemory(false) / 1000000) + " mb";
            long memory = +Process.GetCurrentProcess().PrivateMemorySize64;
            if (memory > MaxMemory)
            {
                MaxMemory = memory;
                LBLMaxMemory.Content = "Max use of memory: " + MaxMemory / 1000000;
            }
            return "Use of memory: " + memory / 1000000 + " mb"; ;

        }
    }
}
