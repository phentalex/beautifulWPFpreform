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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }
        MySqlConnection con = new MySqlConnection();

        private void LoadTable_Click(object sender, RoutedEventArgs e)
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
                DataTable dt = new DataTable("shippings");
                adapter.Fill(dt);
                dgShipping.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddKa AddKa = new AddKa();
            AddKa.Show();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditKa EditKa = new EditKa();
            EditKa.Show();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteKa DelKa = new DeleteKa();
            DelKa.Show();
        }
    }
}
