/// Daniela Olarte Borja || David Montaño Tamayo
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
using stats_s1.model;

namespace stats_s1
{
    public partial class MainWindow : Window
    {
        // Attributes
        private SeriesCollection series;
        private ObservableCollection<DANE> Users;
        private List<Municipality> Mun;

        // Methods
        public MainWindow()
        {
            InitializeComponent();
            Users = new ObservableCollection<DANE>();
            Mun = new List<Municipality>();
        }

        // Import CSV file
        private void import_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog() { Filter = "CSV Files (*.csv)|*.csv", Title = "Open File" };
            var result = fileDialog.ShowDialog();
            if (result == false)
            {
                MessageBox.Show("Failed to Load Data!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                readCSV(fileDialog.FileName);
                TBCodInitial.IsEnabled = true;
                TBCodFinal.IsEnabled = true;
                generatePieChart(Mun);
                MessageBox.Show("Data Loaded!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Read CSV file 
        private void readCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            IEnumerable<DANE> IUsers = lines.Select(line =>
            {
                string[] data = line.Split(',');
                addType(Mun, data[4]);
                return new DANE(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), data[2], data[3], data[4]);
            });
            Users = new ObservableCollection<DANE>(IUsers);
            lvUsers.ItemsSource = Users;
        }

        //Add type of municipality
        private void addType(List<Municipality> mun, string type)
        {
            for (int i = 0; i < mun.Count; i++)
            {
                if (mun[i].Type == type)
                {
                    mun[i].Count++;
                    return;
                }
            }
            mun.Add(new Municipality(type));
        }

        //Validatiton
        private void numberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Selection of 2 municipalities 
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ObservableCollection<DANE> tempUsers = new ObservableCollection<DANE>();
            List<Municipality> tempMun = new List<Municipality>();
            int start = (TBCodInitial.Text.Length > 0) ? Convert.ToInt32(TBCodInitial.Text) : 0;
            int final = (TBCodFinal.Text.Length > 0) ? Convert.ToInt32(TBCodFinal.Text) : int.MaxValue;
            foreach (DANE users in Users)
            {
                if (users.CodMcpio >= start && users.CodMcpio <= final)
                {
                    tempUsers.Add(users);
                    addType(tempMun, users.Type);
                }
            }
            lvUsers.ClearValue(ItemsControl.ItemsSourceProperty);
            lvUsers.ItemsSource = tempUsers;
            pieChart.Series.Clear();
            generatePieChart(tempMun);
        }

        // Create pie chart
        private void generatePieChart(List<Municipality> municipalities)
        {
            series = new SeriesCollection();
            foreach (Municipality mun in municipalities)
            {
                series.Add(new PieSeries()
                {
                    Title = mun.Type,
                    Values = new ChartValues<int> { mun.Count },
                    DataLabels = true,
                });
            }
            pieChart.Series = series;
        }
    }
}

