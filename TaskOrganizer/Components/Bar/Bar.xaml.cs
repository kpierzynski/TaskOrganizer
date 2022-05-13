using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Bar.xaml
    /// </summary>
    public partial class Bar : UserControl
    {
        public delegate void SortingHandler(object sender, Sortings sort);
        public event SortingHandler SortingRequest;

        public delegate void NewTaskHandler(object sender);
        public event NewTaskHandler NewTask;

        ObservableCollection<BarItem> items = new ObservableCollection<BarItem>();

        Dictionary<string, Icon.Types> iconsDict = new Dictionary<string, Icon.Types>()
        {
            {"Home", Icon.Types.Home},
            {"Sort by name", Icon.Types.SortName},
            {"Sort by date", Icon.Types.SortDate },
            {"Sort by priority", Icon.Types.SortNum },
            {"Sort by done", Icon.Types.SortDone },
            {"Add task", Icon.Types.PlusBox }
        };

        public enum Sortings
        {
            ByDate, ByName, ByPriority, ByDone
        }


        Dictionary<Icon.Types, Sortings> sortingDict = new Dictionary<Icon.Types, Sortings>()
        {
            { Icon.Types.SortName , Sortings.ByName},
            { Icon.Types.SortDate, Sortings.ByDate },
            { Icon.Types.SortNum, Sortings.ByPriority },
            { Icon.Types.SortDone, Sortings.ByDone },
        };


        public Bar()
        {
            InitializeComponent();
            foreach(var item in iconsDict)
            {
                BarItem barItem = new BarItem(item.Value, item.Key);
                barItem.Click += BarItemClickHandler;
                items.Add(barItem);
                controls.Children.Add(barItem);
            }
        }

        private void BarItemClickHandler(object sender, Icon.Types type)
        {
            if (sortingDict.ContainsKey(type))
            {
                SortingRequest?.Invoke(this, sortingDict[type]);
                return;
            }

            if (type == Icon.Types.PlusBox)
                NewTask?.Invoke(this);
        }

        private void ExpandMenu(object sender)
        {
            foreach (var item in items) item.SwitchVisibility();
        }
    }
}
