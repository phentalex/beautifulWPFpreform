using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Логика взаимодействия для DeleteManager.xaml
    /// </summary>
    public partial class DeleteManager : Window
    {
        public DeleteManager()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        MySqlConnection con = new MySqlConnection();

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Hide();
        }

        private void btn_DeleteManager_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();

                DeleteAuto();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteAuto()
        {
            string deletestring = "delete from preform.managers where id_manager = '" + txtDeleteID.Text + "'";
            MySqlCommand cmd = new MySqlCommand(deletestring, con);
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                MessageBox.Show("Данные успешно удалены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string querystring = "select id_manager 'ID'," +
                    " manager_firstname 'Имя'," +
                    " manager_secondname 'Фамилия'," +
                    " manager_phone 'Телефон'," +
                    " manager_email 'Почта' from preform.managers";
                MySqlCommand cmd = new MySqlCommand(querystring, con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("managers");
                adapter.Fill(dt);
                dgDeleteManager.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
