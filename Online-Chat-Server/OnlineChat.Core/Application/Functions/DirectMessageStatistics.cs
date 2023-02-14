using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Functions
{
    public class DirectMessageStatistics
    {
        public int TotalMessages { get; set; }
        public int TotalSent { get; set; }
        public int TotalReceived { get; set; }
    }
}
