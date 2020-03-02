using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(long id, Message message)
        {
            if(id != message.MessageId)
                return BadRequest();
            
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Message/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(long id)
        {
            Message result = await _context.Messages.FindAsync(id);

            if(result == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(result);

            var roomMessageQuery = from roomMessage in _context.Set<RoomMessages>()
                where roomMessage.MessageId == id
                select roomMessage;
            var roomMessages = await roomMessageQuery.ToListAsync();
            _context.RoomMessages.RemoveRange(roomMessages);

            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
