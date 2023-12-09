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
using System.Windows.Shapes;

namespace PreformWPF
{
    /// <summary>
    /// Логика взаимодействия для UserHubWindow.xaml
    /// </summary>
    public partial class UserHubWindow : Window
    {
        public UserHubWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page2ForUser();
        }

        private void Page4_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page3ForUser();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Page6_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Page6ForUser();
        }
    }
}
