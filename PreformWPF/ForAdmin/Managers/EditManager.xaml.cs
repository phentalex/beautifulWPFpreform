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
    /// Логика взаимодействия для EditManager.xaml
    /// </summary>
    public partial class EditManager : Window
    {
        public EditManager()
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
                dgEditManager.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_EditManager_Click(object sender, RoutedEventArgs e)
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

        public void EditAuto()
        {
            try
            {
                string editstring = $"update preform.managers set manager_firstname = @fn, manager_secondname = @sn, " +
                                $"manager_phone = @ph, manager_email = @em where id_manager = '" + txtManagerID.Text + "'";
                MySqlCommand cmd = new MySqlCommand(editstring, con);
                cmd.Parameters.AddWithValue("@fn", txtName.Text);
                cmd.Parameters.AddWithValue("@sn", txtSurname.Text);
                cmd.Parameters.AddWithValue("@ph", txtPhone.Text);
                cmd.Parameters.AddWithValue("@em", txtEmail.Text);
                int a = cmd.ExecuteNonQuery();
                if (a == 1)
                {
                    MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
