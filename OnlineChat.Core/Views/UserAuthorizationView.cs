using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.Views
{
    public class UserAuthorizationView
    {
        public string Token { get; }
        public DateTime Expiration { get; }

        public UserAuthorizationView(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
