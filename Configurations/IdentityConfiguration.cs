using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurations
{
    public class IdentityConfiguration
    {
        /// <summary>
        /// Name of default role
        /// </summary>
        public string DefaultRole { get; set; }
        /// <summary>
        /// Secret key for JWT auth
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// The number of hours after which jwt token will not be accepting
        /// </summary>
        public int ExpiresHours { get; set; }
    }
}
