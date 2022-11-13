using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChat.Core.Views
{
    public class UserAuthorizationResult
    {
        public string Token { get; }
        public DateTime Expiration { get; }

        public UserAuthorizationResult(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
