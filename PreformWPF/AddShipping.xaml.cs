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
using MySql.Data.MySqlClient;

namespace PreformWPF
{
    /// <summary>
    /// Логика взаимодействия для AddShipping.xaml
    /// </summary>
    public partial class AddShipping : Window
    {
        public AddShipping()
        {
            InitializeComponent();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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

        MySqlConnection con = new MySqlConnection();

        public void InsertAuto()
        {
            string querystring = "insert into shippings(status, ka_name, ka_city,  manager)" +
                "values (@st, @ka, @city,  @mn)";

            MySqlCommand cmd = new MySqlCommand(querystring, con);

            //var status = txtStatus.Text;
            //var ka = txtKa.Text;
            //var city = txtCity.Text;
            //var dateShip = txtDateShip.Text;
            //var datePay = txtDatePay.Text;
            //var manager = txtManager.Text;


            //NpgsqlParameter paramStatus = new NpgsqlParameter("@st", NpgsqlTypes.NpgsqlDbType.Varchar);
            //paramStatus.Value = status;
            //cmd.Parameters.Add(paramStatus);
            //NpgsqlParameter paramKa = new NpgsqlParameter("@ka", NpgsqlTypes.NpgsqlDbType.Varchar);
            //paramKa.Value = ka;
            //cmd.Parameters.Add(paramKa); 
            //NpgsqlParameter paramCity = new NpgsqlParameter("@city", NpgsqlTypes.NpgsqlDbType.Varchar);
            //paramCity.Value = city;
            //cmd.Parameters.Add(paramCity);
            //NpgsqlParameter paramManager = new NpgsqlParameter("@mn", NpgsqlTypes.NpgsqlDbType.Varchar);
            //paramManager.Value = manager;
            //cmd.Parameters.Add(paramManager);
            //cmd.Parameters.AddWithValue("@ka", ka);
            //cmd.Parameters.AddWithValue("@city", city);

            //DateTime date1 = DateTime.Parse(DatePickerShip.Text);
            //var dateShip = date1.Date;
           // cmd.Parameters.AddWithValue("@dtShip", DatePickerShip.SelectedDate.Value.Date.ToShortDateString());
           // DateTime date2 = DateTime.Parse(txtDatePay.Text);
           //var datePay = date2.Date;
            //cmd.Parameters.AddWithValue("@dtPay", datePay);
            //cmd.Parameters.AddWithValue("@mn", manager);
            cmd.ExecuteNonQuery();
            

            int a = cmd.ExecuteNonQuery();
            if (a == 1)
            {
                MessageBox.Show("Данные успешно добавлены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddShip_Click_1(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();

                InsertAuto();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();

                string querystringStatus = "select status_name from preform.status_shipping";
                string querystringKa = "select ka_name from preform.ka";
                string querystringCity = "select ka_city from preform.ka";
                string querystringManager = "select manager_firstname from preform.managers";

                MySqlCommand cmdStatus = new MySqlCommand(querystringStatus, con);
                MySqlDataReader drStatus = cmdStatus.ExecuteReader();
                while (drStatus.Read())
                {
                    listStatus.Items.Add(drStatus.GetString("status_name"));
                }
                con.Close();

                con.Open();
                MySqlCommand cmdKa = new MySqlCommand(querystringKa, con);
                MySqlDataReader drKa = cmdKa.ExecuteReader();
                while (drKa.Read())
                {
                    listKa.Items.Add(drKa.GetString("ka_name"));
                }
                con.Close();

                con.Open();
                MySqlCommand cmdCity = new MySqlCommand(querystringCity, con);
                MySqlDataReader drCity = cmdCity.ExecuteReader();
                while (drCity.Read())
                {
                    listCity.Items.Add(drCity.GetString("ka_city"));
                }
                con.Close();

                con.Open();
                MySqlCommand cmdManager = new MySqlCommand(querystringManager, con);
                MySqlDataReader drManager = cmdManager.ExecuteReader();
                while (drManager.Read())
                {
                    listManager.Items.Add(drManager.GetString("manager_firstname"));
                }
                con.Close();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
