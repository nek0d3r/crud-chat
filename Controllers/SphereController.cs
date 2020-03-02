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
    public class SphereController : ControllerBase
    {
        private readonly CrudChatContext _context;

        public SphereController(CrudChatContext context)
        {
            _context = context;
        }

        // GET: api/Sphere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sphere>>> GetAllSpheres()
        {
            return await _context.Spheres.ToListAsync();
        }

        // GET: api/Sphere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sphere>> GetSphere(long id)
        {
            return await _context.Spheres.FindAsync(id);
        }

        // GET: api/Sphere/5/rooms
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<List<Room>>> GetSphereRooms(long id)
        {
            var query = from sphereRoom in _context.Set<SphereRooms>()
                join room in _context.Set<Room>()
                on sphereRoom.RoomId equals room.RoomId
                where sphereRoom.SphereId == id
                select room;
            return await query.ToListAsync();
        }

        // POST: api/Sphere/5/rooms
        [HttpPost("{id}/rooms")]
        public async Task<ActionResult<Room>> PostSphereRoom(long id, Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            _context.SphereRooms.Add(new SphereRooms { SphereId = id, RoomId = room.RoomId });
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RoomController.GetRoom), new { id = room.RoomId }, room);
        }

        // PUT: api/Sphere/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSphere(long id, Sphere sphere)
        {
            if(id != sphere.SphereId)
                return BadRequest();
            
            _context.Entry(sphere).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Sphere/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSphere(long id, Sphere sphere)
        {
            Sphere result = await _context.Spheres.FindAsync(id);
            
            if(result == null)
            {
                return NotFound();
            }

            _context.Spheres.Remove(result);

            var sphereRoomQuery = from sphereRoom in _context.Set<SphereRooms>()
                where sphereRoom.SphereId == id
                select sphereRoom;
            var sphereRooms = await sphereRoomQuery.ToListAsync();
            _context.SphereRooms.RemoveRange(sphereRooms);

            var roomQuery = from sphereRoom in _context.Set<SphereRooms>()
                join room in _context.Set<Room>()
                on sphereRoom.RoomId equals room.RoomId
                where sphereRoom.SphereId == id
                select room;
            var rooms = await roomQuery.ToListAsync();
            _context.Rooms.RemoveRange(rooms);

            var roomMessageQuery = from roomMessage in _context.Set<RoomMessages>()
                join sphereRoom in _context.Set<SphereRooms>()
                on roomMessage.RoomId equals sphereRoom.RoomId
                where sphereRoom.SphereId == id
                select roomMessage;
            var roomMessages = await roomMessageQuery.ToListAsync();
            _context.RoomMessages.RemoveRange(roomMessages);

            var messageQuery = from message in _context.Set<Message>()
                join roomMessage in _context.Set<RoomMessages>()
                on message.MessageId equals roomMessage.MessageId
                join sphereRoom in _context.Set<SphereRooms>()
                on roomMessage.RoomId equals sphereRoom.SphereId
                where sphereRoom.SphereId == id
                select message;
            var messages = await messageQuery.ToListAsync();
            _context.Messages.RemoveRange(messages);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
