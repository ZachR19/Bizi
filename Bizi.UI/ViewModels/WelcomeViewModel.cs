using System.Diagnostics;

namespace Bizi.UI
{
    public class WelcomeViewModel : BaseViewModel
    {
        public WelcomeViewModel() : base()
        {

        }

        /// <summary>
        /// Opens the Github page for Bizi
        /// </summary>
        public void ViewGithubButton() => Process.Start(@"https://github.com/ZachR19/Bizi");
    }
}
