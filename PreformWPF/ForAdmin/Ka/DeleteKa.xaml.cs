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
    /// Логика взаимодействия для DeleteKa.xaml
    /// </summary>
    public partial class DeleteKa : Window
    {
        public DeleteKa()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        MySqlConnection con = new MySqlConnection();

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string querystring = "select id_ka 'ID'," +
                    " ka_name 'Контрагент'," +
                    " ka_city 'Город' from preform.ka";
                MySqlCommand cmd = new MySqlCommand(querystring, con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("kas");
                adapter.Fill(dt);
                dgDeleteKa.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void btn_DeleteKa_Click(object sender, RoutedEventArgs e)
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
            string deletestring = "delete from preform.ka where id_ka = '" + txtDeleteID.Text + "'";
            MySqlCommand cmd = new MySqlCommand(deletestring, con);
            int i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                MessageBox.Show("Данные успешно удалены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
