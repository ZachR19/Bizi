using System;
using Bizi.Domain.Messages;
using Stylet;

namespace Bizi.UI.Pages
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<ChangeViewMessage>
    {
        private IWindowManager _winManager;
        private IEventAggregator _eventAgg;

        public ShellViewModel(IWindowManager winman, IEventAggregator eventAgg, NavBarViewModel navbar)
        {
            _winManager = winman;
            _eventAgg = eventAgg;
            _eventAgg.Subscribe(this);

            NavBar = navbar;
        }

        public NavBarViewModel NavBar { get; set; }

        #region Events
        public void Handle(ChangeViewMessage message)
        {
            if (message.Content == null) throw new ArgumentNullException(nameof(message), $"{nameof(message)} cannot be null");

            try
            {
                Screen screen = (Screen) message.Content;
                ActivateItem(screen);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}
