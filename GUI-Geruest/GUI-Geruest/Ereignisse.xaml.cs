﻿using System;
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

namespace GUI_Geruest
{
    /// <summary>
    /// Interaktionslogik für Ereignisse.xaml
    /// </summary>
    public partial class Ereignisse : Page
    {
        List<eventLogListe> SystemList = new List<eventLogListe>();
        List<eventLogListe> ApplicationList = new List<eventLogListe>();

        EventLog applicationAnzahl = new EventLog("Application");
        EventLog systemAnzahl = new EventLog("System");
        List<eventLogListe> gesamtList = new List<eventLogListe>();

        public Ereignisse()
        {
           
            InitializeComponent();

            SystemList = eventlogRead(int.Parse(anzahlElemente.Text), "System");
            ApplicationList = eventlogRead(int.Parse(anzahlElemente.Text), "Application");
            //anzEreig.Content = "Anzahl der Ereignisse im Windows Log: " + (eventlogRead(10, "System")); //+eventlogRead(10, "Application") + eventlogRead(10, "Security") + eventlogRead(10, "Setup") + eventlogRead(10, "Forwarded Events"));
            listeSystem.ItemsSource = SystemList;
            listeApplication.ItemsSource = ApplicationList;
        }

        
       

        List<eventLogListe> eventlogRead(int anzahl, string protokoll)
        {
            List<eventLogListe> EventList = new List<eventLogListe>();
            int count = 0;
            int x = 0;

            EventLog log = new EventLog(protokoll);

            gesamtAnzLogs.Content = "Anzahl Logs: " + log.Entries.Count;

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
                    EventList.Add(new eventLogListe { id = entry.InstanceId, catagory = cat, source = entry.Source, timeWritte = entry.TimeWritten, type = entry.EntryType.ToString(), index = entry.Index, message = entry.Message, user = entry.UserName, pc = entry.MachineName, timeGen = entry.TimeGenerated });
                    x++;
                }
            }
            return EventList;
        }

        private void Application_DragOver(object sender, DragEventArgs e)
        {
         //    = "Das Anwendungsprotokoll enthält Ereignisse, die von Anwendungen oder Programmen protokolliert wurden. \nVon einem Datenbankprogramm könnte beispielsweise ein Dateifehler im Anwendungsprotokoll aufgezeichnet werden. \nDie Entwickler des jeweiligen Programms entscheiden, welche Ereignisse protokolliert werden.";
        }

        private void listeEreignisse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var items = e.AddedItems;
            if (items == null)
            {
                return;
            }
            eventLogListe selected = (eventLogListe)items[0];
            message.Text = selected.message;
            eventID.Content = selected.id.ToString();
            typ.Content = selected.type;
            logErst.Content = selected.timeWritte;
            source.Content = selected.source;
            user.Content = selected.user;
            pc.Content = selected.pc;
            zeit.Content = selected.timeGen;
        }

        void listeAktualisieren()
        {
            //TODO nur applikation
            ApplicationList.Clear();
            ApplicationList = eventlogRead(int.Parse(anzahlElemente.Text), "Application");
            listeApplication.ItemsSource = ApplicationList;
            SystemList.Clear();
            SystemList = eventlogRead(int.Parse(anzahlElemente.Text), "System");
            listeSystem.ItemsSource = SystemList;
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
        //private void anzahlElemente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ApplicationList.Clear();
        //    eventlogRead(anzahlElemente.SelectedIndex, "Application");
        //    listeApplication.ItemsSource = ApplicationList;
        //}
        ////TODO wie viele Events anzeigen

        //TODO Header sotieren implementieren
    }
}


