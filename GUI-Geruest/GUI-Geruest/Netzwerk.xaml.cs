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
        string dhcpServer;
        string[] dnsServer;
        string dhcpAktiv;    
        string dnsSuffix;
        private void NetzwerkInfo()
        {
            ManagementObjectSearcher NetworkInfo = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection moCollection = NetworkInfo.Get();
            foreach (ManagementObject mo in moCollection)
            {
                try //Wenn Einträge nicht vorhanden sind
                {
                    ipAdressen = (string[])mo["IPAddress"];
                    SubnetMasken = (string[])mo["IPSubnet"];
                    DefaultGateways = (string[])mo["DefaultIPGateway"];
                    NetworkCard = mo["Description"].ToString();
                    MACAddress = mo["MACAddress"].ToString();
                    hostname = mo["DNSHostName"].ToString();
                    dhcpServer = (string)mo["DHCPServer"];
                    dnsServer = (string[])mo["DNSServerSearchOrder"];
                    dnsSuffix = (string)mo["DNSDomain"];
                    dhcpAktiv = mo["DHCPEnabled"].ToString();
                }
                catch { }

            }
        }

        void contentLoeschen()
        {
            ip_info.Content = null;
            sm_info.Content = null;
            gw_info.Content = null;
            mac_info.Content = null;
            desc_info.Content = null;
            hn_info.Content = null;
            dhcp.Content = null;
            dhcp_info.Content = null;
            dns.Content = null;
            dns_info.Content = null;
            dnsSuf_info.Content = null;
            dnsSuf.Content = null;
        }
        private void contentAendern()
        {
            NetzwerkInfo();
            ip_info.Content = ipAdressen[0];
            sm_info.Content = SubnetMasken[0];
            gw_info.Content = DefaultGateways[0];
            mac_info.Content = MACAddress;
            desc_info.Content = NetworkCard;
            hn_info.Content = hostname;
            dhcpEnabled_info.Content = dhcpAktiv;
         }

        private void mehrZeigen_Click(object sender, RoutedEventArgs e)
        {
            if (mehrZeigen.Background == Brushes.SkyBlue)
            {
                mehrZeigen.Background = Brushes.White;
                wenigerZeigen.Background = Brushes.SkyBlue;

                contentLoeschen();

                foreach (string ipA in ipAdressen)
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

                mac_info.Content = MACAddress;
                desc_info.Content = NetworkCard;
                hn_info.Content = hostname;

                ip.Content = "IPv4-Adresse" + "\nLink-Lokale IPv6-Adresse:" + "\ntemporäre IPv6-Adresse:" + "\nIPv6-Adresse:";

                dhcp.Content = "DHCP-Server:";
                dhcp_info.Content = dhcpServer;
                dns.Content = "DNS-Server:";
                foreach(string dn in dnsServer)
                {
                    dns_info.Content += dn + "\n";
                }
                
                dnsSuf.Content = "DNS-Suffix:";
                dnsSuf_info.Content = dnsSuffix;
            }

        }

        private void wenigerZeigen_Click(object sender, RoutedEventArgs e)
        {
            if (wenigerZeigen.Background == Brushes.SkyBlue)
            {
                mehrZeigen.Background = Brushes.SkyBlue;
                wenigerZeigen.Background = Brushes.White;
                ip.Content = "IP-Adresse:";
                contentLoeschen();
                contentAendern();
            }

        }
    }

}