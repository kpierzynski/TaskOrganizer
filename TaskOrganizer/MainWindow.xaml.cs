using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using TaskOrganizer.Components.Tasks;
using TaskOrganizer.Windows;
using static TaskOrganizer.Components.Bar.Bar;
using static TaskOrganizer.Windows.NewTask;

namespace TaskOrganizer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {
        List<Task> tasks;
        FileStream fs;

        Sortings sort = Sortings.ByPriority;

        public MainWindow( ) {
            InitializeComponent( );

            try {
                tasks = new List<Task>( );
                fs = File.Open( "tasks.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite );
                using (var file = new StreamReader( fs, leaveOpen: true )) {
                    string line;
                    while (( line = file.ReadLine( ) ) != null) {
                        Trace.WriteLine( line );
                        string[] props = line.Split( "," );

                        tasks.Add( new Task( ) {
                            Data = {
                                Title = props[0],
                                Date = DateTime.Parse( props[1] ),
                                Priority = Enum.Parse<Priority>( props[2] ),
                                IsDone = Boolean.Parse( props[3] )
                            }
                        } );
                        Trace.WriteLine( tasks.Count );
                    }
                }
            } catch (Exception ex) {
                if (ex is ArgumentNullException || ex is FormatException || ex is ArgumentException) {
                    MessageBox.Show( "Invalid file with tasks!" );
                }
            } finally {
                Sort( );
            }
        }

        protected override void OnClosing( CancelEventArgs e ) {
            base.OnClosing( e );
        }

        private void PinningHandler( object sender, bool isPinned ) {
            mainWindow.Topmost = isPinned;
        }

        private void NewTaskHandlerButton( object sender, RoutedEventArgs e ) {
            NewTask( );
        }

        private void NewTaskHandlerBar( object sender ) {
            NewTask( );
        }

        private void NewTask( ) {
            Effect = new BlurEffect( );

            NewTask dialog = new NewTask( ) {
                Owner = this
            };
            dialog.TaskCreated += TaskCreated;
            dialog.ShowDialog( );
            Effect = null;
        }

        private void TaskCreated( string title, NewTask.Priority priority, DateTime date ) {
            Task p = new Task( ) {
                Data = {
                    Title = title,
                    Priority = priority,
                    Date = date
                }
            };
            tasks.Add( p );

            using (var file = new StreamWriter( fs, leaveOpen: true )) {
                file.WriteLine( p.ToString( ) );
            }

            Sort( );
        }

        private void Sort( ) {
            switch (sort) {
                case Sortings.ByPriority:
                    tasks.Sort( ( x, y ) => x.Data.Priority.CompareTo( y.Data.Priority ) );
                    break;
                case Sortings.ByName:
                    tasks.Sort( ( x, y ) => x.Data.Title.CompareTo( y.Data.Title ) );
                    break;
                case Sortings.ByDate:
                    tasks.Sort( ( x, y ) => x.Data.Date.CompareTo( y.Data.Date ) );
                    break;
                case Sortings.ByDone:
                    tasks.Sort( ( x, y ) => x.Data.IsDone.CompareTo( y.Data.IsDone ) );
                    break;
            }
            taskContainer.Children.Clear( );
            foreach (var task in tasks)
                taskContainer.Children.Add( task );
        }

        private void SortingHandler( object sender, Sortings sort ) {
            this.sort = sort;
            Sort( );
        }
    }
}
