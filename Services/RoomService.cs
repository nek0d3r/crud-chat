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

        public async Task<ActionResult<IEnumerable<IModel>>> Get(IEnumerable<long> rooms)
        {
            if(_context == null)
                return null;

            var selectRoomQuery = from room in _context.Set<Room>()
                where rooms.Contains(room.RoomId)
                select room;
            
            return await selectRoomQuery.ToListAsync();
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

        public async Task<List<long>> Delete(IEnumerable<long> rooms)
        {
            if(_context == null)
                return null;
            
            var selectRoomMessageQuery = from roomMessage in _context.Set<RoomMessages>()
                join room in _context.Set<Room>()
                on roomMessage.RoomId equals room.RoomId
                where rooms.Contains(room.RoomId)
                select roomMessage;
            var roomMessagesResult = await selectRoomMessageQuery.ToListAsync();
            _context.RoomMessages.RemoveRange(roomMessagesResult);

            var selectRoomQuery = from room in _context.Set<Room>()
                where rooms.Contains(room.RoomId)
                select room;
            var roomsResult = await selectRoomQuery.ToListAsync();
            _context.Rooms.RemoveRange(roomsResult);

            var selectMessageListQuery = from roomMessage in _context.Set<RoomMessages>()
                join room in _context.Set<Room>()
                on roomMessage.RoomId equals room.RoomId
                where rooms.Contains(room.RoomId)
                select roomMessage.MessageId;
            List<long> result = await selectMessageListQuery.ToListAsync();
            
            await _context.SaveChangesAsync();

            return result;
        }
    }
}