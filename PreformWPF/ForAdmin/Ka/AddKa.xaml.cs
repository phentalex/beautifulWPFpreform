using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для AddKa.xaml
    /// </summary>
    public partial class AddKa : Window
    {
        public AddKa()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        MySqlConnection con = new MySqlConnection();
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Hide();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_AddKa_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();

                InsertAuto();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void InsertAuto()
        {
            string querystring = "insert into preform.ka(ka_name, ka_city) values ('" + txtKaName.Text + "', '" + txtKaCity.Text + "')";

            MySqlCommand cmd = new MySqlCommand(querystring, con);
            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("Данные успешно добавлены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
