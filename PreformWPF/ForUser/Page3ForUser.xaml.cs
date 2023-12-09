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
    /// Логика взаимодействия для Page3ForUser.xaml
    /// </summary>
    public partial class Page3ForUser : Page
    {
        public Page3ForUser()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        }

        MySqlConnection con = new MySqlConnection();

        DataTable dt = new DataTable("shippings");

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
                dt.Clear();
                adapter.Fill(dt);
                dgShipping.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextToFilterKa_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = "Контрагент like '%" + TextToFilterKa.Text + "%'";
        }
    }
}
