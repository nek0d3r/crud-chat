using System;
using System.Collections.Generic;

namespace crud_chat.Models
{
    public class Sphere
    {
        public long SphereId { get; set; }

        public string Name { get; set; }

        public List<Room> Rooms { get; set; } = new List<Room>();

        public DateTime DateCreated { get; set; }
    }
}