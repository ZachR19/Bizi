using System.Diagnostics;
using Bizi.UI.Interfaces;

namespace Bizi.UI.Pages
{
    public class WelcomeViewModel : BaseViewModel
    {

        public WelcomeViewModel() : base()
        {
            DisplayName = "Welcome";
        }

        /// <summary>
        /// Opens the Github page for Bizi
        /// </summary>
        public void ViewGithubButton() => Process.Start(@"https://github.com/ZachR19/Bizi");
    }
}
