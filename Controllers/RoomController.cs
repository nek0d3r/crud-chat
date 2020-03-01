using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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
            return await _context.Rooms.ToListAsync();
        }

        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(long id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        // GET: api/Room/5/messages
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetRoomMessages(long id)
        {
            var query = from roomMessage in _context.Set<RoomMessages>()
                join message in _context.Set<Message>()
                on roomMessage.MessageId equals message.MessageId
                where roomMessage.RoomId == id
                select message;
            return await query.ToListAsync();
        }
    }
}
