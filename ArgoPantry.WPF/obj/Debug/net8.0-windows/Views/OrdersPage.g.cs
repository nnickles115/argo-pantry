﻿#pragma checksum "..\..\..\..\Views\OrdersPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C6C6492115EFB96DA10C1BE983DFD8866F641B9A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ArgoPantry.WPF.Behaviors;
using ArgoPantry.WPF.Helpers;
using ArgoPantry.WPF.ViewModels;
using ArgoPantry.WPF.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Converters;
using Wpf.Ui.Markup;


namespace ArgoPantry.WPF.Views {
    
    
    /// <summary>
    /// OrdersPage
    /// </summary>
    public partial class OrdersPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Views\OrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ArgoPantry.WPF.Views.OrdersPage Root;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Views\OrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\OrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.CardControl Header;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Views\OrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.AutoSuggestBox StudentId_SearchBox;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\Views\OrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.CardControl Footer;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\Views\OrdersPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.TextBox Barcode_TextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ArgoPantry.WPF;component/views/orderspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\OrdersPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Root = ((ArgoPantry.WPF.Views.OrdersPage)(target));
            
            #line 20 "..\..\..\..\Views\OrdersPage.xaml"
            this.Root.Loaded += new System.Windows.RoutedEventHandler(this.OnLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.Header = ((Wpf.Ui.Controls.CardControl)(target));
            return;
            case 4:
            this.StudentId_SearchBox = ((Wpf.Ui.Controls.AutoSuggestBox)(target));
            
            #line 63 "..\..\..\..\Views\OrdersPage.xaml"
            this.StudentId_SearchBox.SuggestionChosen += new Wpf.Ui.Controls.TypedEventHandler<Wpf.Ui.Controls.AutoSuggestBox, Wpf.Ui.Controls.AutoSuggestBoxSuggestionChosenEventArgs>(this.SearchBox_SuggestionChosen);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 92 "..\..\..\..\Views\OrdersPage.xaml"
            ((Wpf.Ui.Controls.ListView)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ListView_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 93 "..\..\..\..\Views\OrdersPage.xaml"
            ((Wpf.Ui.Controls.ListView)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListView_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Footer = ((Wpf.Ui.Controls.CardControl)(target));
            return;
            case 7:
            this.Barcode_TextBox = ((Wpf.Ui.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

