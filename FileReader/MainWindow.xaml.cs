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
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {4, 5, 6, 8},
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true,
                       Title = "Gespräche über 15min"
                },
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {2, 5, 6, 7},
                    StackMode = StackMode.Values,
                    DataLabels = true,
                    Title = "Gespräche unter 15min"
                    
                }
            };

            //adding series updates and animates the chart
            SeriesCollection.Add(new StackedColumnSeries
            {
                Values = new ChartValues<double> { 6, 2, 7 },
                StackMode = StackMode.Values,
                Title = "Nicht angenommen"
            });

            //adding values also updates and animates
            SeriesCollection[2].Values.Add(4d);





            DataContext = this;
        }

        private void setDefaultValues()
        {
          
        }

        private void BtnOpenFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
              
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
