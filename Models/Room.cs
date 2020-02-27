using System;
using System.Collections.Generic;

namespace crud_chat.Models
{
    public class Room
    {
        public long RoomId { get; set; }

        public string Title { get; set; }
        
        public List<RoomMessages> Messages { get; set; } = new List<RoomMessages>();

        public DateTime DateCreated { get; set; }
    }
}