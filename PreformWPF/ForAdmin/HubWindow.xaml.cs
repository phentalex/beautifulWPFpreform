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

namespace PreformWPF
{
    /// <summary>
    /// Interaction logic for HubWindow.xaml
    /// </summary>
    public partial class HubWindow : Window
    {
        public HubWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2();
        }

        private void Page4_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3();
        }

        private void Page6_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page6();
        }
    }
}
