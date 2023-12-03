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
            string querystring = "insert into shippings(status, ka_name, ka_city, date_ship, date_pay, manager)" +
                "values (@st, @ka, @city, @ds, @dp, @mn)";

            MySqlCommand cmd = new MySqlCommand(querystring, con);
            cmd.Parameters.AddWithValue("@st", listStatus.Text);
            cmd.Parameters.AddWithValue("@ka", listKa.Text);
            cmd.Parameters.AddWithValue("@mn", listManager.Text);
            cmd.Parameters.AddWithValue("@city", txtCity.Text);
            cmd.Parameters.AddWithValue("@ds", DatePickerShip.SelectedDate.Value);

            DateTime datepay = DatePickerShip.SelectedDate.Value;
            datepay = datepay.AddDays(30);
            cmd.Parameters.AddWithValue("@dp", datepay);
            

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

        private void listKa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string selectstringCity = $"select ka_city from preform.ka where ka_name= '{listKa.SelectedItem}'";
            MySqlCommand cmdSELcity = new MySqlCommand(selectstringCity, con);
            MySqlDataReader drSELcity = cmdSELcity.ExecuteReader();
            while (drSELcity.Read())
            {
                txtCity.Text = drSELcity.GetValue(0).ToString();
            }
            con.Close();
        }

        private void DatePickerShip_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime datepay = DatePickerShip.SelectedDate.Value;
            datepay = datepay.AddDays(30);
            txtDatePay.Text = datepay.ToString("dd//MM/yyyy");
        }
    }
}
