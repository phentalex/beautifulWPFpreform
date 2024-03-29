﻿using Google.Protobuf.Collections;
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
    /// Interaction logic for EditShipping.xaml
    /// </summary>
    public partial class EditShipping : Window
    {
        public EditShipping()
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

        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Hide();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                string querystring = "select id_shipping 'ID'," +
                    " status 'Статус'," +
                    " ka_name 'Контрагент'," +
                    " ka_city 'Город'," +
                    " date_format(date_ship, '%d/%m/%Y') 'ДатаОтгрузки'," +
                    " date_format(date_pay, '%d/%m/%Y') 'ДатаОплаты'," +
                    " manager 'Менеджер', " +
                    " oplata 'Оплата' from preform.shippings";
                MySqlCommand cmd = new MySqlCommand(querystring, con);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable("shippings");
                adapter.Fill(dt);
                dgEditShipping.ItemsSource = dt.DefaultView;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_EditShip_Click(object sender, RoutedEventArgs e)
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
                string editstring = $"update preform.shippings set status = @st, ka_name = @ka, " +
                                $"ka_city = @city, date_ship = @ds, " +
                                "date_pay = @dp, " +
                                $"manager = @mn, " +
                                $"oplata = @op where id_shipping = '"+txtEditID.Text+"'";
                MySqlCommand cmd = new MySqlCommand(editstring, con);
                cmd.Parameters.AddWithValue("@st", listEditStatus.Text);
                cmd.Parameters.AddWithValue("@ka", listEditKa.Text);
                cmd.Parameters.AddWithValue("@mn", listEditManager.Text);
                cmd.Parameters.AddWithValue("@city", txtEditCity.Text);
                cmd.Parameters.AddWithValue("@ds", DatePickerEditShip.SelectedDate.Value);
                cmd.Parameters.AddWithValue("@dp", DatePickerEditPay.SelectedDate.Value);
                //if (listEditStatus.Text == "Оплачен")
                //{
                //    txtOplata.Text = "Оплачен";
                //}
                //else if (listEditStatus.Text == "Отгружен")
                //{
                //    txtOplata.Text = "Не оплачен";
                //}
                //else
                //{
                //    txtOplata.Text = "unknown";
                //}
                cmd.Parameters.AddWithValue("@op", txtOplata.Text);
                int i = cmd.ExecuteNonQuery();
                if (i > -1)
                {
                    MessageBox.Show("Данные успешно изменены!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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
                    listEditStatus.Items.Add(drStatus.GetString("status_name"));
                }
                con.Close();

                con.Open();
                MySqlCommand cmdKa = new MySqlCommand(querystringKa, con);
                MySqlDataReader drKa = cmdKa.ExecuteReader();
                while (drKa.Read())
                {
                    listEditKa.Items.Add(drKa.GetString("ka_name"));
                }
                con.Close();

                con.Open();
                MySqlCommand cmdManager = new MySqlCommand(querystringManager, con);
                MySqlDataReader drManager = cmdManager.ExecuteReader();
                while (drManager.Read())
                {
                    listEditManager.Items.Add(drManager.GetString("manager_firstname"));
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DatePickerEditShip_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime datepay = DatePickerEditShip.SelectedDate.Value;
            datepay = datepay.AddDays(30);
            DatePickerEditPay.Text = datepay.ToString("dd/MM/yyyy");
            //dgEditShipping
        }

        private void listEditKa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string selectstringCity = $"select ka_city from preform.ka where ka_name= '{listEditKa.SelectedItem}'";
            MySqlCommand cmdSELcity = new MySqlCommand(selectstringCity, con);
            MySqlDataReader drSELcity = cmdSELcity.ExecuteReader();
            while (drSELcity.Read())
            {
                txtEditCity.Text = drSELcity.GetValue(0).ToString();
            }
            con.Close();
        }

        private void listEditStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string selectstringOplata = $"select pay_name from preform.status_shipping where status_name = '{listEditStatus.SelectedItem}'";
            MySqlCommand cmdSELOplata = new MySqlCommand(selectstringOplata, con);
            MySqlDataReader drSELOplata = cmdSELOplata.ExecuteReader();
            while (drSELOplata.Read())
            {
                txtOplata.Text = drSELOplata.GetValue(0).ToString();
            }
            con.Close();
        }
    }
}
