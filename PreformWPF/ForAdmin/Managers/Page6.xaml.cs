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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PreformWPF
{
    /// <summary>
    /// Логика взаимодействия для Page_6.xaml
    /// </summary>
    public partial class Page6 : Page
    {
        public Page6()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        DataTable dt = new DataTable("managers");

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditManager EditMan = new EditManager();
            EditMan.Show();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddManager AddMan = new AddManager();
            AddMan.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteManager DelMan = new DeleteManager();
            DelMan.Show();
        }

        private void LoadTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string querystring = "select id_manager 'ID'," +
                    " manager_firstname 'Имя'," +
                    " manager_secondname 'Фамилия'," +
                    " manager_phone 'Телефон менеджера'," +
                    " manager_email 'Почта менеджера' from preform.managers";
                MySqlCommand cmd = new MySqlCommand(querystring, con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                dt.Clear();
                adapter.Fill(dt);
                dgManager.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextToFilterName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = "Имя like '%" + TextToFilterName.Text + "%'";
        }

        private void TextToFilterSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = "Фамилия like '%" + TextToFilterSurname.Text + "%'";
        }
    }
}
