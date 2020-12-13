using Bizi.UI.Interfaces;
using System.Threading.Tasks;

namespace Bizi.UI.Pages
{
    public class LoginViewModel : BaseViewModel, IBiziViewModel
    {
        public delegate void WindowClosed();

        public event WindowClosed WindowCloseEvent;

        public LoginViewModel()
        {
            DisplayName = "Login";
            CanLogin = true;
        }

        public string Password { get; set; }
        public string Email { get; set; }
        public bool ShowError { get; set; }

        public bool CanLogin { get; set; }

        public async Task Login()
        {
            CanLogin = false;

            //Get db hash and salt corresponding to email
            var result = await Data.SqlDB.GetDBHashAndSalt(Email);

            if (result.Item1 == string.Empty || result.Item2 == string.Empty)
            {
                ShowError = true;
                CanLogin = true;
                return;
            }

            bool success = Data.PasswordHasher.VerifyPassword(Password, result.Item2, result.Item1);

            if (success)
            {
                //Close login form
                ShowError = false;

                WindowCloseEvent?.Invoke();
            }
            else
            {
                //Show invalid credential notice
                ShowError = true;

                CanLogin = true;
            }

        }
    }
}
