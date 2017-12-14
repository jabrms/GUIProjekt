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
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Ereignisse.xaml
    /// </summary>
    public partial class Ereignisse : Page
    {
        //Listen fuer Listview
        List<eventLogListe> SystemList = new List<eventLogListe>();
        List<eventLogListe> ApplicationList = new List<eventLogListe>();
        List<eventLogListe> gesamtList = new List<eventLogListe>();

        //für TextBox "Anzahl anzeigen
        EventLog applicationAnzahl = new EventLog("Application");
        EventLog systemAnzahl = new EventLog("System");
        int anzahlLogsGes, anzahlLogsSys, anzahlLogsApp;

        //Header sotieren
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        string tabItem = null; //welcher Tab gerade gewaehlt

        public Ereignisse()
        {
            InitializeComponent();
            gesamtList = eventlogReadAll();
            listeGesamt.ItemsSource = gesamtList;
            SystemList = eventlogRead("System");
            listeSystem.ItemsSource = SystemList;
            ApplicationList = eventlogRead("Application");
            listeApplication.ItemsSource = ApplicationList;
        }

        List<eventLogListe> eventlogReadAll()
        {
            List<eventLogListe> EventList = new List<eventLogListe>();
            int count = 0;
            int x = 0;
            int anzahl = 0;

            EventLog logSys = new EventLog("System");
            EventLog logApp = new EventLog("Application");

            anzahlLogsGes = logSys.Entries.Count + logApp.Entries.Count;

            if (anzahlLogsGes >= int.Parse(anzahlElemente.Text))
            {
                anzahl = int.Parse(anzahlElemente.Text);
            }          

            anzahl = anzahl / 2;

            foreach (EventLogEntry entry in logSys.Entries)
            {
                count++;
                if (count > logSys.Entries.Count - anzahl)
                {
                    string cat = entry.Category;
                    if (cat == "(0)")   //fängt "null" ab
                    {
                        cat = "unbekannt";
                    }
                    EventList.Add(new eventLogListe { id = entry.InstanceId, catagory = cat, source = entry.Source, timeWritte = entry.TimeWritten, level = entry.EntryType.ToString(), index = entry.Index, message = entry.Message, user = entry.UserName, pc = entry.MachineName, timeGen = entry.TimeGenerated, type = "System" });
                    x++;
                }
            }

            count = 0;
            x = 0;

            foreach (EventLogEntry entry in logApp.Entries)
            {
                count++;
                if (count > logApp.Entries.Count - anzahl)
                {
                    string cat = entry.Category;
                    if (cat == "(0)")   //fängt "null" ab
                    {
                        cat = "unbekannt";
                    }
                    EventList.Add(new eventLogListe { id = entry.InstanceId, catagory = cat, source = entry.Source, timeWritte = entry.TimeWritten, level = entry.EntryType.ToString(), index = entry.Index, message = entry.Message, user = entry.UserName, pc = entry.MachineName, timeGen = entry.TimeGenerated, type = "Application" });
                    x++;
                }
            }
            return EventList;
        }

        List<eventLogListe> eventlogRead(string protokoll)  //TODO prüfen ob Zahl im rahmen liegt
        {
            List<eventLogListe> EventList = new List<eventLogListe>();
            int count = 0;
            int x = 0;
            int anzahl = 0;

            EventLog log = new EventLog(protokoll);

            if (protokoll == "Application")
            {
                anzahlLogsApp = log.Entries.Count;
                if (anzahlLogsApp >= int.Parse(anzahlElemente.Text))
                {
                    anzahl = int.Parse(anzahlElemente.Text);
                }
            }
            else if(protokoll == "System")
            {
                anzahlLogsSys = log.Entries.Count;
                if (anzahlLogsSys >= int.Parse(anzahlElemente.Text))
                {
                    anzahl = int.Parse(anzahlElemente.Text);
                }         
            } 

            foreach (EventLogEntry entry in log.Entries)
            {
                count++;

                if (count > log.Entries.Count - anzahl)
                {
                    string cat = entry.Category;
                    if (cat == "(0)")   //fängt "null" ab
                    {
                        cat = "unbekannt";
                    }
                    EventList.Add(new eventLogListe { id = entry.InstanceId, catagory = cat, source = entry.Source, timeWritte = entry.TimeWritten, level = entry.EntryType.ToString(), index = entry.Index, message = entry.Message, user = entry.UserName, pc = entry.MachineName, timeGen = entry.TimeGenerated });
                    x++;
                }
            }
            return EventList;
        }

        private void Application_DragOver(object sender, DragEventArgs e)   //TODO beschreibung Header
        {
            //    = "Das Anwendungsprotokoll enthält Ereignisse, die von Anwendungen oder Programmen protokolliert wurden. \nVon einem Datenbankprogramm könnte beispielsweise ein Dateifehler im Anwendungsprotokoll aufgezeichnet werden. \nDie Entwickler des jeweiligen Programms entscheiden, welche Ereignisse protokolliert werden.";
        }

        private void listeEreignisse_SelectionChanged(object sender, SelectionChangedEventArgs e)   //Ausgabe Details
        {
            var items = e.AddedItems;
            if (items.Count == 0 || items == null)
            {
                return;
            }
            eventLogListe selected = (eventLogListe)items[0]; //TODO wenn man nicht wartet fehler || hier auch fehler bei zhal eintippen und okay
            message.Text = selected.message;
            eventID.Content = selected.id.ToString();
            typ.Content = selected.level;
            logErst.Content = selected.timeWritte;
            source.Content = selected.source;
            user.Content = selected.user;
            pc.Content = selected.pc;
            zeit.Content = selected.timeGen;
        }

        void listeAktualisieren()
        {
            ApplicationList = eventlogRead("Application");
            listeApplication.ItemsSource = ApplicationList;
            SystemList = eventlogRead("System");
            listeSystem.ItemsSource = SystemList;
            gesamtList = eventlogReadAll();
            listeGesamt.ItemsSource = gesamtList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listeAktualisieren();
        }

        private void anzahlElemente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                listeAktualisieren();
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)    //ohne denn code kein fehler
        {
            tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;
            if (tabItem == "Application")
            {
                gesamtAnzLogs.Content = "Anzahl Application Logs: " + anzahlLogsApp;
                if (int.Parse(anzahlElemente.Text) > anzahlLogsApp)
                {
                    MessageBox.Show("Es exestieren weniger Elemente als Sie anzeigen lassen wollen", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            else if (tabItem == "System")
            {
                gesamtAnzLogs.Content = "Anzahl System Logs: " + anzahlLogsSys;
                if (int.Parse(anzahlElemente.Text) > anzahlLogsSys)
                {
                    MessageBox.Show("Es exestieren weniger Elemente als Sie anzeigen lassen wollen", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            else if (tabItem == "Übersicht")
            {
                gesamtAnzLogs.Content = "Anzahl Logs: " + anzahlLogsGes;
            }
        }

        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)   //immer bei application
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    string header = headerClicked.Column.Header as string;
                    Sort(header, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header  
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            if (tabItem == "Application")
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(listeApplication.ItemsSource);
                dataView.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }

            else if (tabItem == "System")
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(listeSystem.ItemsSource);
                dataView.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }

            else if (tabItem == "Übersicht")
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(listeGesamt.ItemsSource);
                dataView.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }
            


        }


    }
}


