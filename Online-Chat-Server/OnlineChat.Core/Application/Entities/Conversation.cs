﻿using Application.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities
{
    public class Conversation : IEntity
    {
        public int Id { get; set; }
        //TODO: Add logotype
        public string Title { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
