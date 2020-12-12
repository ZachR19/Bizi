using Bizi.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizi.UI.Pages
{
    public class LoginViewModel : BaseViewModel, IBiziViewModel, ILoginViewModel
    {
        public LoginViewModel()
        {
            DisplayName = "Login";
        }



    }
}
