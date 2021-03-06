using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crud_chat.Models;
using crud_chat.Services;

namespace crud_chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly MessageService _messageService;

        public RoomController(ServiceResolver serviceAccessor)
        {
            _roomService = (RoomService) serviceAccessor(ServiceType.ROOM);
            _messageService = (MessageService) serviceAccessor(ServiceType.MESSAGE);
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IModel>>> GetAllRooms()
        {
            try
            {
                IEnumerable<IModel> result = await _roomService.GetAll();
                if(result == null)
                    return StatusCode(500);
                else
                    return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetRoom(long id)
        {
            try
            {
                IModel result = await _roomService.Get(id);
                if(result == null)
                    return StatusCode(500);
                else
                    return Ok(await _roomService.Get(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Room/5/messages
        [HttpGet("{id}/messages")]
        public async Task<ActionResult<IEnumerable<IModel>>> GetRoomMessages(long id)
        {
            try
            {
                IEnumerable<long> roomMessages = await _roomService.GetMessages(id);
                if(roomMessages == null)
                    return StatusCode(500);
                else
                    return Ok(await _messageService.Get(roomMessages));
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Room/5/messages
        [HttpPost("{id}/messages")]
        public async Task<ActionResult<Message>> PostRoomMessage(long id, Message message)
        {
            try
            {
                bool ok = await _roomService.AddMessage(id, message);
                if(!ok)
                    return StatusCode(500);
                else
                    return CreatedAtAction(nameof(MessageController.GetMessage), new { id = message.MessageId }, message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(long id, Room room)
        {
            try
            {
                if(id != room.RoomId)
                    return BadRequest();
                
                bool ok = await _roomService.Change(room);
                if(!ok)
                    return StatusCode(500);
                else
                    return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, room);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(long id)
        {
            try
            {
                List<long> messages = await _roomService.Delete(new List<long>() { id });
                if(messages == null)
                    return StatusCode(500);
                if(messages.Count == 0)
                    return NoContent();
                
                List<long> result = await _messageService.Delete(messages);
                if(result == null)
                    return StatusCode(500);
                
                return NoContent();
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
