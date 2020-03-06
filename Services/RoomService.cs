using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class RoomService : IBLLService
    {
        private CrudChatContext _context { get; set; }

        public RoomService(CrudChatContext context) => _context = context;

        public async Task<IEnumerable<IModel>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<IModel> Get(long id)
        {
            IModel result = await _context.Rooms.FindAsync(id);
            if(result == null)
                throw new Exception("Failed to get room");
            
            return result;
        }

        public async Task<IEnumerable<IModel>> Get(IEnumerable<long> rooms)
        {
            var selectRoomQuery = from room in _context.Set<Room>()
                where rooms.Contains(room.RoomId)
                select room;
            
            return await selectRoomQuery.ToListAsync();
        }

        public async Task<IEnumerable<long>> GetMessages(long id)
        {
            var messageSelectQuery = from roomMessages in _context.Set<RoomMessages>()
                where roomMessages.RoomId == id
                select roomMessages.MessageId;
            
            return await messageSelectQuery.ToListAsync();
        }

        public async Task<bool> Add(IModel room)
        {
            _context.Rooms.Add((Room) room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddMessage(long id, IModel message)
        {
            _context.Messages.Add((Message) message);
            await _context.SaveChangesAsync();
            _context.RoomMessages.Add(new RoomMessages { RoomId = id, MessageId = ((Message) message).MessageId });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Change(IModel room)
        {
            _context.Entry((Room) room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<long>> Delete(IEnumerable<long> rooms)
        {
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