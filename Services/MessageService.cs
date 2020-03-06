using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class MessageService : IBLLService
    {
        private CrudChatContext _context { get; set; }

        public MessageService(CrudChatContext context) => _context = context;

        public async Task<IEnumerable<IModel>> GetAll()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<IModel> Get(long id)
        {
            IModel result = await _context.Messages.FindAsync(id);
            if(result == null)
                throw new Exception("Failed to get message");

            return result;
        }

        public async Task<IEnumerable<IModel>> Get(IEnumerable<long> messages)
        {
            var selectMessageQuery = from message in _context.Set<Message>()
                where messages.Contains(message.MessageId)
                select message;
            
            return await selectMessageQuery.ToListAsync();
        }

        public async Task<bool> Add(IModel message)
        {
            _context.Messages.Add((Message) message);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Change(IModel message)
        {
            _context.Entry((Message) message).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(long id)
        {
            Message message = await _context.Messages.FindAsync(id);

            if(message == null)
                return true;

            _context.Messages.Remove(message);

            var roomMessageQuery = from roomMessage in _context.Set<RoomMessages>()
                where roomMessage.MessageId == id
                select roomMessage;
            var roomMessages = await roomMessageQuery.ToListAsync();
            _context.RoomMessages.RemoveRange(roomMessages);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<long>> Delete(IEnumerable<long> messages)
        {
            var selectMessageQuery = from message in _context.Set<Message>()
                where messages.Contains(message.MessageId)
                select message;
            
            List<Message> result = await selectMessageQuery.ToListAsync();
            _context.Messages.RemoveRange(result);
            await _context.SaveChangesAsync();

            return new List<long>();
        }
    }
}