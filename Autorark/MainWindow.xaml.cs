using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Autorark
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Out_Click(object sender, RoutedEventArgs e)
        {
            UserInfo.UserId = null;
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        private void Tables_Click(object sender, RoutedEventArgs e)
        {
            Tables tables = new Tables();
            tables.Show();
            Close();
        }

        private void Reg_op_Click(object sender, RoutedEventArgs e)
        {
            RegOp regOp = new RegOp();
            regOp.Show();
        }

        private void Reg_ts_Click(object sender, RoutedEventArgs e)
        {
            RegTS regTS = new RegTS();
            regTS.Show();
        }

        private void Reg_client_Click(object sender, RoutedEventArgs e)
        {
            RegClient regClient = new RegClient();
            regClient.Show();
        }
    }
}
