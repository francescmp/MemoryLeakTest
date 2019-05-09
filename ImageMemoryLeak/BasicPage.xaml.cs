using ImageMemoryLeak.Interfaces;
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

namespace ImageMemoryLeak
{
    /// <summary>
    /// Lógica de interacción para BasicPage.xaml
    /// </summary>
    public partial class BasicPage : Page
    {

        public IBasicPageListener Listener { get; set; }

        public BasicPage()
        {
            InitializeComponent();
        }

        private void OnOpenImagePageClick(object sender, RoutedEventArgs e)
        {
            if (Listener != null) Listener.OnOpenImagePage();
        }
    }
}
