using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class MessageService : IBLLService
    {
        private CrudChatContext _context { get; set; }

        public MessageService(CrudChatContext context) => _context = context;

        public async Task<ActionResult<IEnumerable<IModel>>> GetAll()
        {
            if(_context == null)
                return null;
            
            return await _context.Messages.ToListAsync();
        }

        public async Task<ActionResult<IModel>> Get(long id)
        {
            if(_context == null)
                return null;
            
            return await _context.Messages.FindAsync(id);
        }

        public async Task<ActionResult<IEnumerable<IModel>>> Get(IEnumerable<long> messages)
        {
            if(_context == null)
                return null;

            var selectMessageQuery = from message in _context.Set<Message>()
                where messages.Contains(message.MessageId)
                select message;
            
            return await selectMessageQuery.ToListAsync();
        }

        public async Task<bool> Add(IModel message)
        {
            if(_context == null)
                return false;
            
            _context.Messages.Add((Message) message);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Change(IModel message)
        {
            if(_context == null)
                return false;
            
            _context.Entry((Message) message).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ResultType> Delete(long id)
        {
            if(_context == null)
                return ResultType.ContextError;
            
            Message message = await _context.Messages.FindAsync(id);

            if(message == null)
            {
                return ResultType.NotFound;
            }

            _context.Messages.Remove(message);

            var roomMessageQuery = from roomMessage in _context.Set<RoomMessages>()
                where roomMessage.MessageId == id
                select roomMessage;
            var roomMessages = await roomMessageQuery.ToListAsync();
            _context.RoomMessages.RemoveRange(roomMessages);

            await _context.SaveChangesAsync();

            return ResultType.Ok;
        }

        public async Task<ResultType> Delete(IEnumerable<long> messages)
        {
            if(_context == null)
                return ResultType.ContextError;

            var selectMessageQuery = from message in _context.Set<Message>()
                where messages.Contains(message.MessageId)
                select message;
            
            List<Message> result = await selectMessageQuery.ToListAsync();
            _context.Messages.RemoveRange(result);
            await _context.SaveChangesAsync();

            return ResultType.Ok;
        }
    }
}