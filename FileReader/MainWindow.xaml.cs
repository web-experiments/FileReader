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
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;

namespace FileReader
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] Labels { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            setDefaultValues();

          
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = null
                }
            };

            


     

            DataContext = this;
        }

        private void setDefaultValues()
        {
            start.Text = "13:00";
            end.Text = "16:00";
        }

        private void BtnOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Analyse.Text = File.ReadAllText(openFileDialog.FileName);
                int counter = 0;
                string line;
                int numberofcalls = 0;

        System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog.FileName);
                while ((line = file.ReadLine()) != null)
                {
                    if (counter > 9)
                    {
                        if(line.Contains("Gesamtsumme"))
                        {
                            break;
                        }
                        System.Console.WriteLine(line);
                        string[] data = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                        var parsedDate = DateTime.Parse(data[0]);
                        System.Console.WriteLine(parsedDate.DayOfWeek);
                        if(Int16.Parse(data[2]) >= 1)
                        {
                            numberofcalls = numberofcalls + Int16.Parse(data[2]);
                        }
                    }
                    counter++;
                }
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
                System.Console.WriteLine(numberofcalls);

            }
        }

         

    }
}
