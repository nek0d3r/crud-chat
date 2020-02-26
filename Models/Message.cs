using System;

namespace crud_chat.Models
{
    public class Message
    {
        public long MessageId { get; set; }

        public string Content { get; set; }

        public DateTime LastModified { get; set; }
    }
}