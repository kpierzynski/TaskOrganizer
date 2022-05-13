using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TaskOrganizer.Components.Header
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public delegate void PinHandler(object sender, bool isPinned);
        public event PinHandler Pinned;

        private bool isPinned = false;

        public Header()
        {
            InitializeComponent();
        }

        private void DragHandler(object sender, MouseButtonEventArgs e)
        {
            if( e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 1 )
            {
                Application.Current.MainWindow.DragMove();
            }
        }

        private void CloseHandler(object sender)
        {
            Application.Current.Shutdown();
        }

        private void PinningHandler(object sender)
        {
            isPinned = !isPinned;
            pinIcon.Type = isPinned ? Icon.Types.PinOn : Icon.Types.PinOff;
            Pinned?.Invoke(this, isPinned);
        }
    }
}
