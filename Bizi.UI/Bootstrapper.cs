using Bizi.UI.Pages;
using Stylet;
using StyletIoC;

namespace Bizi
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>, IBootstrapper
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<IWindowManager>().To<WindowManager>();
            builder.Bind<LoginViewModel>().ToSelf();
            builder.Bind<RegisterViewModel>().ToSelf();
            builder.Bind<AuthViewModel>().ToSelf();
        }

        protected override void Configure()
        {
            var winManager = Container.Get<IWindowManager>();
            var auth = Container.Get<AuthViewModel>();

            //bool? result = winManager.ShowDialog(auth);
        }
    }
}
