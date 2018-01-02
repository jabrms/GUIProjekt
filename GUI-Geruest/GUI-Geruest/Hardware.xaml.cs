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
using System.Management;


namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Hardware.xaml
    /// </summary>
    public partial class Hardware : Page
    {
        string prozessor = "Win32_Processor";

        Computer pc;

        //DispatcherTimer perfCountTimer = new DispatcherTimer();

        string textboxBefuellen(IHardware hw)
        {
            hw.Update();

            foreach (IHardware subHardware in hw.SubHardware)
            {
                subHardware.Update();
            }
            
            string zurueck = "\n" + hw.Sensors[0].SensorType.ToString();
            string sensorType = hw.Sensors[0].SensorType.ToString(); //pruefen ob gleicher type
            string einheit = "";

            foreach (var sensor in hw.Sensors)
            {
                if(sensorType != sensor.SensorType.ToString())
                {
                    zurueck += "\n" + sensor.SensorType.ToString();
                    sensorType = sensor.SensorType.ToString();
                }

                if (sensorType == "Load" || sensorType == "Control" || sensorType == "Level")
                {
                    einheit = " %";
                }

                else if (sensorType == "Fan")
                {
                    einheit = " RPM";
                }

                else if (sensorType == "Data" && (hw.HardwareType == HardwareType.GpuAti || hw.HardwareType == HardwareType.GpuNvidia))
                {
                    einheit = "MB";
                }    

                else if (sensorType == "Data")
                {
                    einheit = "GB";
                }

                else if (sensorType == "Temperature")
                {
                    einheit = " °C";
                }

                else if (sensorType == "Clock")
                {
                    einheit = " MHz";
                }

                else if (sensorType == "Power")
                {
                    einheit = " W";
                }

                else if (sensorType == "Flow")
                {
                    einheit = " L/h";
                }
                zurueck += "\n\t\t" + sensor.Name.ToString() + "\t Value: " + sensor.Value.Value.ToString() + einheit + "\tMax: " + sensor.Max?.ToString() + einheit +"\n";
            }
            return zurueck;
        }

        public Hardware()
        {
            InitializeComponent();

            pc = new Computer();
            pc.CPUEnabled = true;
            pc.FanControllerEnabled = true;
            pc.GPUEnabled = true;
            pc.HDDEnabled = true;
            pc.RAMEnabled = true;
            pc.MainboardEnabled = true;
            pc.Open();


            foreach (var hardwareItem in pc.Hardware)
            {
               this.Dispatcher.Invoke/* new Thread*/(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    if (hardwareItem.HardwareType == HardwareType.Mainboard)
                        mainboard.Content = "Mainboard:\t" + hardwareItem.Name;

                    else if (hardwareItem.HardwareType == HardwareType.CPU)
                        cpu.Content = "CPU:\t" + hardwareItem.Name + textboxBefuellen(hardwareItem);

                    else if (hardwareItem.HardwareType == HardwareType.RAM)
                        ram.Content = "RAM:\t" + hardwareItem.Name + textboxBefuellen(hardwareItem);

                    else if (hardwareItem.HardwareType == HardwareType.HDD)
                    {
                        hdd.Content += "\nHDD:\t" + hardwareItem.Name + textboxBefuellen(hardwareItem);
                    }

                    else if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
                    {
                        gpu.Content = "GPU:\t" + hardwareItem.Name + textboxBefuellen(hardwareItem);
                    }
                    else        //vorlaufig
                    {
                        MessageBox.Show(hardwareItem.Name + textboxBefuellen(hardwareItem));
                    }
                })/*.Start()*/;
            }
                
            
            cpuName.Content = "Name:\t\t\t\t" + getComponent(prozessor, "Name");
            cpuArchitecture.Content = "Architektur:\t\t\t" + getComponent(prozessor, "Architecture");
            cpuStatus.Content = "Status:\t\t\t\t" + getComponent(prozessor, "CpuStatus");
            cpuSpeed.Content = "aktuelle Geschwindigkeit:\t\t" + getComponent(prozessor, "CurrentClockSpeed") + " MHz";
            cpuVoltage.Content = "StromSpannung:\t\t\t" + getComponent(prozessor, "CurrentVoltage") + " V"; //wenn nur 6 bit -> *10
            cpuDescription.Content = "Beschreibung:\t\t\t" + getComponent(prozessor, "Description");
            //cpuInstallDate.Content = "Einbau Datum:\t" + getComponent(prozessor, "InstallDate");
            cpuL2size.Content = "L2Cache Größe:\t\t\t" + getComponent(prozessor, "L2CacheSize") + " KByte";
            cpuL2speed.Content = "L2Cache Geschwindigkeit:\t\t" + getComponent(prozessor, "L2CacheSpeed") + " MHz";
            cpuL3size.Content = "L3Cache Größe:\t\t\t" + getComponent(prozessor, "L3CacheSize") + " KByte";
            cpuL3speed.Content = "L3Cache Geschwindigkeit:\t\t" + getComponent(prozessor, "L3CacheSpeed") + " MHz";
            cpuLoadPercentage.Content = "Auslastung:\t\t\t" + getComponent(prozessor ,"LoadPercentage") + "%";
            cpuManafacture.Content = "Hersteller:\t\t\t" + getComponent(prozessor, "Manufacturer");
            cpuMaxSpeed.Content = "maximale Geschwindigkeit:\t\t" + getComponent(prozessor, "MaxClockSpeed") + " MHz";
            cpuNumberCore.Content = "Anzahl Kerne:\t\t\t" + getComponent(prozessor, "NumberOfCores");
            //cpuSerialNumber.Content = "Seriennummer:\t\t\t" + getComponent(prozessor, "SerialNumber") + " (Erst ab Windows 10 / Windows Server 2016 unterstützt)";
            //cpuSocket.Content = "Socket:\t\t\t\t" + getComponent(prozessor, "SocketDesignation");
            cpuVirtualizatian.Content = "Virtualisierung im BIOS aktiviert:\t " + getComponent(prozessor, "VirtualizationFirmwareEnabled");
            //cpuTemperature.Content = 
        }

        private string getComponent(string Hardawareclass, string syntax)
        {
            string info = null;

            ManagementObjectSearcher hw = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + Hardawareclass);
            foreach (ManagementObject h in hw.Get())
            {
                info = h[syntax]?.ToString();
            }
            if (syntax == "Architecture")
            {
                info = architecturePruefen(info);
            }
            else if (syntax == "CpuStatus")
            {
                info = statusPruefen(info);
            }
            return info;
        }

        string architecturePruefen(string info)
        {
            switch (info)   //TODO wenn keine der Zhalen default
            {
                case "0": info = "x86"; break;
                case "1": info = "MIPS"; break;
                case "2": info = "Alpha"; break;
                case "3": info = "PowerPC"; break;
                case "6": info = "ia64"; break;
                case "9": info = "x64"; break;
            }
            return info;
        }

        string statusPruefen(string info)
        {
            switch (info)   //TODO wenn keine der Zhalen default
            {
                case "0": info = "Unknown"; break;
                case "1": info = "CPU Enabled"; break;
                case "2": info = "CPU Disabled by User via BIOS Setup"; break;
                case "3": info = "CPU Disabled By BIOS (POST Error)"; break;
                case "4": info = "CPU is Idle"; break;
                case "5":
                case "6": info = "Reserved "; break;
                case "7": info = "Other"; break;
            }
            return info;
        }
    }


}
