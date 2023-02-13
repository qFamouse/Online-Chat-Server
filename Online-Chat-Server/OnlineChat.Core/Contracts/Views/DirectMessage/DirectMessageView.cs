﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Views.DirectMessage
{
    public class DirectMessageView
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
