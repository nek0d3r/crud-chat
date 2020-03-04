using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crud_chat.Models;
using crud_chat.Services;

namespace crud_chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(ServiceResolver serviceAccessor) => _messageService = (MessageService) serviceAccessor(ServiceType.MESSAGE);

        // GET: api/Message/all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IModel>>> GetAllMessages()
        {
            ActionResult<IEnumerable<IModel>> result = await _messageService.GetAll();
            if(result == null)
                return StatusCode(500);
            else
                return result;
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetMessage(long id)
        {
            ActionResult<IModel> result = await _messageService.Get(id);
            if(result == null)
                return StatusCode(500);
            else
                return await _messageService.Get(id);
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(long id, Message message)
        {
            if(id != message.MessageId)
                return BadRequest();
            
            bool ok = await _messageService.Change(message);
            if(!ok)
                return StatusCode(500);
            else
                return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
        }

        // DELETE: api/Message/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(long id)
        {
            switch(await _messageService.Delete(id))
            {
                case ResultType.NotFound:
                    return NotFound();
                case ResultType.Ok:
                    return NoContent();
                default:
                    return StatusCode(500);
            }
        }
    }
}
