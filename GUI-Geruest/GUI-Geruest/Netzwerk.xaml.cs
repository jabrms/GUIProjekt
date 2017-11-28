using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
            contentAendern();
        }

        string[] ipAdressen;
        string[] SubnetMasken;
        string[] DefaultGateways;
        string NetworkCard;
        string MACAddress;
        string hostname;
        private void NetzwerkInfo()
        {
            ManagementObjectSearcher NetworkInfo = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection moCollection = NetworkInfo.Get();
            foreach (ManagementObject mo in moCollection)
            {
                ipAdressen = (string[])mo["IPAddress"];
                SubnetMasken = (string[])mo["IPSubnet"];
                DefaultGateways = (string[])mo["DefaultIPGateway"];
                NetworkCard = mo["Description"].ToString();
                MACAddress = mo["MACAddress"].ToString();
                hostname = mo["DNSHostName"].ToString();
            }
        }
        private void contentAendern()
        {
            NetzwerkInfo();
            /*
            foreach(string ipA in ipAdressen)
            {
                ip_info.Content += ipA + "\n";     
            }

            foreach (string subm in SubnetMasken)
            {
                sm_info.Content += subm + "\n";
            }

            foreach (string gw in DefaultGateways)
            {
                gw_info.Content += gw + "\n";
            }
            //TODO bei meht Details button
            */
            ip_info.Content = ipAdressen[0];
            sm_info.Content = SubnetMasken[0];
            gw_info.Content = DefaultGateways[0];
            mac_info.Content = MACAddress;
            desc_info.Content = NetworkCard;
            hn_info.Content = hostname;
         }
    }
}