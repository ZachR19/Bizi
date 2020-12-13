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

            bool? result = winManager.ShowDialog(auth);
        }

        protected override async void OnLaunch()
        {
            var email = "raudebaughzach@gmail.com";
            var pass = "123456789asdfghjkl";

            var hashAndSalt = await Data.SqlDB.GetDBHashAndSalt(email);

            var success = Data.PasswordHasher.VerifyPassword(pass, hashAndSalt.Item2, hashAndSalt.Item1);

            base.OnLaunch();
        }
    }
}
