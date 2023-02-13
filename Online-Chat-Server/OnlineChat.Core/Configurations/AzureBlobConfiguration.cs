using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurations
{
    public class AzureBlobConfiguration
    {
        /// <summary>
        /// Name of file container with attachments for direct messages
        /// </summary>
        public string DirectMessagesContainer { get; set; }
    }
}
