using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
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
using System.Threading;
using System.Windows.Threading;


namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Hardware.xaml
    /// </summary>
    public partial class Hardware : Page
    {
        Computer pc;
        DispatcherTimer perfCountTimer = new DispatcherTimer();
        public Hardware()
        {
            InitializeComponent();
            pc = new Computer();
            pc.CPUEnabled = true;
            pc.FanControllerEnabled = true;
            pc.GPUEnabled = true;
            pc.HDDEnabled = true;
            pc.RAMEnabled = true;
            pc.Open();

            //perfCountTimer.Tick += PerfCountTimer_Tick;
            //perfCountTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            //perfCountTimer.IsEnabled = true;
            PerfCountTimer_Tick();
        }

        private void PerfCountTimer_Tick(/*object sender, EventArgs e*/)
        {
            aktualisieren();
        }

            void aktualisieren()
        {
            foreach (var hardwareItem in pc.Hardware)
            {
                //if (hardwareItem.HardwareType == HardwareType.CPU)
                //{
                  
                    hardwareItem.Update();

                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }                     

                    foreach (var sensor in hardwareItem.Sensors)
                    {          
                            temp.Content +=  "Hardware:" + hardwareItem.Name.ToString()+" Sensortype"+(sensor.SensorType.ToString() + " name:" + sensor.Name + " Value: " + sensor.Value.Value.ToString()) + "\n";
                    } 
                //}
            }
        }
    }
}
