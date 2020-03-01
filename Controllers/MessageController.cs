using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using crud_chat.Models;

namespace crud_chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly CrudChatContext _context;

        public MessageController(CrudChatContext context)
        {
            _context = context;
        }

        // GET: api/Message/all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            await _context.Messages.ToListAsync();
            return new List<Message>() {
                new Message { MessageId = 1, Content = "Test1", LastModified = DateTime.Now },
                new Message { MessageId = 2, Content = "Test2", LastModified = DateTime.Now },
                new Message { MessageId = 3, Content = "Test3", LastModified = DateTime.Now }
            };
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(long id)
        {
            await _context.Messages.FindAsync(id);
            return new Message { MessageId = id, Content = "SingleRoomTest", LastModified = DateTime.Now };
        }
    }
}
