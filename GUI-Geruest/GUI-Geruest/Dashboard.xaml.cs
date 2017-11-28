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

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        DispatcherTimer perfCountTimer = new DispatcherTimer(); // Timer zur regelmaessigen Abfrage des aktuellen Ressourcenverbrauchs


        //Abfrage der verfuegbaren Netzwerkinstanzen
        static PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
        static string[] networkInterfaces = category.GetInstanceNames();
        //PerformanceCounter fuer Auslesen der Daten
        public PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public PerformanceCounter ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public PerformanceCounter diskIOCounter = new PerformanceCounter("PhysicalDisk", "Disk Bytes/sec", "0 C:");
        public PerformanceCounter networkRecCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkInterfaces[2]);
        public PerformanceCounter networkSenCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkInterfaces[2]);

        public Dashboard()
        {
            InitializeComponent();
            
            //Einrichtung Timer
            perfCountTimer.Tick += PerfCountTimer_Tick;
            perfCountTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            perfCountTimer.IsEnabled = true;
            //Festplattenbelegung abbilden
            drawPieChart(storageChart);
        }

        // Variablen fuer die aktuellen Werte der PerformanceCounter
        private Int32 currentCPU = new int();
        private Int32 currentRAM = new int();
        private Int32 currentDiskIO = new int();
        private Int32 currentNetworkRec = new int();
        private Int32 currentNetworkSen = new int();

        // Arrays fuer die letzten 100 Werte des PerformanceCounter
        private int[] cpuData = new int[100];
        private int[] ramData = new int[100];
        private int[] diskIOData = new int[100];
        private int[] networkRecData = new int[100];
        private int[] networkSenData = new int[100];


        //Wir bei jedem Timer-Tick ausgefuehrt
        private void PerfCountTimer_Tick(object sender, EventArgs e)
        {
            // Aktualisierung der Werte durch aufruf der PerformanceCounter
            currentCPU = Convert.ToInt32(cpuCounter.NextValue());
            currentRAM = Convert.ToInt32(ramCounter.NextValue());
            currentDiskIO = Convert.ToInt32(diskIOCounter.NextValue() / 1048576);
            currentNetworkRec = Convert.ToInt32(networkRecCounter.NextValue() / 1024);
            currentNetworkSen = Convert.ToInt32(networkSenCounter.NextValue() / 1024);
            
            //Aktualisierung der Arrays
            cpuData = refreshData(cpuData, currentCPU);
            ramData = refreshData(ramData, currentRAM);
            diskIOData = refreshData(diskIOData, currentDiskIO);
            networkRecData = refreshData(networkRecData, currentNetworkRec);
            networkSenData = refreshData(networkSenData, currentNetworkSen);

            // Aktualisierung der Graphen
            refreshLineChart(cpuChart, cpuData);
            cpuChart.Title = "CPU-Auslastung: " + currentCPU + "%";
            refreshLineChart(ramChart, ramData);
            ramChart.Title = "RAM-Auslastung: " + currentRAM + "%";
            refreshLineChart(diskIOChart, diskIOData);
            diskIOChart.Title = "Schreib-/Leserate: " + currentDiskIO + "MiB/s";
            refreshLineChart(networkRecChart, networkRecData);
            networkRecChart.Title = "Netzwerk empfangen: " + currentNetworkRec + "KiB/s";
            refreshLineChart(networkSenChart, networkSenData);
            networkSenChart.Title = "Netzwerk gesendet: " + currentNetworkSen + "KiB/s";

        }

        // Funktion zur Aktualisierung der Arrays
        static int[] refreshData(int[] currentData, int valueToAdd)
        {
            for (int i = 0; i < currentData.Length - 1; i++)
            {
                currentData[i] = currentData[i + 1];
            }
            currentData[currentData.Length - 1] = valueToAdd;
            return currentData;
        }

        // Funktion um die Werte in die Graphen zu uebertragen
        static void refreshLineChart(Chart chart, int[] data)
        {

            ((LineSeries)chart.Series[0]).ItemsSource =
                new KeyValuePair<double, int>[] 
                {
                    new KeyValuePair<double, int>(-9.9, data[0]),
                    new KeyValuePair<double, int>(-9.8, data[1]),
                    new KeyValuePair<double, int>(-9.7, data[2]),
                    new KeyValuePair<double, int>(-9.6, data[3]),
                    new KeyValuePair<double, int>(-9.5, data[4]),
                    new KeyValuePair<double, int>(-9.4, data[5]),
                    new KeyValuePair<double, int>(-9.3, data[6]),
                    new KeyValuePair<double, int>(-9.2, data[7]),
                    new KeyValuePair<double, int>(-9.1, data[8]),
                    new KeyValuePair<double, int>(-9.0, data[9]),
                    new KeyValuePair<double, int>(-8.9, data[10]),
                    new KeyValuePair<double, int>(-8.8, data[11]),
                    new KeyValuePair<double, int>(-8.7, data[12]),
                    new KeyValuePair<double, int>(-8.6, data[13]),
                    new KeyValuePair<double, int>(-8.5, data[14]),
                    new KeyValuePair<double, int>(-8.4, data[15]),
                    new KeyValuePair<double, int>(-8.3, data[16]),
                    new KeyValuePair<double, int>(-8.2, data[17]),
                    new KeyValuePair<double, int>(-8.1, data[18]),
                    new KeyValuePair<double, int>(-8.0, data[19]),
                    new KeyValuePair<double, int>(-7.9, data[20]),
                    new KeyValuePair<double, int>(-7.8, data[21]),
                    new KeyValuePair<double, int>(-7.7, data[22]),
                    new KeyValuePair<double, int>(-7.6, data[23]),
                    new KeyValuePair<double, int>(-7.5, data[24]),
                    new KeyValuePair<double, int>(-7.4, data[25]),
                    new KeyValuePair<double, int>(-7.3, data[26]),
                    new KeyValuePair<double, int>(-7.2, data[27]),
                    new KeyValuePair<double, int>(-7.1, data[28]),
                    new KeyValuePair<double, int>(-7.0, data[29]),
                    new KeyValuePair<double, int>(-6.9, data[30]),
                    new KeyValuePair<double, int>(-6.8, data[31]),
                    new KeyValuePair<double, int>(-6.7, data[32]),
                    new KeyValuePair<double, int>(-6.6, data[33]),
                    new KeyValuePair<double, int>(-6.5, data[34]),
                    new KeyValuePair<double, int>(-6.4, data[35]),
                    new KeyValuePair<double, int>(-6.3, data[36]),
                    new KeyValuePair<double, int>(-6.2, data[37]),
                    new KeyValuePair<double, int>(-6.1, data[38]),
                    new KeyValuePair<double, int>(-6.0, data[39]),
                    new KeyValuePair<double, int>(-5.9, data[40]),
                    new KeyValuePair<double, int>(-5.8, data[41]),
                    new KeyValuePair<double, int>(-5.7, data[42]),
                    new KeyValuePair<double, int>(-5.6, data[43]),
                    new KeyValuePair<double, int>(-5.5, data[44]),
                    new KeyValuePair<double, int>(-5.4, data[45]),
                    new KeyValuePair<double, int>(-5.3, data[46]),
                    new KeyValuePair<double, int>(-5.2, data[47]),
                    new KeyValuePair<double, int>(-5.1, data[48]),
                    new KeyValuePair<double, int>(-5.0, data[49]),
                    new KeyValuePair<double, int>(-4.9, data[50]),
                    new KeyValuePair<double, int>(-4.8, data[51]),
                    new KeyValuePair<double, int>(-4.7, data[52]),
                    new KeyValuePair<double, int>(-4.6, data[53]),
                    new KeyValuePair<double, int>(-4.5, data[54]),
                    new KeyValuePair<double, int>(-4.4, data[55]),
                    new KeyValuePair<double, int>(-4.3, data[56]),
                    new KeyValuePair<double, int>(-4.2, data[57]),
                    new KeyValuePair<double, int>(-4.1, data[58]),
                    new KeyValuePair<double, int>(-4.0, data[59]),
                    new KeyValuePair<double, int>(-3.9, data[60]),
                    new KeyValuePair<double, int>(-3.8, data[61]),
                    new KeyValuePair<double, int>(-3.7, data[62]),
                    new KeyValuePair<double, int>(-3.6, data[63]),
                    new KeyValuePair<double, int>(-3.5, data[64]),
                    new KeyValuePair<double, int>(-3.4, data[65]),
                    new KeyValuePair<double, int>(-3.3, data[66]),
                    new KeyValuePair<double, int>(-3.2, data[67]),
                    new KeyValuePair<double, int>(-3.1, data[68]),
                    new KeyValuePair<double, int>(-3.0, data[69]),
                    new KeyValuePair<double, int>(-2.9, data[70]),
                    new KeyValuePair<double, int>(-2.8, data[71]),
                    new KeyValuePair<double, int>(-2.7, data[72]),
                    new KeyValuePair<double, int>(-2.6, data[73]),
                    new KeyValuePair<double, int>(-2.5, data[74]),
                    new KeyValuePair<double, int>(-2.4, data[75]),
                    new KeyValuePair<double, int>(-2.3, data[76]),
                    new KeyValuePair<double, int>(-2.2, data[77]),
                    new KeyValuePair<double, int>(-2.1, data[78]),
                    new KeyValuePair<double, int>(-2.0, data[79]),
                    new KeyValuePair<double, int>(-1.9, data[80]),
                    new KeyValuePair<double, int>(-1.8, data[81]),
                    new KeyValuePair<double, int>(-1.7, data[82]),
                    new KeyValuePair<double, int>(-1.6, data[83]),
                    new KeyValuePair<double, int>(-1.5, data[84]),
                    new KeyValuePair<double, int>(-1.4, data[85]),
                    new KeyValuePair<double, int>(-1.3, data[86]),
                    new KeyValuePair<double, int>(-1.2, data[87]),
                    new KeyValuePair<double, int>(-1.1, data[88]),
                    new KeyValuePair<double, int>(-1.0, data[89]),
                    new KeyValuePair<double, int>(-0.9, data[90]),
                    new KeyValuePair<double, int>(-0.8, data[91]),
                    new KeyValuePair<double, int>(-0.7, data[92]),
                    new KeyValuePair<double, int>(-0.6, data[93]),
                    new KeyValuePair<double, int>(-0.5, data[94]),
                    new KeyValuePair<double, int>(-0.4, data[95]),
                    new KeyValuePair<double, int>(-0.3, data[96]),
                    new KeyValuePair<double, int>(-0.2, data[97]),
                    new KeyValuePair<double, int>(-0.1, data[98]),
                    new KeyValuePair<double, int>(0, data[99])
                };
        }

        // Funktion um Festplattenbelegung zu zeichnen
        static void drawPieChart(Chart chart)
        {
            DriveInfo drive = new DriveInfo("C");

            chart.Title = "Festplattenspeicher: " + drive.TotalSize / 1073741824 + "GiB";
            long used = drive.TotalSize - drive.TotalFreeSpace;
            

            ((PieSeries)chart.Series[0]).ItemsSource =
                new KeyValuePair<string, long>[]
                {
                    new KeyValuePair<string, long>("Frei: " + Math.Round((double)drive.TotalFreeSpace / (double)drive.TotalSize * 100.0) + "%", drive.TotalFreeSpace),
                    new KeyValuePair<string, long>("Belegt: " + Math.Round((double)used / (double)drive.TotalSize * 100.0) + "%", used)
                    
                };
        }

    }
}
