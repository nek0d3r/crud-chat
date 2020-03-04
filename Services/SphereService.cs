using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class SphereService : IBLLService
    {
        private CrudChatContext _context { get; set; }

        public SphereService(CrudChatContext context) => _context = context;

        public async Task<ActionResult<IEnumerable<IModel>>> GetAll()
        {
            if(_context == null)
                return null;
            
            return await _context.Spheres.ToListAsync();
        }

        public async Task<ActionResult<IModel>> Get(long id)
        {
            if(_context == null)
                return null;
            
            return await _context.Spheres.FindAsync(id);
        }

        public async Task<IEnumerable<long>> GetRooms(long id)
        {
            if(_context == null)
                return null;
            
            var roomSelectQuery = from sphereRooms in _context.Set<SphereRooms>()
                where sphereRooms.SphereId == id
                select sphereRooms.RoomId;
            
            return await roomSelectQuery.ToListAsync();
        }

        public async Task<bool> Add(Sphere sphere)
        {
            if(_context == null)
                return false;
            
            _context.Spheres.Add(sphere);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddRoom(long id, IModel room)
        {
            if(_context == null)
                return false;
            
            _context.SphereRooms.Add(new SphereRooms { SphereId = id, RoomId = ((Room) room).RoomId });
            _context.Rooms.Add((Room) room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Change(IModel sphere)
        {
            if(_context == null)
                return false;
            
            _context.Entry((Sphere) sphere).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<long>> Delete(long id)
        {
            if(_context == null)
                return null;
            
            Sphere sphere = await _context.Spheres.FindAsync(id);

            if(sphere == null)
            {
                return new List<long>();
            }

            _context.Spheres.Remove(sphere);

            var sphereRoomQuery = from sphereRoom in _context.Set<SphereRooms>()
                where sphereRoom.SphereId == id
                select sphereRoom;
            var sphereRooms = await sphereRoomQuery.ToListAsync();
            _context.SphereRooms.RemoveRange(sphereRooms);

            var roomQuery = from sphereRoom in _context.Set<SphereRooms>()
                join room in _context.Set<Room>()
                on sphereRoom.RoomId equals room.RoomId
                where sphereRoom.SphereId == id
                select room;
            var rooms = await roomQuery.ToListAsync();
            _context.Rooms.RemoveRange(rooms);

            var roomListQuery = from sphereRoom in _context.Set<SphereRooms>()
                join room in _context.Set<Room>()
                on sphereRoom.RoomId equals room.RoomId
                where sphereRoom.SphereId == id
                select room.RoomId;
            List<long> result = await roomListQuery.ToListAsync();

            await _context.SaveChangesAsync();

            return result;
        }
    }
}