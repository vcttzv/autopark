using System.Windows;
using MySql.Data.MySqlClient;
using System;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
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
            else
            {
                string login = box_login.Text;
                string password = box_password.Password;
                string connectionString = "Server=localhost;Port=3306;Database=autopark;Uid=root;Pwd=030201qwq;";

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        
                        string query = "SELECT emp_id FROM employee WHERE login = @login AND password = @password";
                        
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@password", password);
                            
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                UserInfo.UserId = result.ToString();
                                
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                mainWindow.welcome.Text = "Добро пожаловать, " + login + "!";
                                
                                Close();
                            }
                            else
                            {
                                ErrorHandler.ShowError("Не удалось найти учётную запись!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandler.ShowError("Ошибка подключения к базе данных: " + ex.Message);
                }
            }
        }
    }
}
