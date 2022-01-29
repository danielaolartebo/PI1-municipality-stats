﻿/// Daniela Olarte Borja || David Montaño Tamayo
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts;

namespace stats_s1
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        ObservableCollection<Users> Users = new ObservableCollection<Users>();

        // Import CSV file
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog() { Filter = "CSV Files (*.csv)|*.csv", Title = "Open File" };
            var result = fileDialog.ShowDialog();
            if (result == false)
            {
                MessageBox.Show("Failed to Load Data!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                ReadCSV(fileDialog.FileName);
                TBCodInitial.IsEnabled = true;
                TBCodFinal.IsEnabled = true;
                MessageBox.Show("Data Loaded!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            generatePieChart();
        }

        // Read CSV file 
        private void ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            IEnumerable<Users> IUsers = lines.Select(line =>
            {
                string[] data = line.Split(',');
                return new Users(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), data[2], data[3], data[4]);
            });
            Users = new ObservableCollection<Users>(IUsers);
            lvUsers.ItemsSource = Users;
        }

        //Validatiton
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Selection of 2 municipalities 
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<Users> tempUsers = new ObservableCollection<Users>();
            int start = (TBCodInitial.Text.Length > 0) ? Convert.ToInt32(TBCodInitial.Text) : 0;
            int final = (TBCodFinal.Text.Length > 0) ? Convert.ToInt32(TBCodFinal.Text) : int.MaxValue;
            foreach (Users users in Users)
            {
                if (users.CodMcpio >= start && users.CodMcpio <= final)
                    tempUsers.Add(users);
            }
            lvUsers.ClearValue(ItemsControl.ItemsSourceProperty);
            lvUsers.ItemsSource = tempUsers;
        }

        // Create pie chart
        private void generatePieChart()
        {
            SeriesCollection series = new SeriesCollection();
            foreach (Users users in Users)
            {
                series.Add(new PieSeries()
                {
                    Title = users.Type,
                    Values = new ChartValues<int> { users.getTypes().Count },
                    DataLabels = true,
                });
            }
            pieChart.Series = series;
        }
    }

    //User class
    public class Users
    {

        // Attributes
        public int CodDpto { get; }
        public int CodMcpio { get; }
        public string NameDpto { get; }
        public string NameMcpio { get; }
        public string Type { get; }
        public List<Users> types;

        // Methods
        public Users(int codDpto, int codMcpio, string nameDpto, string nameMcpio, string type)
        {
            CodDpto = codDpto;
            CodMcpio = codMcpio;
            NameDpto = nameDpto;
            NameMcpio = nameMcpio;
            Type = type;
            types = new List<Users>();
        }

        public List<Users> getTypes()
        {
            return types;
        }
        
    }
}


