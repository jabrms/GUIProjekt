using System;
using System.Management;
using System.Collections.Generic;
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



namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Festplatte.xaml
    /// </summary>
    public partial class Netzwerk : Page
    {
        public Netzwerk()
        {
            InitializeComponent();
        }
        
        //string[] Adressen;
        //string[] SubnetMasken;
        //string[] DefaultGateways;
        //string NetworkCard;
        //string MACAddress;
        void NetzwerkInfo()
        {     
           // ManagementObjectSearcher NetworkInfo = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            //ManagementObjectCollection MOC = NetworkInfo.Get();
            //foreach (ManagementObject mo in MOC)
            //{
            //    Adressen = (string[])mo["IPAddress"];
            //    SubnetMasken = (string[])mo["IPSubnet"];
            //    DefaultGateways = (string[])mo["DefaultIPGateway"];

            //    NetworkCard = mo["Description"].ToString();
            //    MACAddress = mo["MACAddress"].ToString();
            //}
        }
    }
}