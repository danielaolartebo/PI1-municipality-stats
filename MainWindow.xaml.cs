/// Daniela Olarte Borja || David Montaño Tamayo
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections.Generic;
using LiveCharts;
using LiveCharts.Wpf;

namespace stats_s1
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        private Users users;

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
                lvUsers.ItemsSource = ReadCSV(fileDialog.FileName);
                MessageBox.Show("Data Loaded!", "Import Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // generatePieChart();
        }

        // Read CSV file 

        private IEnumerable<Users> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                return new Users(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), data[2], data[3], data[4]);
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // Create pie chart
        /*
        private void generatePieChart()
        {
            SeriesCollection series = new SeriesCollection();
            foreach (Users types in users.getNameDpto())
            {
                series.Add(new PieSeries()
                {
                    Title = users.getType(),
                    Values = new ChartValues<int> { users.getCodDpto().Count },
                    DataLabels = true,
                });
           
            }
         
        }
        */

    }



    public class Users
    {

        // Attributes

        public int CodDpto;
        public int CodMcpio;
        public string NameDpto;
        public string NameMcpio;
        public string Type;

        // Methods

        public Users(int codDpto, int codMcpio, string nameDpto, string nameMcpio, string type)
        {
            CodDpto = codDpto;
            CodMcpio = codMcpio;
            NameDpto = nameDpto;
            NameMcpio = nameMcpio;
            Type = type;
        }

        // Getters and setters

        public int getCodDpto()
        {
            return CodDpto;
        }

        public int getCodMcpio()
        {
            return CodMcpio;
        }

        public string getNameDpto()
        {
            return NameDpto;
        }

        public string getNameMcpio()
        {
            return NameMcpio;
        }

        public string getType()
        {
            return Type;
        }
    }
}

