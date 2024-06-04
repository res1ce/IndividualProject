using IndidProject.models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IndidProject
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

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text.Trim();
            string pass_1 = passbox_1.Password.Trim();
            if (login.Length < 5)
            {
                TextBoxLogin.ToolTip = "Логин должен быть не меньше 5 символов!";
                TextBoxLogin.Background = Brushes.DarkRed;
                return;
            }
            else if (pass_1.Length < 5)
            {
                passbox_1.ToolTip = "Пароль должен быть не меньше 5 символов!";
                passbox_1.Background = Brushes.DarkRed;
                return;
            }
            else
            {
                TextBoxLogin.ToolTip = " ";
                TextBoxLogin.Background = Brushes.Transparent;
                passbox_1.ToolTip = " ";
                passbox_1.Background = Brushes.Transparent;

                User authUser = DatabaseService.GetUser(login, pass_1);

                if (authUser != null)
                {
                    MessageBox.Show($"Добро пожаловать {login} !");
                    AptekaWindow aptekaWindow = new AptekaWindow(authUser.roleId);
                    aptekaWindow.Show();
                    this.Close();
                }
                else MessageBox.Show("Неверный логин или пароль!");
            }
        }
    }
}
