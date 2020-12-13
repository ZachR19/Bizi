using Bizi.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizi.UI.Pages
{
    public class LoginViewModel : BaseViewModel, IBiziViewModel
    {
        public LoginViewModel()
        {
            DisplayName = "Login";
        }

        public string Password { get; set; }
        public string Email { get; set; }


        public async Task Login()
        {
            //Get db hash and salt corresponding to email
            var result = await Data.SqlDB.GetDBHashAndSalt(Email);

            var success = Data.PasswordHasher.VerifyPassword(Password, result.Item2, result.Item1);

            if (success)
            {
                //Close login form
            }
            else
            {
                //Show invalid credential notice

            }

        }
    }
}
