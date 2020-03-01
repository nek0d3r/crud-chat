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
    }
}
