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
using System.Windows.Shapes;

namespace TaskOrganizer.Windows
{
    /// <summary>
    /// Interaction logic for NewTask.xaml
    /// </summary>
    public partial class NewTask : Window
    {
        public delegate void CreateTaskHandler(string title, Priority priority, DateTime date);
        public event CreateTaskHandler TaskCreated;
        public enum Priority
        {
            VeryHigh, High, Normal, Low, VeryLow
        }
        public NewTask()
        {
            InitializeComponent();
            priority.ItemsSource = Enum.GetValues(typeof(Priority));
        }

        private void CloseHandler(object sender)
        {
            this.Close();
        }

        private void CreateHandler(object sender, RoutedEventArgs e)
        {
            TaskCreated?.Invoke(title.Text, (Priority)priority.SelectedItem, date.SelectedDate.Value);
            this.Close();
        }
    }
}
