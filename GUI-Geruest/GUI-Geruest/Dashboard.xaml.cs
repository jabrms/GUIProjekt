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
using System.Windows.Threading;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization;
using System.IO;
using System.Windows.Forms.Integration;
using System.Net.NetworkInformation;

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        DispatcherTimer perfCountTimer = new DispatcherTimer();

        //Abfrage der verfuegbaren Netzwerkinstanzen
        static PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
        static String[] instancenames = category.GetInstanceNames();
        //PerformanceCounter fuer Auslesen der Daten
        public PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"); // % der genutzten CPU-Zeit
        public PerformanceCounter ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use"); // % des belegten RAMs
        public PerformanceCounter diskIOCounter = new PerformanceCounter("PhysicalDisk", "Disk Bytes/sec", "_Total"); // Schreib-/Leserate aller Festplatten


        public LineChart cpuLineChart;
        public LineChart ramLineChart;
        public LineChart diskIOLineChart;
        public LineChart networkRecLineChart;
        public LineChart networkSenLineChart;
        public PieChart storagePieChart;

        public Dashboard()
        {
            InitializeComponent();

            networkRecGrid.Visibility = Visibility.Collapsed;
            networkSenGrid.Visibility = Visibility.Collapsed;

            // Eintragen der vorhandenen Netzwerk Interfaces ins DropDownMenu
            foreach (String instance in instancenames)
            {
                networkComboBox.SelectedItem = networkComboBox.Items[0];
                networkComboBox.Items.Add(instance);

            }
            




            //Einrichtung Timer
            perfCountTimer.Tick += PerfCountTimer_Tick;
            perfCountTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            perfCountTimer.IsEnabled = true;


            cpuLineChart = new LineChart(cpuChart, cpuCounter, "Percentage", "CPU-Auslastung");
            ramLineChart = new LineChart(ramChart, ramCounter, "Percentage", "RAM-Auslastung");
            storagePieChart = new PieChart(storageChart, "C");
            diskIOLineChart = new LineChart(diskIOChart, diskIOCounter, "MiB/s", "Lese-/Schreibrate");

        }

        //Wir bei jedem Timer-Tick ausgefuehrt
        private void PerfCountTimer_Tick(object sender, EventArgs e)
        {
            // Aktualisierung der Werte durch aufruf der PerformanceCounter
            cpuLineChart.refresh();
            ramLineChart.refresh();
            diskIOLineChart.refresh();
            if (networkRecLineChart == null || networkSenLineChart == null) // verhindert Aktualisierung von Netzwerkcharts wenn kein Interface gewaehlt wurde
            {
                return;
            }
            networkRecLineChart.refresh();
            networkSenLineChart.refresh();
        }

        // wird aktiviert, wenn das netzwerkinterface geaendert wird
        private void networkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (networkComboBox.SelectedItem == networkComboBox.Items[0])
            {
                return;
            }
            else if (networkRecLineChart == null )
            {
                noInterfaceOverlayLabel.Visibility = Visibility.Collapsed;
                networkRecGrid.Visibility = Visibility.Visible;
                networkSenGrid.Visibility = Visibility.Visible;
                networkRecLineChart = new LineChart(networkRecChart, new PerformanceCounter("Network Interface", "Bytes Received/sec", (string)networkComboBox.SelectedItem), "KiB/s", "Netzwerk Empfagsrate");
                networkSenLineChart = new LineChart(networkSenChart, new PerformanceCounter("Network Interface", "Bytes Sent/sec", (string)networkComboBox.SelectedItem), "KiB/s", "Netzwerk Senderate");
            }
            else
            {
                networkRecLineChart.changePerfCounter(new PerformanceCounter("Network Interface", "Bytes Received/sec", (string)networkComboBox.SelectedItem));
                networkSenLineChart.changePerfCounter(new PerformanceCounter("Network Interface", "Bytes Sent/sec", (string)networkComboBox.SelectedItem));
            }
        }
    }
}
