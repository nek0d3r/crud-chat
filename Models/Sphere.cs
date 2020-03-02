using System;
using System.Collections.Generic;

namespace crud_chat.Models
{
    public class Sphere
    {
        public long SphereId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}