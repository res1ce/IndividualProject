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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IndidProject
{
    /// <summary>
    /// Логика взаимодействия для AddAvaibalityWindow.xaml
    /// </summary>
    public partial class AddAvaibalityWindow : Window
    {
        Regex inputRegex = new Regex(@"^[0-9]$");
        Regex inputRegexFloat = new Regex(@"^\d,\d{0,2}$");

        public AddAvaibalityWindow(ObservableCollection<Apteka> aptekas, ObservableCollection<Drug> drugs)
        {
            InitializeComponent();
            FillComboBox(aptekas, drugs);
        }
        private void NumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputRegex.Match(e.Text);
            //если количество символов в строке больше или равно одному либо
            //если введенный символ не подходит нашему правилу
            if ((sender as TextBox).Text.Length >= 5 || !match.Success)
            {
                //обработка события прекращается и ввода неправильного символа не происходит
                e.Handled = true;
            }
        }        
        private void FloatPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputRegexFloat.Match(e.Text);
            if ((sender as TextBox).Text.Length >= 9 || match.Success)
            {
                e.Handled = true;
            }
        }
        private void FillComboBox(ObservableCollection<Apteka> aptekas, ObservableCollection<Drug> drugs)
        {
            DrugComboBox.ItemsSource = drugs;
            DrugComboBox.DisplayMemberPath = "number";
            AptekaComboBox.ItemsSource = aptekas;
            AptekaComboBox.DisplayMemberPath = "number";
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxQuantity.Text.Trim() == ""|| TextBoxCost.Text.Trim() == "" || AptekaComboBox.SelectedItem as Apteka == null || DrugComboBox.SelectedItem as Drug == null
                || (DateTime)ReleaseDateSelect.SelectedDate == null)
            {
                MessageBox.Show("Поля не должны быть пустыми", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Apteka selectedApteka = AptekaComboBox.SelectedItem as Apteka;
            Drug selectedDrug = DrugComboBox.SelectedItem as Drug;
            DateTime release_date = (DateTime)ReleaseDateSelect.SelectedDate;
            int quantity = Int32.Parse(TextBoxQuantity.Text.Trim());
            float cost = float.Parse(TextBoxCost.Text.Trim());

            if (quantity < 0)
            {
                MessageBox.Show("Количество не может быть меньше 0", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DatabaseService.AddAvaibality(new Avaibality
                {
                    number_apteka = selectedApteka.number,
                    number_drug = selectedDrug.number,
                    release_date = release_date,
                    quantity = quantity,
                    cost = cost
                });
                MessageBox.Show("Препарат в наличие успешно добавлен.");
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}");
            }
        }
    }
}
