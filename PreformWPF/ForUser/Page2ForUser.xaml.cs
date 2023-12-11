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
    /// Логика взаимодействия для Page2ForUser.xaml
    /// </summary>
    public partial class Page2ForUser : Page
    {
        public Page2ForUser()
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
                string querystring = "select id_shipping 'ID отгрузки'," +
                    " status 'Статус'," +
                    " ka_name 'Контрагент'," +
                    " ka_city 'Город'," +
                    " date_format(date_ship, '%d/%m/%Y') 'Дата отгрузки'," +
                    " date_format(date_add(date_ship, interval 30 day), '%d/%m/%Y') 'Дата оплаты'," +
                    " manager 'Менеджер' from preform.shippings";
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

        private void TextToFilterStatus_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = "Статус like '%" + TextToFilterStatus.Text + "%'";
        }

        private void TextToFilterKa_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = "Контрагент like '%" + TextToFilterKa.Text + "%'";
        }

        private void TextToFilterManager_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dvManager = dt.DefaultView;
            dvManager.RowFilter = "Менеджер like '%" + TextToFilterManager.Text + "%'";
        }
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddShippingForUser addsh = new AddShippingForUser();
            addsh.Show();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditShippingForUser EddSh = new EditShippingForUser();
            EddSh.Show();
        }
    }
}
