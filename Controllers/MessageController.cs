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
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(long id)
        {
            return await _context.Messages.FindAsync(id);
        }
    }
}
