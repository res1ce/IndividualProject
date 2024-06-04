using IndidProject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IndidProject
{
    /// <summary>
    /// Логика взаимодействия для AddAptekaWindow.xaml
    /// </summary>
    public partial class AddAptekaWindow : Window
    {
        Regex inputRegex = new Regex(@"^[0-9]$");
        ObservableCollection<Apteka> Aptekas;
        public AddAptekaWindow(ObservableCollection<Apteka> aptekas)
        {
            InitializeComponent();
            Aptekas = aptekas;
        }

        private void NumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputRegex.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 2 || !match.Success)
            {
                e.Handled = true;
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            string nameText = TextBoxName.Text.Trim();
            string addressText = TextBoxAddress.Text.Trim();
            string start_workText = TextBoxStartWork.Text.Trim();
            string end_workText = TextBoxEndWork.Text.Trim();
            if(nameText == "" || addressText == "" || start_workText == "" || end_workText == "")
            {
                MessageBox.Show("Поля не должны быть пустыми", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(Int32.Parse(start_workText) < 0 || Int32.Parse(start_workText) > 23)
            {
                MessageBox.Show("Часы начала работы не могут быть меньше 0 или больше 23", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Int32.Parse(end_workText) < 0 || Int32.Parse(end_workText) > 23)
            {
                MessageBox.Show("Часы конца работы не могут быть меньше 0 или больше 23", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Random random = new Random();
            int randomNumber = random.Next(100, 100000);
            while (CheckNumberApteka(randomNumber) == true)
            {
                randomNumber = random.Next(100, 100000);
            }
            try
            {
                DatabaseService.AddApteka(new Apteka
                {
                    number = randomNumber,
                    name = nameText,
                    address = addressText,
                    start_work_time = Int32.Parse(start_workText),
                    end_work_time = Int32.Parse(end_workText)
                });
                MessageBox.Show("Аптека успешно добавлена.");
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}");
            }
        }

        private bool CheckNumberApteka(int number)
        {
            if(Aptekas != null) return Aptekas.Any(a => a.number == number);
            return false;
        }
    }
}
