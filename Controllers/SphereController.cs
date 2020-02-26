using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SphereController : ControllerBase
    {
        private readonly CrudChatContext _context;

        public SphereController(CrudChatContext context)
        {
            _context = context;
        }

        // GET: Sphere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sphere>>> GetAllSpheres()
        {
            await _context.Spheres.ToListAsync();
            return new List<Sphere> {
                new Sphere { SphereId = 1, Name = "Test1", DateCreated = DateTime.Now },
                new Sphere { SphereId = 2, Name = "Test2", DateCreated = DateTime.Now },
                new Sphere { SphereId = 3, Name = "Test3", DateCreated = DateTime.Now }
            };
        }

        // GET: Sphere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sphere>> GetSphere(long id)
        {
            await _context.Spheres.FindAsync(id);
            return new Sphere { SphereId = 1, Name = "Single Sphere Test", DateCreated = DateTime.Now };
        }
    }
}
