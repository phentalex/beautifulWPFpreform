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
    /// Логика взаимодействия для EditKa.xaml
    /// </summary>
    public partial class EditKa : Window
    {
        public EditKa()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        MySqlConnection con = new MySqlConnection();

        private void btn_EditKa_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();

                EditAuto();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

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
                dgEditShipping.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EditAuto()
        {
            try
            {
                string editstring = "update preform.ka set ka_name = @name, ka_city = @city where id_ka = '" + txtKaID.Text + "'";
                MySqlCommand cmd = new MySqlCommand(editstring, con);
                cmd.Parameters.AddWithValue("@name", txtKaName.Text);
                cmd.Parameters.AddWithValue("@city", txtKaCity.Text);
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
