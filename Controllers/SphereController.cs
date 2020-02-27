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
            await _context.Spheres.ToListAsync();
            return new List<Sphere> {
                new Sphere { SphereId = 1, Name = "Test1", Rooms = new List<SphereRooms>() { new SphereRooms { SphereRoomsId = 1, SphereId = 1, RoomId = 1 } }, DateCreated = DateTime.Now },
                new Sphere { SphereId = 2, Name = "Test2", Rooms = new List<SphereRooms>() { new SphereRooms { SphereRoomsId = 2, SphereId = 2, RoomId = 2 } }, DateCreated = DateTime.Now },
                new Sphere { SphereId = 3, Name = "Test3", Rooms = new List<SphereRooms>() { new SphereRooms { SphereRoomsId = 3, SphereId = 3, RoomId = 3 } }, DateCreated = DateTime.Now }
            };
        }

        // GET: api/Sphere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sphere>> GetSphere(long id)
        {
            await _context.Spheres.FindAsync(id);
            return new Sphere { SphereId = 1, Name = "Single Sphere Test", Rooms = new List<SphereRooms>() { new SphereRooms { SphereRoomsId = 1, SphereId = 1, RoomId = 1 } }, DateCreated = DateTime.Now };
        }
    }
}
