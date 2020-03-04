using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class RoomService : IBLLService
    {
        private CrudChatContext _context { get; set; }

        public RoomService(CrudChatContext context) => _context = context;

        public async Task<ActionResult<IEnumerable<IModel>>> GetAll()
        {
            if(_context == null)
                return null;
            
            return await _context.Rooms.ToListAsync();
        }

        public async Task<ActionResult<IModel>> Get(long id)
        {
            if(_context == null)
                return null;
            
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<IEnumerable<long>> GetMessages(long id)
        {
            if(_context == null)
                return null;
            
            var messageSelectQuery = from roomMessages in _context.Set<RoomMessages>()
                where roomMessages.RoomId == id
                select roomMessages.MessageId;
            
            return await messageSelectQuery.ToListAsync();
        }

        public async Task<bool> Add(IModel room)
        {
            if(_context == null)
                return false;
            
            _context.Rooms.Add((Room) room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddMessage(long id, IModel message)
        {
            if(_context == null)
                return false;
            
            _context.RoomMessages.Add(new RoomMessages { RoomId = id, MessageId = ((Message) message).MessageId });
            _context.Messages.Add((Message) message);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Change(IModel room)
        {
            if(_context == null)
                return false;
            
            _context.Entry((Room) room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ResultType> Delete(long id)
        {
            if(_context == null)
                return ResultType.ContextError;
            
            Room room = await _context.Rooms.FindAsync(id);

            if(room == null)
            {
                return ResultType.NotFound;
            }

            _context.Rooms.Remove(room);

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

            return ResultType.Ok;
        }
    }
}