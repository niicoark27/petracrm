﻿#pragma checksum "..\..\..\Pages\pageCorrespondence.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D1DD751454624B531C25D67CF793534C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace petracrm.Pages {
    
    
    /// <summary>
    /// pageCorrespondence
    /// </summary>
    public partial class pageCorrespondence : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal petracrm.Pages.pageCorrespondence winCorrespondence;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgCorrespondence;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtID;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDescription;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbCategories;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\pageCorrespondence.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCode;
        
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
            System.Uri resourceLocater = new System.Uri("/petracrm;component/pages/pagecorrespondence.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\pageCorrespondence.xaml"
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
            this.winCorrespondence = ((petracrm.Pages.pageCorrespondence)(target));
            
            #line 7 "..\..\..\Pages\pageCorrespondence.xaml"
            this.winCorrespondence.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgCorrespondence = ((System.Windows.Controls.DataGrid)(target));
            
            #line 14 "..\..\..\Pages\pageCorrespondence.xaml"
            this.dgCorrespondence.PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dgCorrespondence_PreviewMouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Pages\pageCorrespondence.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Pages\pageCorrespondence.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cmbCategories = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.txtCode = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

