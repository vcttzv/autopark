using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для RegTS.xaml
    /// </summary>
    public partial class RegTS : Window
    {
        public RegTS()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
        }

        private void Reset()
        {
            template.Clear();
            mark.Clear();
            full_price.Clear();
            rent_price.Clear();
            type.SelectedIndex = -1;
            status.SelectedIndex = -1;
            autopark.SelectedIndex = -1;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (template.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите регистрационный номер.");
                template.Focus();
            }
            else if (mark.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите марку транспортного средства.");
                mark.Focus();
            }
            else if (type.SelectedIndex == -1)
            {
                ErrorHandler.ShowError("Выберите тип транспортного средства.");
                type.Focus();
            }
            else if (status.SelectedIndex == -1)
            {
                ErrorHandler.ShowError("Выберите статус транспортного средства.");
                status.Focus();
            }
            else if (autopark.SelectedIndex == -1)
            {
                ErrorHandler.ShowError("Выберите автопарк, в котором расположено транспортное средство.");
                autopark.Focus();
            }
            else if (full_price.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите полную стоимость транспортного средства.");
                full_price.Focus();
            }
            else if (rent_price.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите арендную стоимость транспортного средства за месяц эксплуатации.");
                rent_price.Focus();
            } else if (!Regex.IsMatch(template.Text, @"^[ABEKMNOPCTYX][0-9]{3}[ABEKMNOPCTYX]{2}$"))
            {
                ErrorHandler.ShowError("Некорректный регистрационный номер.");
                template.Focus();
            }
            else
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string templatedb = "SELECT * FROM transport WHERE template = @template_text";

                string template_text = template.Text;
                MySqlCommand cmd0 = new MySqlCommand(templatedb, connection);
                cmd0.Parameters.AddWithValue("@template_text", template_text);

                // Выполнение запроса и получение количества найденных строк
                int foundCount = Convert.ToInt32(cmd0.ExecuteScalar());

                if (foundCount > 0)
                {
                    ErrorHandler.ShowError("Транспортное средство с указанным регистрационным номером уже зарегистрировано.");
                }
                else
                {
                    string mark_text = mark.Text;
                    string type_option = (type.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string status_option = (status.SelectedItem as ComboBoxItem)?.Content.ToString();
                    string full_price_text = full_price.Text;
                    string rent_price_text = rent_price.Text;
                    
                    ComboBoxItem selectedItem = autopark.SelectedItem as ComboBoxItem;
                    int autopark_option = (selectedItem?.Tag != null) ? (int)selectedItem.Tag : 0;

                    string query = "INSERT INTO transport (mark, template, type, status, base_id, full_price, rent_price) VALUES (@mark_text, @template_text, @type_option, @status_option, @autopark_option, @full_price_text, @rent_price_text)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@mark_text", mark_text);
                    cmd.Parameters.AddWithValue("@type_option", type_option);
                    cmd.Parameters.AddWithValue("@autopark_option", autopark_option);
                    cmd.Parameters.AddWithValue("@status_option", status_option);
                    cmd.Parameters.AddWithValue("@full_price_text", full_price_text);
                    cmd.Parameters.AddWithValue("@rent_price_text", rent_price_text);
                    cmd.Parameters.AddWithValue("@template_text", template_text);

                    cmd.ExecuteNonQuery();

                    // Получение ID последней вставленной записи
                    cmd.CommandText = "SELECT LAST_INSERT_ID()";
                    int ts_id = Convert.ToInt32(cmd.ExecuteScalar());

                    connection.Close();

                    NotificationHandler.ShowNotification("Транспортное средство сохранено с ID: " + ts_id + "!");
                    Reset();
                }
            }
        }

        public void FillComboBox()
        {
            string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT DISTINCT base_id, address, base_name FROM base";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                    {
                        string name = !reader.IsDBNull(reader.GetOrdinal("base_name"))
                            ? reader.GetString(reader.GetOrdinal("base_name"))
                            : "Неизвестно";
                        string address = !reader.IsDBNull(reader.GetOrdinal("address"))
                            ? reader.GetString(reader.GetOrdinal("address"))
                            : "Не указан";
                        int id = !reader.IsDBNull(reader.GetOrdinal("base_id"))
                            ? reader.GetInt32(reader.GetOrdinal("base_id"))
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
                                Text = $"{name} [{id}]",
                                FontWeight = FontWeights.Bold,
                            },
                            new TextBlock
                            {
                                Text = address,
                                FontWeight = FontWeights.Normal,
                            }
                        }
                            },
                            Tag = id
                        };

                        autopark.Items.Add(item);
                    }

                    reader.Close();
                    connection.Close();
                }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}