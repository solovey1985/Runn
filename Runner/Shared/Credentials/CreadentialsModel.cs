using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Shared.Credentials
{
    public class CredentialsModel
    {
        private string login;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string passwordConfiramation;

        public string PasswordConfirmation
        {
            get { return passwordConfiramation; }
            set { passwordConfiramation = value; }
        }

    }
    
}
