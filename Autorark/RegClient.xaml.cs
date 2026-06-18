using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для RegClient.xaml
    /// </summary>
    public partial class RegClient : Window
    {
        public RegClient()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            /*
            name
            lastname
            birthdate
            telephone
            address
            passport
             */
            if (name.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите имя.");
                name.Focus();
            }
            else if (!Regex.IsMatch(name.Text, @"^[А-ЯA-Z][а-яa-z]*$"))
            {
                ErrorHandler.ShowError("Имя введено некорректно.");
                name.Focus();
            }
            else if (lastname.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите фамилию.");
                lastname.Focus();
            }
            else if (!Regex.IsMatch(lastname.Text, @"^[А-ЯA-Z][а-яa-z]*$"))
            {
                ErrorHandler.ShowError("Фамилия введена некорректно.");
                lastname.Focus();
            }
            else if (telephone.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите контактный номер телефона.");
                telephone.Focus();
            }
            else if (!Regex.IsMatch(telephone.Text, @"^\d{11}$"))
            {
                ErrorHandler.ShowError("Номер телефона введён некорректно.");
                telephone.Focus();
            }
            else if (address.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите адрес.");
                address.Focus();
            }
            else if (passport.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите номер и серию паспорта.");
                passport.Focus();
            }
            else if (!Regex.IsMatch(passport.Text, @"^\d{10}$"))
            {
                ErrorHandler.ShowError("Номер и серия паспорта введены некорректно.");
                passport.Focus();
            }
            else if (birthdate.SelectedDate == null)
            {
                ErrorHandler.ShowError("Выберите дату рождения.");
                birthdate.Focus();
            }
            else
            {
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string passportdb = "SELECT * FROM client WHERE passport = @passport_text";

                string passport_text = passport.Text;
                MySqlCommand cmd0 = new MySqlCommand(passportdb, connection);
                cmd0.Parameters.AddWithValue("@passport_text", passport_text);
                
                int foundCount = Convert.ToInt32(cmd0.ExecuteScalar());

                if (foundCount > 0)
                {
                    ErrorHandler.ShowError("Клиент с указанным номером и серией паспорта уже зарегистрирован.");
                }
                else
                {
                    string name_text = name.Text;
                    string lastname_text = lastname.Text;
                    DateTime? selectedDate = birthdate.SelectedDate;
                    string birthdate_date = selectedDate.Value.ToString("yyyy-MM-dd");
                    string telephone_text = telephone.Text;
                    string address_text = address.Text;

                    string query = "INSERT INTO client (name, lastname, birth, phone, address, passport) VALUES (@name_text, @lastname_text, @birthdate_date, @telephone_text, @address_text, @passport_text)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name_text", name_text);
                    cmd.Parameters.AddWithValue("@lastname_text", lastname_text);
                    cmd.Parameters.AddWithValue("@birthdate_date", birthdate_date);
                    cmd.Parameters.AddWithValue("@telephone_text", telephone_text);
                    cmd.Parameters.AddWithValue("@address_text", address_text);
                    cmd.Parameters.AddWithValue("@passport_text", passport_text);

                    cmd.ExecuteNonQuery();

                    // Получение ID последней вставленной записи
                    cmd.CommandText = "SELECT LAST_INSERT_ID()";
                    int client_id = Convert.ToInt32(cmd.ExecuteScalar());

                    connection.Close();

                    NotificationHandler.ShowNotification("Клиент " + name_text + " " + lastname_text + " зарегистрирован с ID: " + client_id + "!");
                    Reset();
                }
            }
        }

        private void Reset()
        {
            name.Clear();
            lastname.Clear();
            birthdate.SelectedDate = null;
            telephone.Clear();
            address.Clear();
            passport.Clear();
        }
    }
}
