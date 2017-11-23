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

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        DispatcherTimer perfCountTimer = new DispatcherTimer(); // Timer zur regelmaessigen Abfrage des aktuellen Ressourcenverbrauchs
        public Dashboard()
        {
            InitializeComponent();
            
            perfCountTimer.Tick += PerfCountTimer_Tick;
            perfCountTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            perfCountTimer.IsEnabled = true;
        }

        public PerformanceCounter cpuCounter =
           new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public PerformanceCounter ramCounter =
            new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public Int32 currentCPU = 0;
        public Int32 currentRAM = 0;

        int[] cpuData = new int[18];
        int[] ramData = new int[18];
        private void PerfCountTimer_Tick(object sender, EventArgs e)
        {
            currentCPU = Convert.ToInt32(cpuCounter.NextValue());
            currentRAM = Convert.ToInt32(ramCounter.NextValue());
            cpuData = renewData(cpuData, currentCPU);
            ramData = renewData(ramData, currentRAM);
            progBarCPU0.Value = cpuData[0];
            labelCPU.Content = "CPU-Auslastung: " + currentCPU + "%";
            progBarRAM0.Value = ramData[0];
            labelRAM.Content = "RAM-Auslastung: " + currentRAM + "%";
            progBarCPU1.Value = cpuData[1];
            progBarRAM1.Value = ramData[1];
            progBarCPU2.Value = cpuData[2];
            progBarRAM2.Value = ramData[2];
            progBarCPU3.Value = cpuData[3];
            progBarRAM3.Value = ramData[3];
            progBarCPU4.Value = cpuData[4];
            progBarRAM4.Value = ramData[4];
            progBarCPU5.Value = cpuData[5];
            progBarRAM5.Value = ramData[5];
            progBarCPU6.Value = cpuData[6];
            progBarRAM6.Value = ramData[6];
            progBarCPU7.Value = cpuData[7];
            progBarRAM7.Value = ramData[7];
            progBarCPU8.Value = cpuData[8];
            progBarRAM8.Value = ramData[8];
            progBarCPU9.Value = cpuData[9];
            progBarRAM9.Value = ramData[9];
            progBarCPU10.Value = cpuData[10];
            progBarRAM10.Value = ramData[10];
            progBarCPU11.Value = cpuData[11];
            progBarRAM11.Value = ramData[11];
            progBarCPU12.Value = cpuData[12];
            progBarRAM12.Value = ramData[12];
            progBarCPU13.Value = cpuData[13];
            progBarRAM13.Value = ramData[13];
            progBarCPU14.Value = cpuData[14];
            progBarRAM14.Value = ramData[14];
            progBarCPU15.Value = cpuData[15];
            progBarRAM15.Value = ramData[15];
            progBarCPU16.Value = cpuData[16];
            progBarRAM16.Value = ramData[16];
            progBarCPU17.Value = cpuData[17];
            progBarRAM17.Value = ramData[17];
        }

        static int[] renewData(int[] currentData, int valueToAdd)
        {
            for (int i = 0; i < currentData.Length - 1; i++)
            {
                currentData[i] = currentData[i + 1];
            }
            currentData[currentData.Length - 1] = valueToAdd;
            return currentData;
        }

    }
}
