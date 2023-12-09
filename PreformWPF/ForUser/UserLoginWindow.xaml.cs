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
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PreformWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UserLoginWindow : Window
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand com = new MySqlCommand();
        MySqlDataReader dr;
        public UserLoginWindow()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnLoginUser_Click(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            if (VerifyUser(txtUsername.Text.Trim(), txtPassword.Password))
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                if (CheckRole(txtUsername.Text.Trim(), txtPassword.Password) == true)
                {
                    MessageBox.Show("Вы успешно вошли как администратор!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    HubWindow Hub = new HubWindow();
                    this.Close();
                    Hub.Show();
                    con.Close();
                }
                else if (CheckRole(txtUsername.Text.Trim(), txtPassword.Password) == false)
                {
                    MessageBox.Show("Вы успешно вошли как пользователь!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserHubWindow Hub = new UserHubWindow();
                    this.Close();
                    Hub.Show();
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckRole(string username, string password)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            com.Connection = con;
            com.CommandText = "select id_role from preform.users where user_login = '" + username + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToInt32(dr["id_role"]) == 1)
                {
                    return true;
                }
                else if (Convert.ToInt32(dr["id_role"]) == 2)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool VerifyUser(string username, string password)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            com.Connection = con;
            com.CommandText = "select status from users where user_login='" + username + "' and user_pass='" + password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                if (Convert.ToBoolean(dr["status"]) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
