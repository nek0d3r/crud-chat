using System;
using System.Collections.Generic;

namespace crud_chat.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        public string Title { get; set; }
        
        public List<Message> Messages { get; set; } = new List<Message>();

        public DateTime DateCreated { get; set; }
    }
}