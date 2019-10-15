
using Bizi.Domain;
using Caliburn.Micro;

namespace Bizi.UI
{
    public class ShellViewModel : Conductor<Screen>.Collection.AllActive
    {
        private IWindowManager _winManager;

        public ShellViewModel(IWindowManager winman, 
                              WelcomeViewModel welcome)
        {
            _winManager = winman;

            ActivateItem(welcome);
        }

    }
}
