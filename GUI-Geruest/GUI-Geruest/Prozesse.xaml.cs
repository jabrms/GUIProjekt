using System;
using System.Collections.Generic;
using System.Diagnostics;
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

//TODO Button Akzualisieren
//TODO Liste sotieren
//TODO aus Liste AUswählen


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
            //ListBoxProcess.SelectionMode = SelectionMode.Single;
        }

        Process[] process;

        /*
        private Process[] sortProcess(Process[] proc)
        {
            Process tauschen;
            int laenge = proc.Length;
            for (int i = 0; i < laenge; i++)
            {
                for (int x = i; x < laenge-1; x++)
                {
                    String stringA = proc[x].ToString().Substring(29).Trim(')');
                    String stringB = proc[x + 1].ToString().Substring(29).Trim(')');
                    //MessageBox.Show("\n" + stringA + " " + stringB + " " + "\n" + String.Compare(stringA, stringB, true));
                    if (String.Compare(stringA, stringB, false) == -1)
                    {
                       tauschen = proc[x];
                       proc[x] = proc[i];
                       proc[i] = tauschen;
                   }
               }
           }
        return proc;
        }
        */

        /*alt
    private void GetAllProcess()
    {

        process = Process.GetProcesses();

        ListBoxProcess.Items.Clear();
        foreach (Process p in process)
        {
            ListBoxProcess.Items.Add(p.ProcessName);
        }

        anzahl.Content = "Anzahl laufender Prozesse: " + process.Length;
    }
    */
        private void GetAllProcess()
        {
            process = Process.GetProcesses();
            int laenge = process.Length;
            string[] ProcessName = new string[laenge];
            for (int i = 0; i < laenge; i++)
            {
                ProcessName[i] = process[i].ToString().Substring(28).Trim(')');
            }

            Array.Sort(ProcessName);

            ListBoxProcess.Items.Clear();
            foreach (String p in ProcessName)
            {
                ListBoxProcess.Items.Add(p);
            }

            anzahl.Content = "Anzahl laufender Prozesse: " + process.Length;
        }



        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetAllProcess();
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            programmStarten seite = new programmStarten();//TODO mit Using

            //{
            if (seite.ShowDialog() == seite.DialogResult)
            {
                GetAllProcess();
            }

            //}
        }
        private void kill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                process[ListBoxProcess.SelectedIndex].Kill();
                GetAllProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Meldung", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void aktualisieren_Click(object sender, RoutedEventArgs e)
        {
            GetAllProcess();

        }
    }
}
