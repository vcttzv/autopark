using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Windows;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        private void Reset()
        {
            box_login.Clear();
            box_password.Clear();
            box_password_confirm.Clear();
            box_name.Clear();
            box_lastname.Clear();
            box_email.Clear();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (box_login.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите логин.");
                box_login.Focus();
            }
            else if (box_password.Password.Length == 0)
            {
                ErrorHandler.ShowError("Введите пароль.");
                box_password.Focus();
            }
            else if (box_password.Password.Length < 8)
            {
                ErrorHandler.ShowError("Пароль слишком короткий!");
                box_password.Focus();
            }
            else if (box_password.Password != box_password_confirm.Password)
            {
                ErrorHandler.ShowError("Пароли не совпадают.");
                box_password_confirm.Focus();
            }
            else if (!Regex.IsMatch(box_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                ErrorHandler.ShowError("Введите корректный адрес электронной почты.");
                box_email.Select(0, box_email.Text.Length);
                box_email.Focus();
            }
            else if (box_name.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите имя.");
                box_name.Focus();
            }
            else if (box_lastname.Text.Length == 0)
            {
                ErrorHandler.ShowError("Введите фамилию.");
                box_lastname.Focus();
            }
            else
            {
                string login = box_login.Text;
                string password = box_password.Password;
                string email = box_email.Text;
                string name = box_name.Text;
                string lastname = box_lastname.Text;

                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO employee (login, password, email, name, lastname) VALUES (@login, @password, @email, @name, @lastname)";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@lastName", lastname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@login", login);

                cmd.ExecuteNonQuery();
                connection.Close();
                NotificationHandler.ShowNotification("Вы успешно зарегистрированы!");
                Reset();
            }
        }
    }
}