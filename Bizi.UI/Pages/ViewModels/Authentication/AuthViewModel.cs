using Bizi.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizi.UI.Pages
{
    public class AuthViewModel : BaseViewModel, IBiziViewModel
    {
        public LoginViewModel LoginVM { get; set; }
        public RegisterViewModel RegisterVM { get; set; }

        public AuthViewModel(LoginViewModel login, RegisterViewModel register)
        {
            LoginVM = login;
            RegisterVM = register;
        }

    }
}
