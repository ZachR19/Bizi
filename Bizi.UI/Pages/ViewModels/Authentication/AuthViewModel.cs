using Bizi.UI.Interfaces;
using Stylet;
using System;

namespace Bizi.UI.Pages
{
    public class AuthViewModel : Conductor<Screen>.Collection.OneActive, IBiziViewModel
    {
        public LoginViewModel LoginVM { get; set; }
        public RegisterViewModel RegisterVM { get; set; }
        public IWindowManager WinMan { get; set; }

        public AuthViewModel(LoginViewModel login, RegisterViewModel register)
        {
            LoginVM = login;
            RegisterVM = register;

            LoginVM.WindowCloseEvent += CloseWindow;
            RegisterVM.WindowCloseEvent += CloseWindow;
        }

        public void CloseWindow()
        {
            this.RequestClose();
        }

        public void ShutdownApp()
        {
            Environment.Exit(0);
        }

    }
}
