using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Text.Json;
using System.Xml.Schema;
using System.Xml.Serialization;
using TaskOrganizer.Windows;
using static TaskOrganizer.Windows.NewTask;
using System.Reflection;

namespace TaskOrganizer.Components.Tasks {
    /// <summary>
    /// Interaction logic for Task.xaml
    /// </summary>
    /// 
    public class PriorityOptions {
        public String Title {
            get; set;
        }
        public SolidColorBrush Color {
            get; set;
        }
    }

    public class TaskData : INotifyPropertyChanged {

        public static Dictionary<Priority, PriorityOptions> tags = new Dictionary<Priority, PriorityOptions>( ) {
            {NewTask.Priority.VeryHigh, new PriorityOptions(){Title = "Very high", Color = new SolidColorBrush(Colors.Red)} },
            {NewTask.Priority.High, new PriorityOptions(){Title = "High", Color = new SolidColorBrush(Colors.Orange)} },
            {NewTask.Priority.Normal, new PriorityOptions(){Title = "Normal", Color = new SolidColorBrush(Colors.Green)} },
            {NewTask.Priority.Low, new PriorityOptions(){Title = "Low", Color = new SolidColorBrush(Colors.Blue)} },
            {NewTask.Priority.VeryLow, new PriorityOptions(){Title = "Very low", Color = new SolidColorBrush(Colors.Purple)} },
        };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string name = null ) {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( name ) );
        }

        public override string ToString( ) {
            return title + "," + date.ToShortDateString( ) + "," + priority.ToString( ) + "," + isDone.ToString( );
        }

        string title;
        DateTime date;
        Priority priority;
        Boolean isDone;

        public String Title {
            get {
                return title;
            }
            set {
                title = value;
                OnPropertyChanged( );
            }
        }
        public Priority Priority {
            get {
                return priority;
            }
            set {
                priority = value;
                OnPropertyChanged( );
                OnPropertyChanged( "PriorityString" );
                OnPropertyChanged( "Color" );
            }
        }

        public String PriorityString {
            get {
                return tags[ priority ].Title;
            }
        }

        public DateTime Date {
            get {
                return date;
            }
            set {
                date = value;
                OnPropertyChanged( );
                OnPropertyChanged( "DateString" );
            }
        }

        public String DateString {
            get {
                return date.ToShortDateString( );
            }
        }

        public Boolean IsDone {
            get {
                return isDone;
            }
            set {
                isDone = value;
                OnPropertyChanged( );
            }
        }

        public SolidColorBrush Color {
            get {
                Trace.WriteLine( priority.ToString( ) + "OMG");
                return tags[ priority ].Color;
            }
        }
    }
    public partial class Task : UserControl, INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged( [CallerMemberName] string name = null ) {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( name ) );
        }

        TaskData taskData;
        public TaskData Data {
            get {
                return taskData;
            }
            set {
                taskData = value;
                OnPropertyChanged( );
            }
        }

        public Task( ) {
            taskData = new TaskData( );
            InitializeComponent( );
            DataContext = this;
        }

        public override string ToString( ) {
            return taskData.ToString( );
        }
    }
}
