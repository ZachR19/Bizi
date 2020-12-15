﻿using System.ComponentModel;
using System.Linq;
using Bizi.Domain.Messages;
using Bizi.UI.Interfaces;
using PropertyChanged;
using Stylet;

namespace Bizi.UI.Pages
{
    public class NavBarViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAgg;

        public NavBarViewModel(WelcomeViewModel welcome, EmployeeViewModel employees, LoginViewModel login,
                               SettingsViewModel settings,
                               IEventAggregator eventAgg)
        {
            _eventAgg = eventAgg;

            Tabs.Add(new BiziTabItem(welcome, "Home"));
            Tabs.Add(new BiziTabItem(employees, "Person"));
            Tabs.Add(new BiziTabItem(employees, "Calendar"));

            PersonalTabs.Add(new BiziTabItem(settings, "Settings"));

        }

        public BindableCollection<BiziTabItem> Tabs { get; set; } = new BindableCollection<BiziTabItem>();

        public BindableCollection<BiziTabItem> PersonalTabs { get; set; } = new BindableCollection<BiziTabItem>();


        public void RadChecked(BiziTabItem selectedTab)
        {
            foreach (BiziTabItem tab in Tabs)
            {
                tab.IsShown = false;
            }

            foreach (BiziTabItem tab in PersonalTabs)
            {
                tab.IsShown = false;
            }

            selectedTab.IsShown = true;

            _eventAgg.PublishOnUIThread(new ChangeViewMessage(this, selectedTab.ViewModel));
        }
    }

    public class BiziTabItem : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
