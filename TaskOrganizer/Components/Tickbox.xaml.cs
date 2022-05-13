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

namespace TaskOrganizer.Components
{
    /// <summary>
    /// Interaction logic for Tickbox.xaml
    /// </summary>
    public partial class Tickbox : UserControl, INotifyPropertyChanged
    {
        public delegate void ClickHandler(object sender, bool isTicked);
        public event ClickHandler Click;

        private static readonly DependencyProperty HasFishProperty =
        DependencyProperty.Register(
            name: "IsTicked",
            propertyType: typeof( bool ),
            ownerType: typeof( Tickbox ),
            typeMetadata: new FrameworkPropertyMetadata( defaultValue: false ) );

        public bool IsTicked
        {
            get
            {
                return (bool)GetValue( HasFishProperty );
            }
            set
            {
                SetValue( HasFishProperty, value );
                OnPropertyChanged("IsTicked");
            }
        }
        public Tickbox()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void clickHandler(object sender, MouseButtonEventArgs e)
        {
            IsTicked = !IsTicked;
            rect.Opacity = 1;

            Click?.Invoke(this, IsTicked);
        }

        private void opacityClickHandler(object sender, MouseButtonEventArgs e)
        {
            rect.Opacity = 0.75;
        }
    }
}
