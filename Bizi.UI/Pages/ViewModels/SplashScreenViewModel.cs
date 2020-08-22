using Bizi.Domain;
using Stylet;
using System.Windows;

namespace Bizi.UI.Pages
{
    public class SplashScreenViewModel : IHandle<LoadedMessage>, ISplashScreenViewModel
    {
        public SplashScreenViewModel() 
        {
                
        }

        public void Handle(LoadedMessage message)
        {
            MessageBox.Show(message.Content.ToString());
        }
    }
}
