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
    public class SphereController : ControllerBase
    {
        private readonly SphereService _sphereService;
        private readonly RoomService _roomService;
        private readonly MessageService _messageService;

        public SphereController(ServiceResolver serviceResolver)
        {
            _sphereService = (SphereService) serviceResolver(ServiceType.SPHERE);
            _roomService = (RoomService) serviceResolver(ServiceType.ROOM);
            _messageService = (MessageService) serviceResolver(ServiceType.MESSAGE);
        }

        // GET: api/Sphere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IModel>>> GetAllSpheres()
        {
            try
            {
                IEnumerable<IModel> result = await _sphereService.GetAll();
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

        // GET: api/Sphere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IModel>> GetSphere(long id)
        {
            try
            {
                IModel result = await _sphereService.Get(id);
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

        // GET: api/Sphere/5/rooms
        [HttpGet("{id}/rooms")]
        public async Task<ActionResult<IEnumerable<IModel>>> GetSphereRooms(long id)
        {
            try
            {
                IEnumerable<long> sphereRooms = await _sphereService.GetRooms(id);
                if(sphereRooms == null)
                    return StatusCode(500);
                else
                    return Ok(await _roomService.Get(sphereRooms));
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Sphere
        [HttpPost]
        public async Task<ActionResult<IModel>> PostSphere(Sphere sphere)
        {
            try
            {
                bool ok = await _sphereService.Add(sphere);
                if(!ok)
                    return StatusCode(500);
                else
                    return CreatedAtAction(nameof(GetSphere), new { id = ((Sphere)sphere).SphereId }, sphere);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST: api/Sphere/5/rooms
        [HttpPost("{id}/rooms")]
        public async Task<ActionResult<IModel>> PostSphereRoom(long id, Room room)
        {
            try
            {
                bool ok = await _sphereService.AddRoom(id, room);
                
                if(!ok)
                    return StatusCode(500);
                else
                    return CreatedAtAction(nameof(MessageController.GetMessage), new { id = room.RoomId }, room);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/Sphere/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSphere(long id, Sphere sphere)
        {
            if(id != sphere.SphereId)
                return BadRequest();
            
            try
            {
                bool ok = await _sphereService.Change(sphere);
                if(!ok)
                    return StatusCode(500);
                else
                    return CreatedAtAction(nameof(GetSphere), new { id = sphere.SphereId }, sphere);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE: api/Sphere/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSphere(long id)
        {
            try
            {
                List<long> rooms = await _sphereService.Delete(new List<long>() { id });
                if(rooms == null)
                    return StatusCode(500);
                if(rooms.Count == 0)
                    return NoContent();
                
                List<long> messages = await _roomService.Delete(rooms);
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
