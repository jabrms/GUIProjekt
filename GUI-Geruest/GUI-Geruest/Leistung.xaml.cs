using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.VisualBasic.Devices;

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Leistung.xaml
    /// </summary>
    public partial class Leistung : Page
    {
        DispatcherTimer perfCountTimer = new DispatcherTimer();

        //einzelne Prozessorkerne abrufen
        static PerformanceCounterCategory category = new PerformanceCounterCategory("Processor");
        static string[] instancenames = category.GetInstanceNames();

        //Arrays fuer PerfCounter/LineCharts der Kerne
        public PerformanceCounter[] cpuPerfCounter = new PerformanceCounter[8];
        public LineChart[] cpuLineCharts = new LineChart[8];

        // RAM PerfCounter/LineChart
        public PerformanceCounter ramAvCounter = new PerformanceCounter("Memory", "Available Bytes");
        public PerformanceCounter ramComCounter = new PerformanceCounter("Memory", "Committed Bytes");
        public LineChart ramLineChart;

        // alle verfuegbaren Speichermedien abrufen
        static PerformanceCounterCategory diskCategory = new PerformanceCounterCategory("PhysicalDisk");
        static string[] diskInstancenames = diskCategory.GetInstanceNames();

        public LineChart diskLineChart;

        public Leistung()
        {
            InitializeComponent();


            // erstellt PerformanceCounter fuer jeden CPU-Kern
            int i = 0;
            foreach (string instance in instancenames.Skip(1))
            {
                cpuPerfCounter[i] = new PerformanceCounter("Processor", "% Processor Time", instance);
                i++;
            }

            setNumberOfCpuCharts();

            ramLineChart = new LineChart(ramChart, ramComCounter, "GiB", "RAM-Belegung");
            ramChart.ChartAreas[0].AxisY.Maximum = (ramComCounter.NextValue() + ramAvCounter.NextValue()) / 1073741824;

            ramLabel.Content = "Physikalischer Speicher: " + Math.Round((decimal)new ComputerInfo().TotalPhysicalMemory / 1073741824, 2) + " GiB";

            // Eintragen der vorhandenen Speichermedien ins DropDownMenu
            foreach (String diskInstance in diskInstancenames)
            {
                diskComboBox.SelectedItem = diskComboBox.Items[0];
                diskComboBox.Items.Add(diskInstance);

            }

            //Einrichtung Timer
            perfCountTimer.Tick += PerfCountTimer_Tick;
            perfCountTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            perfCountTimer.IsEnabled = true;

        }

        private void PerfCountTimer_Tick(object sender, EventArgs e)
        {
            foreach (LineChart lineChart in cpuLineCharts)
            {
                if (lineChart == null)
                {
                    break;
                }
                lineChart.refresh();
                
            }

            ramLineChart.refresh();

            ramUsedLabel.Content = "Genutzter Speicher: " + Math.Round((ramComCounter.NextValue() / 1073741824), 2) + " GiB";
            ramAvailableLabel.Content = "Freier Speicher: " + Math.Round((ramAvCounter.NextValue() / 1073741824), 2) + " GiB";

            if (diskLineChart == null)
            {
                return;
            }

            diskLineChart.refresh();
            

        }


        // legt das Layout entsprechend der vorhandenen CPU-Kerne fest
        private void setNumberOfCpuCharts()
        {
            switch (instancenames.Length - 1)
            {
                case 1:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 2);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 5);

                    cpu1Grid.Visibility = Visibility.Collapsed;
                    cpu2Grid.Visibility = Visibility.Collapsed;
                    cpu3Grid.Visibility = Visibility.Collapsed;
                    cpu4Grid.Visibility = Visibility.Collapsed;
                    cpu5Grid.Visibility = Visibility.Collapsed;
                    cpu6Grid.Visibility = Visibility.Collapsed;
                    cpu7Grid.Visibility = Visibility.Collapsed;
                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    break;
                case 2:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 2);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 2);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 2);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 2);

                    column2.Width = GridLength.Auto;

                    cpu2Grid.Visibility = Visibility.Collapsed;
                    cpu3Grid.Visibility = Visibility.Collapsed;
                    cpu4Grid.Visibility = Visibility.Collapsed;
                    cpu5Grid.Visibility = Visibility.Collapsed;
                    cpu6Grid.Visibility = Visibility.Collapsed;
                    cpu7Grid.Visibility = Visibility.Collapsed;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    break;
                case 3:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 2);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 1);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 2);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu1Grid.SetValue(Grid.RowProperty, 1);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 2);

                    cpu2Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu2Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu2Grid.SetValue(Grid.RowProperty, 1);
                    cpu2Grid.SetValue(Grid.RowSpanProperty, 2);

                    column1.Width = GridLength.Auto;
                    column3.Width = GridLength.Auto;

                    cpu3Grid.Visibility = Visibility.Collapsed;
                    cpu4Grid.Visibility = Visibility.Collapsed;
                    cpu5Grid.Visibility = Visibility.Collapsed;
                    cpu6Grid.Visibility = Visibility.Collapsed;
                    cpu7Grid.Visibility = Visibility.Collapsed;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    cpuLineCharts[2] = new LineChart(cpu2Chart, cpuPerfCounter[2], "Percentage", "CPU 3:");
                    break;
                case 4:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 1);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 2);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 2);
                    cpu1Grid.SetValue(Grid.RowProperty, 1);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu2Grid.SetValue(Grid.ColumnProperty, 0);
                    cpu2Grid.SetValue(Grid.ColumnSpanProperty, 2);
                    cpu2Grid.SetValue(Grid.RowProperty, 2);
                    cpu2Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu3Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu3Grid.SetValue(Grid.ColumnSpanProperty, 2);
                    cpu3Grid.SetValue(Grid.RowProperty, 2);
                    cpu3Grid.SetValue(Grid.RowSpanProperty, 1);

                    column2.Width = GridLength.Auto;

                    cpu4Grid.Visibility = Visibility.Collapsed;
                    cpu5Grid.Visibility = Visibility.Collapsed;
                    cpu6Grid.Visibility = Visibility.Collapsed;
                    cpu7Grid.Visibility = Visibility.Collapsed;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    cpuLineCharts[2] = new LineChart(cpu2Chart, cpuPerfCounter[2], "Percentage", "CPU 3:");
                    cpuLineCharts[3] = new LineChart(cpu3Chart, cpuPerfCounter[3], "Percentage", "CPU 4:");
                    break;

                case 5:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 1);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 1);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 2);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu1Grid.SetValue(Grid.RowProperty, 1);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu2Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu2Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu2Grid.SetValue(Grid.RowProperty, 1);
                    cpu2Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu3Grid.SetValue(Grid.ColumnProperty, 0);
                    cpu3Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu3Grid.SetValue(Grid.RowProperty, 2);
                    cpu3Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu4Grid.SetValue(Grid.ColumnProperty, 2);
                    cpu4Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu4Grid.SetValue(Grid.RowProperty, 2);
                    cpu4Grid.SetValue(Grid.RowSpanProperty, 1);

                    column1.Width = GridLength.Auto;
                    column3.Width = GridLength.Auto;


                    cpu5Grid.Visibility = Visibility.Collapsed;
                    cpu6Grid.Visibility = Visibility.Collapsed;
                    cpu7Grid.Visibility = Visibility.Collapsed;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    cpuLineCharts[2] = new LineChart(cpu2Chart, cpuPerfCounter[2], "Percentage", "CPU 3:");
                    cpuLineCharts[3] = new LineChart(cpu3Chart, cpuPerfCounter[3], "Percentage", "CPU 4:");
                    cpuLineCharts[4] = new LineChart(cpu4Chart, cpuPerfCounter[4], "Percentage", "CPU 5:");

                    break;

                case 6:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 1);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 1);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 2);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu1Grid.SetValue(Grid.RowProperty, 1);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu2Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu2Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu2Grid.SetValue(Grid.RowProperty, 1);
                    cpu2Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu3Grid.SetValue(Grid.ColumnProperty, 0);
                    cpu3Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu3Grid.SetValue(Grid.RowProperty, 2);
                    cpu3Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu4Grid.SetValue(Grid.ColumnProperty, 2);
                    cpu4Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu4Grid.SetValue(Grid.RowProperty, 2);
                    cpu4Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu5Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu5Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu5Grid.SetValue(Grid.RowProperty, 2);
                    cpu5Grid.SetValue(Grid.RowSpanProperty, 1);

                    column1.Width = GridLength.Auto;
                    column3.Width = GridLength.Auto;


                    cpu6Grid.Visibility = Visibility.Collapsed;
                    cpu7Grid.Visibility = Visibility.Collapsed;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    cpuLineCharts[2] = new LineChart(cpu2Chart, cpuPerfCounter[2], "Percentage", "CPU 3:");
                    cpuLineCharts[3] = new LineChart(cpu3Chart, cpuPerfCounter[3], "Percentage", "CPU 4:");
                    cpuLineCharts[4] = new LineChart(cpu4Chart, cpuPerfCounter[4], "Percentage", "CPU 5:");
                    cpuLineCharts[5] = new LineChart(cpu5Chart, cpuPerfCounter[5], "Percentage", "CPU 6:");


                    break;

                case 7:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 1);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 1);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 1);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu1Grid.SetValue(Grid.RowProperty, 1);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu2Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu2Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu2Grid.SetValue(Grid.RowProperty, 1);
                    cpu2Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu3Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu3Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu3Grid.SetValue(Grid.RowProperty, 1);
                    cpu3Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu4Grid.SetValue(Grid.ColumnProperty, 0);
                    cpu4Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu4Grid.SetValue(Grid.RowProperty, 2);
                    cpu4Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu5Grid.SetValue(Grid.ColumnProperty, 1);
                    cpu5Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu5Grid.SetValue(Grid.RowProperty, 2);
                    cpu5Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu6Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu6Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu6Grid.SetValue(Grid.RowProperty, 2);
                    cpu6Grid.SetValue(Grid.RowSpanProperty, 1);

                    column2.Width = GridLength.Auto;


                    cpu7Grid.Visibility = Visibility.Collapsed;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    cpuLineCharts[2] = new LineChart(cpu2Chart, cpuPerfCounter[2], "Percentage", "CPU 3:");
                    cpuLineCharts[3] = new LineChart(cpu3Chart, cpuPerfCounter[3], "Percentage", "CPU 4:");
                    cpuLineCharts[4] = new LineChart(cpu4Chart, cpuPerfCounter[4], "Percentage", "CPU 5:");
                    cpuLineCharts[5] = new LineChart(cpu5Chart, cpuPerfCounter[5], "Percentage", "CPU 6:");
                    cpuLineCharts[6] = new LineChart(cpu6Chart, cpuPerfCounter[6], "Percentage", "CPU 7:");


                    break;

                case 8:
                    cpu0Grid.SetValue(Grid.RowSpanProperty, 1);
                    cpu0Grid.SetValue(Grid.ColumnSpanProperty, 1);

                    cpu1Grid.SetValue(Grid.ColumnProperty, 1);
                    cpu1Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu1Grid.SetValue(Grid.RowProperty, 1);
                    cpu1Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu2Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu2Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu2Grid.SetValue(Grid.RowProperty, 1);
                    cpu2Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu3Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu3Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu3Grid.SetValue(Grid.RowProperty, 1);
                    cpu3Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu4Grid.SetValue(Grid.ColumnProperty, 0);
                    cpu4Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu4Grid.SetValue(Grid.RowProperty, 2);
                    cpu4Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu5Grid.SetValue(Grid.ColumnProperty, 1);
                    cpu5Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu5Grid.SetValue(Grid.RowProperty, 2);
                    cpu5Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu6Grid.SetValue(Grid.ColumnProperty, 3);
                    cpu6Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu6Grid.SetValue(Grid.RowProperty, 2);
                    cpu6Grid.SetValue(Grid.RowSpanProperty, 1);

                    cpu7Grid.SetValue(Grid.ColumnProperty, 4);
                    cpu7Grid.SetValue(Grid.ColumnSpanProperty, 1);
                    cpu7Grid.SetValue(Grid.RowProperty, 2);
                    cpu7Grid.SetValue(Grid.RowSpanProperty, 1);

                    column2.Width = GridLength.Auto;

                    cpuLineCharts[0] = new LineChart(cpu0Chart, cpuPerfCounter[0], "Percentage", "CPU 1:");
                    cpuLineCharts[1] = new LineChart(cpu1Chart, cpuPerfCounter[1], "Percentage", "CPU 2:");
                    cpuLineCharts[2] = new LineChart(cpu2Chart, cpuPerfCounter[2], "Percentage", "CPU 3:");
                    cpuLineCharts[3] = new LineChart(cpu3Chart, cpuPerfCounter[3], "Percentage", "CPU 4:");
                    cpuLineCharts[4] = new LineChart(cpu4Chart, cpuPerfCounter[4], "Percentage", "CPU 5:");
                    cpuLineCharts[5] = new LineChart(cpu5Chart, cpuPerfCounter[5], "Percentage", "CPU 6:");
                    cpuLineCharts[6] = new LineChart(cpu6Chart, cpuPerfCounter[6], "Percentage", "CPU 7:");
                    cpuLineCharts[7] = new LineChart(cpu7Chart, cpuPerfCounter[7], "Percentage", "CPU 8:");


                    break;
                default:
                    break;
            }
        }

        private void diskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (diskComboBox.SelectedItem == diskComboBox.Items[0] || diskComboBox.SelectedItem == diskComboBox.Items[diskComboBox.Items.IndexOf("_Total")])
            {
                return;
            }
            else if (diskLineChart == null)
            {
                diskLineChart = new LineChart(diskIOChart, new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", (string)diskComboBox.SelectedItem), new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", (string)diskComboBox.SelectedItem), "MiB/s", "Lese-/Schreibrate");
            }
            else
            {
                diskLineChart.changePerfCounter(new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", (string)diskComboBox.SelectedItem), new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", (string)diskComboBox.SelectedItem));
            }
        }
    }
}
