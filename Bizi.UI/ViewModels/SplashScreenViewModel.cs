using Bizi.Domain;
using Caliburn.Micro;
using System.Windows;

namespace Bizi.UI
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
