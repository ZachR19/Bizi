using System.Linq;
using Bizi.Domain.Messages;
using Bizi.UI.Interfaces;
using PropertyChanged;
using Stylet;

namespace Bizi.UI.Pages
{
    public class NavBarViewModel : BaseViewModel, IBiziViewModel
    {
        private readonly IEventAggregator _eventAgg;

        public NavBarViewModel(WelcomeViewModel welcome, EmployeeViewModel employees, LoginViewModel login,
                               IEventAggregator eventAgg)
        {
            _eventAgg = eventAgg;

            Tabs.Add(new BiziTabItem(welcome, "Welcome"));
            Tabs.Add(new BiziTabItem(employees, "Employees"));
            Tabs.Add(new BiziTabItem(login, "Login"));

        }

        public BindableCollection<BiziTabItem> Tabs { get; set; } = new BindableCollection<BiziTabItem>();

        public void RadChecked(BiziTabItem selectedTab)
        {
            foreach (BiziTabItem tab in Tabs)
            {
                tab.IsShown = false;
            }

            selectedTab.IsShown = true;

            _eventAgg.PublishOnUIThread(new ChangeViewMessage(this, selectedTab.ViewModel));
        }
    }

    public class BiziTabItem
    {
        public BiziTabItem(IBiziViewModel model, string header)
        {
            ViewModel = model;
            Header = header;
        }

        public IBiziViewModel ViewModel { get; set; }

        public string Header { get; set; }

        public bool IsChecked
        {
            get {return IsShown;}
        }

        [AlsoNotifyFor(nameof(IsChecked))]
        public bool IsShown { get; set; }

        public bool IsEnabled { get; set; } = true;

    }
}
