using System;
using System.Collections.Generic;

namespace crud_chat.Models
{
    public class Room
    {
        public long RoomId { get; set; }

        public string Title { get; set; }

        public DateTime DateCreated { get; set; }
    }
}