﻿#pragma checksum "..\..\Prozesse.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5A7D7A8E7F5A601B964584752BF87B5312462F6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GUI_Geruest {
    
    
    /// <summary>
    /// Prozesse
    /// </summary>
    public partial class Prozesse : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\Prozesse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxProcess;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Prozesse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox Prozesse1;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Prozesse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label anzahl;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Prozesse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button go;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Prozesse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button kill;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Prozesse.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button aktualisieren;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GUI-Geruest;component/prozesse.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Prozesse.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ListBoxProcess = ((System.Windows.Controls.ListBox)(target));
            
            #line 13 "..\..\Prozesse.xaml"
            this.ListBoxProcess.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Prozesse1 = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 3:
            this.anzahl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.go = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\Prozesse.xaml"
            this.go.Click += new System.Windows.RoutedEventHandler(this.go_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.kill = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Prozesse.xaml"
            this.kill.Click += new System.Windows.RoutedEventHandler(this.kill_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.aktualisieren = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\Prozesse.xaml"
            this.aktualisieren.Click += new System.Windows.RoutedEventHandler(this.aktualisieren_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

