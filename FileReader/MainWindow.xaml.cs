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
using Microsoft.Win32;

namespace FileReader
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setDefaultValues();
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
                    }
                    counter++;
                }

            }
        }

         

    }
}
