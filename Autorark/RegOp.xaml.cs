using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для RegOp.xaml
    /// </summary>
    public partial class RegOp : Window
    {
        public RegOp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox_ts();
            FillComboBox_client();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Reset()
        {
            ts.SelectedIndex = -1;
            client.SelectedIndex = -1;
            rent_start.SelectedDate = null;
            rent_end.SelectedDate = null;
            price.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            /*
            ts
            client
            rent_start
            rent_end
            price
             */
            if (ts.SelectedIndex == -1)
            {
                ErrorHandler.ShowError("Выберите транспортное средство.");
                ts.Focus();
            }
            else if (client.SelectedIndex == -1)
            {
                ErrorHandler.ShowError("Выберите клиента.");
                client.Focus();
            }
            else if (rent_start.SelectedDate == null)
            {
                ErrorHandler.ShowError("Выберите дату начала аренды.");
                rent_start.Focus();
            }
            else if (rent_end.SelectedDate == null)
            {
                ErrorHandler.ShowError("Выберите дату окончания аренды.");
                rent_end.Focus();
            }
            else if (price.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите итоговую стоимость сделки.");
                price.Focus();
            }
            else
            {
                // внести в бд
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                ComboBoxItem selectedItem1 = ts.SelectedItem as ComboBoxItem;
                int ts_option = (selectedItem1?.Tag != null) ? (int)selectedItem1.Tag : 0;
                ComboBoxItem selectedItem2 = client.SelectedItem as ComboBoxItem;
                int client_option = (selectedItem2?.Tag != null) ? (int)selectedItem2.Tag : 0;
                DateTime? selectedDate1 = rent_start.SelectedDate;
                string start_date = selectedDate1.Value.ToString("yyyy-MM-dd");
                DateTime? selectedDate2 = rent_end.SelectedDate;
                string end_date = selectedDate2.Value.ToString("yyyy-MM-dd");
                string price_text = price.Text;
                string employee_id = UserInfo.UserId;

                string query = "INSERT INTO operation (employee, client, transport, date, rent_for, price) VALUES (@employee, @client, @transport, @date, @rent_for, @price)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@employee", employee_id);
                cmd.Parameters.AddWithValue("@client", client_option);
                cmd.Parameters.AddWithValue("@transport", ts_option);
                cmd.Parameters.AddWithValue("@date", start_date);
                cmd.Parameters.AddWithValue("@rent_for", end_date);
                cmd.Parameters.AddWithValue("@price", price_text);

                cmd.ExecuteNonQuery();

                // Получение ID последней вставленной записи
                cmd.CommandText = "SELECT LAST_INSERT_ID()";
                int id = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();

                NotificationHandler.ShowNotification("Сделка сохранена с ID: " + id + "!");
                Reset();
            }
        }

        public void FillComboBox_ts()
        {
            string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT DISTINCT ts_id, template, mark, type FROM transport";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string template = !reader.IsDBNull(reader.GetOrdinal("template"))
                    ? reader.GetString(reader.GetOrdinal("template"))
                    : "X 000 XX";
                string mark = !reader.IsDBNull(reader.GetOrdinal("mark"))
                    ? reader.GetString(reader.GetOrdinal("mark"))
                    : "Не указана";
                string type = !reader.IsDBNull(reader.GetOrdinal("type"))
                    ? reader.GetString(reader.GetOrdinal("type"))
                    : "Неизвестно";
                int id = !reader.IsDBNull(reader.GetOrdinal("ts_id"))
                    ? reader.GetInt32(reader.GetOrdinal("ts_id"))
                    : 0;

                ComboBoxItem item = new ComboBoxItem
                {
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Children =
                        {
                            new TextBlock
                            {
                                Text = $"{template} [{id}]",
                                FontWeight = FontWeights.Bold,
                            },
                            new TextBlock
                            {
                                Text = $"{type}, {mark}",
                                FontWeight = FontWeights.Normal,
                            }
                        }
                    },
                    Tag = id
                };

                ts.Items.Add(item);
            }

            reader.Close();
            connection.Close();
        }

        public void FillComboBox_client()
        {
            string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT DISTINCT name, lastname, client_id, phone FROM client";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string name = !reader.IsDBNull(reader.GetOrdinal("name"))
                    ? reader.GetString(reader.GetOrdinal("name"))
                    : "Имя";
                string lastname = !reader.IsDBNull(reader.GetOrdinal("lastname"))
                    ? reader.GetString(reader.GetOrdinal("lastname"))
                    : "Фамилия";
                string phone = !reader.IsDBNull(reader.GetOrdinal("phone"))
                    ? reader.GetString(reader.GetOrdinal("phone"))
                    : "Не указан";
                int id = !reader.IsDBNull(reader.GetOrdinal("client_id"))
                    ? reader.GetInt32(reader.GetOrdinal("client_id"))
                    : 0;

                ComboBoxItem item = new ComboBoxItem
                {
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Children =
                        {
                            new TextBlock
                            {
                                Text = $"{name} {lastname} [{id}]",
                                FontWeight = FontWeights.Bold,
                            },
                            new TextBlock
                            {
                                Text = $"+{phone}",
                                FontWeight = FontWeights.Normal,
                            }
                        }
                    },
                    Tag = id
                };

                client.Items.Add(item);
            }

            reader.Close();
            connection.Close();
        }

    }
}
