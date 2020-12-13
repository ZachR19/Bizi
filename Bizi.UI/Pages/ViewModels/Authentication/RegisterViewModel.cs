using Bizi.UI.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Bizi.UI.Pages
{
    public class RegisterViewModel : BaseViewModel, IBiziViewModel
    {
        public delegate void WindowClosed();

        public event WindowClosed WindowCloseEvent;

        public RegisterViewModel()
        {
            CanRegister = true;
        }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public bool ShowError { get; set; } = true;

        public bool CanRegister { get; set; }

        public async void Register()
        {
            CanRegister = false;

            await Validation();
            if (ErrorMessage != string.Empty)
            {
                CanRegister = true;
                return;
            }

            //Create user
            await CreateUser();
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async Task CreateUser()
        {
            Guid id;

            //ensure that user GUID is not already taken
            do
            {
                id = Guid.NewGuid();
            } while (await Data.SqlDB.IsGUIDConflict(id));

            var hash = Data.PasswordHasher.HashPassword(Password);

            //insert user into database
            await Data.SqlDB.InsertUser(id, hash.Item1, hash.Item2, Email, FirstName, LastName);

            //Close window
            WindowCloseEvent?.Invoke();
        }

        private async Task Validation()
        {
            //Verify email is valid
            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Email is not valid";
                return;
            }

            //Check email wasnt taken
            if (await Data.SqlDB.IsEmailTaken(Email))
            {
                ErrorMessage = "Email is already registered";
                return;
            }

            //Verify password = confirmation
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords are not the same";
                return;
            }

            //Verify password length
            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be longer than 6 characters";
                return;
            }

            ErrorMessage = string.Empty;
        }
    }
}
