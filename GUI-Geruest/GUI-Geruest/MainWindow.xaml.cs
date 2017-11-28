using System;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Naviagtion

        private void farbeAendern(Button click)
        {
            click.Background = Brushes.White;
        }

        private void standardFarbe()
        {
            dashboard.Background = Brushes.SkyBlue;
            hardware.Background = Brushes.SkyBlue;
            festplatte.Background = Brushes.SkyBlue;
            leistung.Background = Brushes.SkyBlue;
            prozesse.Background = Brushes.SkyBlue;
            ereignisse.Background = Brushes.SkyBlue;
        }
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            fenster.Content = new Dashboard();
        }
        private void Hardware_Click(object sender, RoutedEventArgs e)
        {
            fenster.Content = new Hardware();
            
        }
        private void Festplatte_Click(object sender, RoutedEventArgs e)
        {
            fenster.Content = new Festplatte();
        }
        private void Leistung_Click(object sender, RoutedEventArgs e)
        {
            fenster.Content = new Leistung();
        }
        private void Prozesse_Click(object sender, RoutedEventArgs e)
        {
            fenster.Content = new Prozesse();
        }
        private void Ereignisse_Click(object sender, RoutedEventArgs e)
        {
            fenster.Content = new Ereignisse();
        }

        private void fenster_ContentRendered(object sender, EventArgs e)    //Makiert Navigations Butten -> akt. Frame
        {
            string welchesFrame ="";
            welchesFrame = fenster.Content.ToString(); 
            welchesFrame = welchesFrame.Substring(12);  //TODO name des Nmaespace abfragen -> laenge bestimmen
            standardFarbe();
            if(welchesFrame == "Dashboard"){
                farbeAendern(dashboard);
            }
            else if (welchesFrame == "Hardware")
            {
                farbeAendern(hardware);
            }
            else if (welchesFrame == "Festplatte")
            {
                farbeAendern(festplatte);
            }
            else if (welchesFrame == "Leistung")
            {
                farbeAendern(leistung);
            }
            else if (welchesFrame == "Prozesse")
            {
                farbeAendern(prozesse);
            }
            else if (welchesFrame == "Ereignisse")
            {
                farbeAendern(ereignisse);
            }
        }
    }
}
