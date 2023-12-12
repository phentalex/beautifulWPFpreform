using MySql.Data.MySqlClient;
using PreformWPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PreformWPF
{
    /// <summary>
    /// Логика взаимодействия для Page7.xaml
    /// </summary>
    public partial class Page7 : Page
    {
        public Page7()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        DataTable dt = new DataTable("users");

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddUser AddU = new AddUser();
            AddU.Show();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditUser EdU = new EditUser();
            EdU.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser DelU = new DeleteUser();
            DelU.Show();
        }

        private void LoadTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string querystring1 = "select users.id_user 'ID', users.user_login 'Логин', users.user_pass 'Пароль', users.status 'Активность', " +
                    "user_roles.role_name as 'Роль' from preform.users " +
                    "left join preform.user_roles on users.id_role = user_roles.id_role;";
                MySqlCommand cmd1 = new MySqlCommand(querystring1, con);
                cmd1.ExecuteNonQuery();

                MySqlDataAdapter adapter1 = new MySqlDataAdapter(cmd1);
                dt.Clear();
                adapter1.Fill(dt);
                dgUser.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextToFilterLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvUser = dt.DefaultView;
            dvUser.RowFilter = "Логин like '%"+TextToFilterLogin.Text+"%'";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
