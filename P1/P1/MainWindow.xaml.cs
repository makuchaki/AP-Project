using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace P1
{
    public partial class MainWindow : Window
    {
        public List<Info> Persons = new List<Info>();
        Equations EQ;
        Clock CL;

        public MainWindow()
        {
            InitializeComponent();

            EQ = new Equations(EquationsTextBox, EquationsAnswerLabel);

            CL = new Clock(LeftCanvas, ClockDayLabel, SecondCounter, MinuteCounter, MinutePointer, HourCounter, HourPointer);
            CL.Run();
        }

        #region Equations Events
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            EQ.answer();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            EquationsDiagramCanvas.Children.Clear();
            EquationsInformationCanvas.Children.Clear();
            EquationsInformationCanvas.Width = 0;

            EquationsTextBox.Text = string.Empty;
            EquationsAnswerLabel.Content = string.Empty;
        }

        private void EqautionsDrawButton_Click(object sender, RoutedEventArgs e)
        {
            EQ.Draw(EquationsDiagramCanvas, EquationsInformationCanvas);
        }

        private void EquationsPrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog PD = new PrintDialog();
            if (PD.ShowDialog() == true)
                PD.PrintVisual(EquationsDiagramCanvas, "Printing Diagram");
        }

        #endregion

        string firstName;
        string LastName;
        string City;
        int age;
        string path = @"C:\Users\mirzakuchaki";
        string output;

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            DataEntry.Text = string.Empty;

            OlderCheckBox.IsChecked = false;
            OlderTextBox.Text = string.Empty;

            YoungerCheckBox.IsChecked = false;
            YoungerTextBox.Text = string.Empty;

            PlaceCheckBox.IsChecked = false;
            PlaceTextBox.Text = string.Empty;

            NameCheckBox.IsChecked = false;
            NameTextox.Text = string.Empty;

            DataOutput.Text = string.Empty;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(path + "DataOutput.txt"))
            {
                output = string.Empty;

                if ((bool)OlderCheckBox.IsChecked && int.Parse(OlderTextBox.Text) < age)
                    output += firstName + ' ' + LastName + '\n';

                if ((bool)YoungerCheckBox.IsChecked && age < int.Parse(YoungerTextBox.Text))
                    output += firstName + ' ' + LastName + '\n';

                if ((bool)PlaceCheckBox.IsChecked && PlaceTextBox.Text == City)
                    output += firstName + ' ' + LastName + '\n';

                if ((bool)NameCheckBox.IsChecked && NameTextox.Text == firstName)
                    output += firstName + ' ' + LastName + '\n';

                sw.WriteLine(output);

            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Querybutton_Click(object sender, RoutedEventArgs e)
        {
            var lines = DataEntry.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            firstName = lines[0];
            LastName = lines[1];
            City = lines[2];

            int age = 0;
            for (int i = 3; i < lines.Length; i++)
                if (int.TryParse(lines[i], out age))
                    break;

            output = string.Empty;

            if ((bool)OlderCheckBox.IsChecked && int.Parse(OlderTextBox.Text) < age)
                output += firstName + ' ' + LastName + '\n';

            if ((bool)YoungerCheckBox.IsChecked && age < int.Parse(YoungerTextBox.Text))
                output += firstName + ' ' + LastName + '\n';

            if ((bool)PlaceCheckBox.IsChecked && PlaceTextBox.Text == City)
                output += firstName + ' ' + LastName + '\n';

            if ((bool)NameCheckBox.IsChecked && NameTextox.Text == firstName)
                output += firstName + ' ' + LastName + '\n';

            DataOutput.Text = output;

        }
    }
}