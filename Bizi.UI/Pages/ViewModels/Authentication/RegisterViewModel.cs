using Bizi.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizi.UI.Pages
{
    public class RegisterViewModel : BaseViewModel, IBiziViewModel
    {
        public RegisterViewModel()
        {

        }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public bool ShowError { get; set; }

        public void Register()
        {


            ErrorMessage = "Email is already in use.";
            ShowError = true;
        }
    }
}
