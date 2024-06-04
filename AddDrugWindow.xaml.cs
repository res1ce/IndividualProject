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
    /// Логика взаимодействия для AddDrugWindow.xaml
    /// </summary>
    public partial class AddDrugWindow : Window
    {
        ObservableCollection<Drug> Drugs;
        public AddDrugWindow(ObservableCollection<Drug> drugs)
        {
            InitializeComponent();
            FillGroupComboBox();
            Drugs = drugs;
        }

        private void FillGroupComboBox()
        {
            List<DrugGroup> groups = DatabaseService.GetDrugGroups();
            GroupComboBox.ItemsSource = groups;
            GroupComboBox.DisplayMemberPath = "name";
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text.Trim() == "" || TextBoxDosage.Text.Trim() == "" || TextBoxExpirationDays.Text.Trim() == "" || GroupComboBox.SelectedItem as DrugGroup == null)
            {
                MessageBox.Show("Поля не должны быть пустыми", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string nameText = TextBoxName.Text.Trim();
            DrugGroup selectedGroup = GroupComboBox.SelectedItem as DrugGroup;
            string dosageText = TextBoxDosage.Text.Trim();
            string expirationDaysText = TextBoxExpirationDays.Text.Trim();
            Random random = new Random();
            int randomNumber = random.Next(1, 100000);
            while (CheckNumberDrug(randomNumber) == true)
            {
                randomNumber = random.Next(1, 100000);
            }
            try
            {
                DatabaseService.AddDrug(new Drug
                {
                    number = randomNumber,
                    name = nameText,
                    groupId = selectedGroup.id,
                    dosage = Int32.Parse(dosageText),
                    expiration_days = Int32.Parse(expirationDaysText)
                });
                MessageBox.Show("Препарат успешно добавлен.");
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}");
            }
        }

        private bool CheckNumberDrug(int number)
        {
            if (Drugs != null) return Drugs.Any(a => a.number == number);
            return false;
        }
    }
}
