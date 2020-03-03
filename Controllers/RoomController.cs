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

        // POST: api/Room/5/messages
        [HttpPost("{id}/messages")]
        public async Task<ActionResult<Message>> PostRoomMessage(long id, Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            _context.RoomMessages.Add(new RoomMessages { RoomId = id, MessageId = message.MessageId });
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(MessageController.GetMessage), new { id = message.MessageId }, message);
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(long id, Room room)
        {
            if(id != room.RoomId)
                return BadRequest();
            
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, room);
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(long id)
        {
            Room result = await _context.Rooms.FindAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(result);

            var sphereRoomQuery = from sphereRoom in _context.Set<SphereRooms>()
                where sphereRoom.RoomId == id
                select sphereRoom;
            var sphereRooms = await sphereRoomQuery.ToListAsync();
            _context.SphereRooms.RemoveRange(sphereRooms);

            var roomMessageQuery = from roomMessage in _context.Set<RoomMessages>()
                where roomMessage.RoomId == id
                select roomMessage;
            var roomMessages = await roomMessageQuery.ToListAsync();
            _context.RoomMessages.RemoveRange(roomMessages);

            var messageQuery = from message in _context.Set<Message>()
                join roomMessage in _context.Set<RoomMessages>()
                on message.MessageId equals roomMessage.MessageId
                where roomMessage.RoomId == id
                select message;
            var messages = await messageQuery.ToListAsync();
            _context.Messages.RemoveRange(messages);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
