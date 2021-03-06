﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für programmStarten.xaml
    /// </summary>
    public partial class programmStarten : Window
    {
        public programmStarten()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxProgramm.Text))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = textBoxProgramm.Text;
                    process.Start();
                    this.Close();
                }
                catch (Exception except)
                {
                    MessageBox.Show(except.Message, "Meldung", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxProgramm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Click(sender, e);
            }
        }
    }
}
