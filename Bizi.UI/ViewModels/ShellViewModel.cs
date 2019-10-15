
using Bizi.Domain;
using Caliburn.Micro;

namespace Bizi.UI
{
    public class ShellViewModel : Conductor<Screen>.Collection.AllActive
    {
        public ShellViewModel()
        {
            ActivateItem(new WelcomeViewModel());
        }

    }
}
