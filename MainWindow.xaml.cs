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

namespace stats_s1
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

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
        }

        private IEnumerable<Users> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                return new Users(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), data[2], data[3], data[4]);
            });
        }
    }

    public class Users
    {
        public int CodDpto { get; }
        public int CodMcpio { get; }
        public string NameDpto { get; }
        public string NameMcpio { get; }
        public string Type { get; }

        public Users(int codDpto, int codMcpio, string nameDpto, string nameMcpio, string type)
        {
            CodDpto = codDpto;
            CodMcpio = codMcpio;
            NameDpto = nameDpto;
            NameMcpio = nameMcpio;
            Type = type;
        }
    }
}
