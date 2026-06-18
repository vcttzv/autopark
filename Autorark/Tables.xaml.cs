using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для Tables.xaml
    /// </summary>
    public partial class Tables : Window
    {
        public Tables()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();

            DisplayTS();
            DisplayOp();
            DisplayEmployees();
            DisplayBases();
            DisplayClients();

            SetVisibility();
            select_db.Visibility = Visibility.Visible;
        }

        public void SetVisibility()
        {
            transport_grid.Visibility = Visibility.Collapsed;
            operation_grid.Visibility = Visibility.Collapsed;
            employee_grid.Visibility = Visibility.Collapsed;
            client_grid.Visibility = Visibility.Collapsed;
            base_grid.Visibility = Visibility.Collapsed;

            no_results.Visibility = Visibility.Collapsed;
            select_db.Visibility = Visibility.Collapsed;

            filtr_ts.Visibility = Visibility.Collapsed;
            filtr_ops.Visibility = Visibility.Collapsed;
            filtr_bases.Visibility = Visibility.Collapsed;
        }

        public void ClearStyles()
        {
            transport.Style = (Style)FindResource("InactiveTab");
            operations.Style = (Style)FindResource("InactiveTab");
            employees.Style = (Style)FindResource("InactiveTab");
            clients.Style = (Style)FindResource("InactiveTab");
            parks.Style = (Style)FindResource("InactiveTab");
        }

        // Функционал вкладок

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Transport_Click(object sender, RoutedEventArgs e)
        {
            ClearStyles();
            transport.Style = (Style)FindResource("ActiveTab");
            SetVisibility();
            transport_grid.Visibility = Visibility.Visible;
            filtr_ts.Visibility = Visibility.Visible;
        }

        private void Operations_Click(object sender, RoutedEventArgs e)
        {
            ClearStyles();
            operations.Style = (Style)FindResource("ActiveTab");
            SetVisibility();
            operation_grid.Visibility = Visibility.Visible;
            filtr_ops.Visibility = Visibility.Visible;
        }

        private void Employees_Click(object sender, RoutedEventArgs e)
        {
            ClearStyles();
            employees.Style = (Style)FindResource("ActiveTab");
            SetVisibility();
            employee_grid.Visibility = Visibility.Visible;
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ClearStyles();
            clients.Style = (Style)FindResource("ActiveTab");
            SetVisibility();
            client_grid.Visibility = Visibility.Visible;
        }

        private void Parks_Click(object sender, RoutedEventArgs e)
        {
            ClearStyles();
            parks.Style = (Style)FindResource("ActiveTab");
            SetVisibility();
            base_grid.Visibility = Visibility.Visible;
            filtr_bases.Visibility = Visibility.Visible;
        }

        // Конец функционала вкладок

        // Подгрузка данных в таблицы

        private void DisplayTS()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT t.ts_id AS ts_id, t.template AS template, t.mark AS mark, t.type AS type, t.base_id AS base_id, b.base_name AS base_name, t.rent_price AS rent_price, t.full_price AS full_price, t.status AS status FROM transport t JOIN base b ON b.base_id = t.base_id ORDER BY t.ts_id ASC;";
                // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count == 0)
                {
                    transport_grid.Visibility = Visibility.Collapsed;
                    no_results.Visibility = Visibility.Visible;
                }
                else
                {
                    transport_grid.ItemsSource = dataTable.DefaultView;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DisplayOp()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT operation_id, e.login AS employee, CONCAT(c.name, ' ', c.lastname) AS client, t.template AS template, date, rent_for, price FROM operation o JOIN employee e ON e.emp_id = o.employee JOIN transport t ON t.ts_id = o.transport JOIN client c ON c.client_id = o.client ORDER BY operation_id ASC;";
                // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count == 1)
                {
                    operation_grid.Visibility = Visibility.Collapsed;
                    no_results.Visibility = Visibility.Visible;
                }
                else
                {
                    operation_grid.ItemsSource = dataTable.DefaultView;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DisplayEmployees()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT emp_id, login, name, lastname, email FROM employee ORDER BY emp_id ASC;";
                // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count == 0)
                {
                    employee_grid.Visibility = Visibility.Collapsed;
                    no_results.Visibility = Visibility.Visible;
                }
                else
                {
                    employee_grid.ItemsSource = dataTable.DefaultView;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DisplayClients()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT client_id, name, lastname, birth, phone, address, passport FROM client ORDER BY client_id ASC;";
                // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count == 0)
                {
                    client_grid.Visibility = Visibility.Collapsed;
                    no_results.Visibility = Visibility.Visible;
                }
                else
                {
                    client_grid.ItemsSource = dataTable.DefaultView;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DisplayBases()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "SELECT base_id, address, year_rent, rent_for, max_ts, base_name, (b.max_ts - (SELECT COUNT(*) FROM transport t WHERE t.base_id = b.base_id)) AS free_spots FROM base b ORDER BY base_id ASC;";
                // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                if (dataTable.Rows.Count == 0)
                {
                    base_grid.Visibility = Visibility.Collapsed;
                    no_results.Visibility = Visibility.Visible;
                }
                else
                {
                    base_grid.ItemsSource = dataTable.DefaultView;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // ----- Конец подгрузки данных в таблицы

        // Фильтры транспортных средств

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unsave.Visibility = Visibility.Visible;
        }

        private void Autopark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unsave.Visibility = Visibility.Visible;
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unsave.Visibility = Visibility.Visible;
        }

        // Подгрузка автопарков в ComboBox

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

        // ----- Конец фильтров транспортных средств

        // ----- Фильтры операций

        // Фильтр вкладки Транспорт
        private void Button_filter_Click(object sender, RoutedEventArgs e)
        {
            unsave.Visibility = Visibility.Collapsed;
            // применение фильтров
            if (type.SelectedIndex == -1 && type.SelectedIndex == -1 && status.SelectedIndex == -1)
            {
                ErrorHandler.ShowError("Не выбрано ни одного значения для фильтрации.");
            }
            else
            {
                MessageBox.Show("Можно фильтровать");
                // применение фильтров
            }
        }

        // Фильтр вкладки Операции
        private void Button_filter_ops_Click(object sender, RoutedEventArgs e)
        {
            unsave_ops.Visibility = Visibility.Collapsed;
            if (min_date.SelectedDate == null)
            {
                ErrorHandler.ShowError("Выберите начальную дату.");
                min_date.Focus();
            }
            else if (max_date.SelectedDate == null)
            {
                ErrorHandler.ShowError("Выберите конечную дату.");
                max_date.Focus();
            }
            else if (max_date.SelectedDate < min_date.SelectedDate)
            {
                ErrorHandler.ShowError("Даты выбраны некорректно. Конечная дата не может быть меньше начальной даты.");
            }
            else
            {
                try
                {
                    string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();

                    DateTime? selectedDate1 = min_date.SelectedDate;
                    DateTime? selectedDate2 = max_date.SelectedDate;

                    string query = "SELECT o.operation_id, e.login AS employee, CONCAT(c.name, ' ', c.lastname) AS client, t.template AS template, o.date, o.rent_for, o.price FROM operation o JOIN employee e ON e.emp_id = o.employee JOIN transport t ON t.ts_id = o.transport JOIN client c ON c.client_id = o.client WHERE NOT(o.rent_for<=@max_date OR o.date>@min_date) ORDER BY o.operation_id ASC; ";
                    // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.Add("@min_date", MySqlDbType.Date).Value = selectedDate1.Value.Date;
                    command.Parameters.Add("@max_date", MySqlDbType.Date).Value = selectedDate2.Value.Date;
                    MySqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    if (dataTable.Rows.Count == 0)
                    {
                        operation_grid.Visibility = Visibility.Collapsed;
                        no_results.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        operation_grid.ItemsSource = dataTable.DefaultView;
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        // Фильтр вкладка Автопарк
        private void Button_filter_bases_Click(object sender, RoutedEventArgs e)
        {
            unsave_bases.Visibility = Visibility.Collapsed;

            if (min_spaces.Text.Length == 0)
            {
                ErrorHandler.ShowError("Не введено значение для фильтрации!");
                min_spaces.Focus();
            }
            else
            {
                try
                {
                    string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();

                    string min_spots = min_spaces.Text;

                    string query = "SELECT base_id, address, year_rent, rent_for, max_ts, base_name, (b.max_ts - (SELECT COUNT(*) FROM transport t WHERE t.base_id = b.base_id)) AS free_spots FROM base b WHERE (b.max_ts - (SELECT COUNT(*) FROM transport t WHERE t.base_id = b.base_id)) > @min_spots ORDER BY base_id ASC;";
                    // SELECT itemID, i.type AS i_type, i.name AS i_name, price, characteristics, status, s.name AS s_name FROM items i JOIN structures s ON i.structure=s.strucID ORDER BY itemID ASC

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@min_spots", min_spots);
                    MySqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    if (dataTable.Rows.Count == 0)
                    {
                        base_grid.Visibility = Visibility.Collapsed;
                        no_results.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        base_grid.ItemsSource = dataTable.DefaultView;
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        // ----- Конец фильтров операций
    }
}
