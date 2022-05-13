using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace TaskOrganizer.Components
{
    public partial class Icon : UserControl, INotifyPropertyChanged
    {
        private bool isRounded = false;
        public bool IsRounded
        {
            get { return isRounded; }
            set
            {
                if (value == isRounded) return;
                isRounded = value;
                OnPropertyChanged("IsRounded");
             }
        }

        private int size = 30;
        public int Size { 
            get { return size; }
            set { 
                if (value == size) return;
                size = value;
                OnPropertyChanged("Size");
            } 
        }

        private Types type = Types.Close;
        public Types Type { 
            get { return type; } 
            set {
                if (value == type) return;
                type = value;
                OnPropertyChanged("Path");
            } 
        }

        private Brush fill = new SolidColorBrush(Colors.White);
        public Brush Fill
        {
            get { return fill; }
            set { 
                if (value == fill) return; 
                fill = value; 
                OnPropertyChanged("Fill");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public delegate void IconClickedHandler(object sender);
        public event IconClickedHandler? IconClicked;

        public enum Types
        {
            Close, Minimize, Maximize, PlusBox, PinOff, PinOn, Menu, Date, SortName, SortDate, SortNum, Home, SortDone
        }

        public string Path
        {
            get
            {
                return icons[type];
            }
        }

        Dictionary<Types, String> icons = new Dictionary<Types, String>()
        {
            {Types.Close, "M19,6.41L17.59,5L12,10.59L6.41,5L5,6.41L10.59,12L5,17.59L6.41,19L12,13.41L17.59,19L19,17.59L13.41,12L19,6.41Z" },
            {Types.Minimize, "M20,14H4V10H20"},
            {Types.Maximize,"M4,4H20V20H4V4M6,8V18H18V8H6Z" },
            {Types.PlusBox, "M17,13H13V17H11V13H7V11H11V7H13V11H17M19,3H5C3.89,3 3,3.89 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3Z" },
            {Types.PinOff, "M8,6.2V4H7V2H17V4H16V12L18,14V16H17.8L14,12.2V4H10V8.2L8,6.2M20,20.7L18.7,22L12.8,16.1V22H11.2V16H6V14L8,12V11.3L2,5.3L3.3,4L20,20.7M8.8,14H10.6L9.7,13.1L8.8,14Z" },
            {Types.PinOn, "M16,12V4H17V2H7V4H8V12L6,14V16H11.2V22H12.8V16H18V14L16,12M8.8,14L10,12.8V4H14V12.8L15.2,14H8.8Z" },
            {Types.Menu, "M3,6H21V8H3V6M3,11H21V13H3V11M3,16H21V18H3V16Z" },
            {Types.Date, "M9,10H7V12H9V10M13,10H11V12H13V10M17,10H15V12H17V10M19,3H18V1H16V3H8V1H6V3H5C3.89,3 3,3.9 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5A2,2 0 0,0 19,3M19,19H5V8H19V19Z" },
            {Types.SortName, "M19 17H22L18 21L14 17H17V3H19M11 13V15L7.67 19H11V21H5V19L8.33 15H5V13M9 3H7C5.9 3 5 3.9 5 5V11H7V9H9V11H11V5C11 3.9 10.11 3 9 3M9 7H7V5H9Z" },
            {Types.SortDate, "M21 17H24L20 21L16 17H19V3H21V17M8 16H11V13H8V16M13 5H12V3H10V5H6V3H4V5H3C1.89 5 1 5.89 1 7V18C1 19.11 1.89 20 3 20H13C14.11 20 15 19.11 15 18V7C15 5.89 14.11 5 13 5M3 18L3 11H13L13 18L3 18Z" },
            {Types.Home, "M10,20V14H14V20H19V12H22L12,3L2,12H5V20H10Z" },
            {Types.SortNum, "M19 17H22L18 21L14 17H17V3H19V17M9 13H7C5.9 13 5 13.9 5 15V16C5 17.11 5.9 18 7 18H9V19H5V21H9C10.11 21 11 20.11 11 19V15C11 13.9 10.11 13 9 13M9 16H7V15H9V16M9 3H7C5.9 3 5 3.9 5 5V9C5 10.11 5.9 11 7 11H9C10.11 11 11 10.11 11 9V5C11 3.9 10.11 3 9 3M9 9H7V5H9V9Z" },
            {Types.SortDone, "M19 17H22L18 21L14 17H17V3H19V17M9 13H5C3.89 13 3 13.89 3 15V19C3 20.11 3.89 21 5 21H9C10.11 21 11 20.11 11 19V15C11 13.89 10.11 13 9 13M6.27 19.5L3.74 16.95L4.81 15.9L6.28 17.39L9.2 14.5L10.26 15.55L6.27 19.5M9 3H5C3.89 3 3 3.89 3 5V9C3 10.11 3.89 11 5 11H9C10.11 11 11 10.11 11 9V5C11 3.89 10.11 3 9 3M9 9H5V5H9V9Z" }
        };

        public Icon()
        {
            InitializeComponent();
        }

        private void ClickHandler(object sender, RoutedEventArgs e)
        {
            IconClicked?.Invoke(this);
        }
    }
}
