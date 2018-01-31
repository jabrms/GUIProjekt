using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            fenster.Content = new Dashboard();
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
            netzwerk.Background = Brushes.SkyBlue;
            leistung.Background = Brushes.SkyBlue;
            prozesse.Background = Brushes.SkyBlue;
            ereignisse.Background = Brushes.SkyBlue;
        }
        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            fenster.Content = new Dashboard();
            Cursor = Cursors.Arrow;
        }
        private void Hardware_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            fenster.Content = new Hardware();
            Cursor = Cursors.Arrow;

        }
        private void Netzwerk_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            fenster.Content = new Netzwerk();
            Cursor = Cursors.Arrow;
        }
        private void Leistung_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            fenster.Content = new Leistung();
            Cursor = Cursors.Arrow;
        }
        private void Prozesse_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            fenster.Content = new Prozesse();
            Cursor = Cursors.Arrow;
        }
        private void Ereignisse_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            fenster.Content = new Ereignisse();
            Cursor = Cursors.Arrow;
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
            else if (welchesFrame == "Netzwerk")
            {
                farbeAendern(netzwerk);
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
