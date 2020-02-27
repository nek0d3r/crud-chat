using System;
using System.Collections.Generic;

namespace crud_chat.Models
{
    public class Sphere
    {
        public long SphereId { get; set; }

        public string Name { get; set; }

        public List<SphereRooms> Rooms { get; set; } = new List<SphereRooms>();

        public DateTime DateCreated { get; set; }
    }
}