using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Prozesse.xaml
    /// </summary>
    public partial class Prozesse : Page
    {
        public Prozesse()
        {
            InitializeComponent();
            GetAllProcess();
        }

        Process[] process;

        private void GetAllProcess()    //Array aus allen Prozessen
        {
            process = Process.GetProcesses();
            int laenge = process.Length;
            string[] ProcessName = new string[laenge];  //String aus Prozessnamen zum sotieren
            for (int i = 0; i < laenge; i++)
            {
                ProcessName[i] = process[i].ToString().Substring(28).Trim(')');     //Schneidet unnoetigen Inhalt ab, nur Prozessname
            }
            Array.Sort(ProcessName);

            ListBoxProcess.Items.Clear();
            foreach (string p in ProcessName)
            {
                ListBoxProcess.Items.Add(p);
            }

            anzahl.Content = "Anzahl laufender Prozesse: " + process.Length;
        }



        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)   //Auswahl Prozess aus Liste
        {
           ListBoxItem processSelected = ((sender as ListBox).SelectedItem as ListBoxItem);
        }

        private void go_Click(object sender, RoutedEventArgs e)     //Programm starten
        {
            programmStarten seite = new programmStarten();//TODO mit Using
            if (seite.ShowDialog() == seite.DialogResult)
            {
                GetAllProcess();
            }
        }
        private void kill_Click(object sender, RoutedEventArgs e)   //Programm beenden
        {
            try
            {
                Process[] toKill = Process.GetProcessesByName(ListBoxProcess.SelectedItem.ToString());
               if (toKill.GetLength(0) > 1) {
                    switch (MessageBox.Show("Alle Prozesse mit diesem Namen beenden", "Frage", MessageBoxButton.YesNoCancel, MessageBoxImage.Question)) //alle Prozesse oder nur einen mit selben namen beenden
                    {
                        case MessageBoxResult.Cancel:
                            break;
                        case MessageBoxResult.No:
                            toKill[0].Kill();
                            break;
                        case MessageBoxResult.Yes:
                            foreach (Process proc in toKill)
                            {
                                proc.Kill();
                            }
                            break;
                    }
                }
                else
                {
                    toKill[0].Kill();
                }
                GetAllProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Meldung", MessageBoxButton.OK, MessageBoxImage.Error); //hat nicht geklappt
            }
            GetAllProcess();
        }

        private void aktualisieren_Click(object sender, RoutedEventArgs e)      //Programm aktualisieren
        {
            GetAllProcess();
        }
    }
}
