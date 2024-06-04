using IndidProject.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace IndidProject
{
    /// <summary>
    /// Логика взаимодействия для AptekaWindow.xaml
    /// </summary>
    public partial class AptekaWindow : Window
    {
        public ObservableCollection<Apteka> Aptekas { get; set; }
        public ObservableCollection<Drug> Drugs { get; set; }
        public ObservableCollection<Avaibality> Avaibalities { get; set; }
        public AptekaWindow(int roleId)
        {
            InitializeComponent();
            LoadDataApteka();
            LoadDataDrug();
            LoadDataAvaibality();
            this.DataContext = this;
            if (roleId == 1)
            {
                AvailabilitysDataGrid.IsReadOnly = true;
                AvaibalityAddButton.IsEnabled = false;
                AvaibalityDeleteButton.Visibility = Visibility.Hidden;
            }
            else if (roleId == 2) 
            {
                AvailabilitysDataGrid.IsReadOnly = true;
                AptekaDataGrid.IsReadOnly = true;
                DrugsDataGrid.IsReadOnly = true;
                AvaibalityAddButton.IsEnabled = false;
                AvaibalityDeleteButton.Visibility = Visibility.Hidden;
                DrugsAddButton.IsEnabled = false;
                DrugDeleteButton.Visibility = Visibility.Hidden;
                AptekaAddButton.IsEnabled = false;
                AptekaDeleteButton.Visibility = Visibility.Hidden;
            }
            else
            {
                DrugsDataGrid.IsReadOnly = true;
                DrugsAddButton.IsEnabled = false;
                DrugDeleteButton.Visibility = Visibility.Hidden;
            }
        }

        private void LoadDataApteka()
        {
            var getAptekas = DatabaseService.GetAptekas();
            Aptekas = getAptekas;
            AptekaDataGrid.ItemsSource = getAptekas;
        }

        private void LoadDataAvaibality()
        {
            var getAvaibality = DatabaseService.GetAvaibality();
            Avaibalities = getAvaibality;
            AvailabilitysDataGrid.ItemsSource = getAvaibality;
        }

        private void LoadDataDrug() 
        {
            Drugs = DatabaseService.GetDrugs();
            foreach (var drug in Drugs)
            {
                string groupName = DatabaseService.GetGroupNameByGroupId(drug.groupId);
                drug.groupName = groupName;
            }
            DrugsDataGrid.ItemsSource = Drugs;
        }

        private void AddApteka_Button(object sender, RoutedEventArgs e)
        {
            AddAptekaWindow addAbiturientWindow = new AddAptekaWindow(Aptekas);
            if (addAbiturientWindow.ShowDialog() == true)
            {
                LoadDataApteka();
            }
        }

        private void AptekaDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedApteka = e.Row.Item as Apteka;
                if (editedApteka != null)
                {
                    UpdateApteka(editedApteka);
                }
            }
        }

        private void UpdateApteka(Apteka editedApteka)
        {
            DatabaseService.UpdateApteka(editedApteka);
        }

        private void Delete_Button_Apteka_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить данную аптеку?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    int number = (int)button.Tag;
                    DeleteApteka(number);
                }
            }
        }

        private void DeleteApteka(int number)
        {
            DatabaseService.DeleteApteka(number);
            LoadDataApteka();
        }

        private void AddDrug_Button(object sender, RoutedEventArgs e)
        {
            AddDrugWindow addDrugWindow = new AddDrugWindow(Drugs);
            if (addDrugWindow.ShowDialog() == true)
            {
                LoadDataDrug();
            }
        }

        private void DrugsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedDrug = e.Row.Item as Drug;
                if (editedDrug != null)
                {
                    UpdateDrugs(editedDrug);
                }
            }
        }

        private void UpdateDrugs(Drug editedDrug)
        {
            DatabaseService.UpdateDrugs(editedDrug);
        }

        private void Delete_Button_Drug_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить данный препарат?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    int number = (int)button.Tag;
                    DeleteDrug(number);
                }
            }
        }

        private void DeleteDrug(int number)
        {
            DatabaseService.DeleteDrug(number);
            LoadDataDrug();
        }

        private void AddAvalibality_Button(object sender, RoutedEventArgs e)
        {
            AddAvaibalityWindow addAvaibalityWindow = new AddAvaibalityWindow(Aptekas, Drugs);
            if (addAvaibalityWindow.ShowDialog() == true)
            {
                LoadDataAvaibality();
            }
        }

        private void AvailabilitysDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedAvaibality = e.Row.Item as Avaibality;
                if (editedAvaibality != null)
                {
                    UpdateAvaibality(editedAvaibality);
                }
            }
        }

        private void UpdateAvaibality(Avaibality editedAvaibality)
        {
            DatabaseService.UpdateAvaibality(editedAvaibality);
        }

        private void Delete_Button_Avaibality_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы хотите удалить данный препарат из наличности?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                if (button != null)
                {
                    int id = (int)button.Tag;
                    DeleteAvaibality(id);
                }
            }
        }

        private void DeleteAvaibality(int id)
        {
            DatabaseService.DeleteAvaibality(id);
            LoadDataAvaibality();
        }
    }
}
