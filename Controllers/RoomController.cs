using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly CrudChatContext _context;

        public RoomController(CrudChatContext context)
        {
            _context = context;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            await _context.Rooms.ToListAsync();
            return new List<Room>() {
                new Room { RoomId = 1, Title = "Test1", DateCreated = DateTime.Now },
                new Room { RoomId = 2, Title = "Test2", DateCreated = DateTime.Now },
                new Room { RoomId = 3, Title = "Test3", DateCreated = DateTime.Now }
            };
        }

        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(long id)
        {
            await _context.Rooms.FindAsync(id);
            return new Room { RoomId = 1, Title = "SingleRoomTest", Messages = new List<RoomMessages>() { new RoomMessages { RoomMessagesId = 1, RoomId = 1, MessageId = 1 } }, DateCreated = DateTime.Now };
        }

        // GET: api/Room/5/messages
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetRoomMessages(long id)
        {
            await _context.Rooms.FindAsync(id);
            return new List<Message>() { new Message { MessageId = 1, Content = "Test message", LastModified = DateTime.Now } };
        }
    }
}
