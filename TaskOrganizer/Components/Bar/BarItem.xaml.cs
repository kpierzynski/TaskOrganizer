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

namespace TaskOrganizer.Components.Bar
{
    /// <summary>
    /// Interaction logic for BarItem.xaml
    /// </summary>
    public partial class BarItem : UserControl
    {
        public delegate void ClickHandler(object sender, Icon.Types type);
        public event ClickHandler Click;

        Icon.Types type;

        public BarItem(Icon.Types type, String title)
        {
            InitializeComponent();
            icon.Type = type;
            this.type = type;
            this.title.Text = title;
            this.title.Visibility = Visibility.Collapsed;
        }

        public void SwitchVisibility()
        {
            if( title.Visibility == Visibility.Collapsed ) title.Visibility = Visibility.Visible;
            else title.Visibility = Visibility.Collapsed;
        }

        private void IconClickHandler(object sender)
        {
            Click?.Invoke(this, this.type);
        }
    }
}
