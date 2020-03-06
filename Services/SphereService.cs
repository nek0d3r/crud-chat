using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class SphereService : IBLLService
    {
        private CrudChatContext _context { get; set; }

        public SphereService(CrudChatContext context) => _context = context;

        public async Task<IEnumerable<IModel>> GetAll()
        {
            return await _context.Spheres.ToListAsync();
        }

        public async Task<IModel> Get(long id)
        {
            IModel result = await _context.Spheres.FindAsync(id);
            if(result == null)
                throw new Exception("Failed to get sphere");
            
            return result;
        }

        public async Task<IEnumerable<long>> GetRooms(long id)
        {
            var roomSelectQuery = from sphereRooms in _context.Set<SphereRooms>()
                where sphereRooms.SphereId == id
                select sphereRooms.RoomId;
            
            return await roomSelectQuery.ToListAsync();
        }

        public async Task<bool> Add(IModel sphere)
        {
            _context.Spheres.Add((Sphere) sphere);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddRoom(long id, IModel room)
        {
            _context.Rooms.Add((Room) room);
            await _context.SaveChangesAsync();
            _context.SphereRooms.Add(new SphereRooms { SphereId = id, RoomId = ((Room) room).RoomId });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Change(IModel sphere)
        {
            _context.Entry((Sphere) sphere).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<long>> Delete(IEnumerable<long> ids)
        {
            var sphereQuery = from sphere in _context.Set<Sphere>()
                where ids.Contains(sphere.SphereId)
                select sphere;
            var spheres = await sphereQuery.ToListAsync();
            _context.Spheres.RemoveRange(spheres);

            var sphereRoomQuery = from sphereRoom in _context.Set<SphereRooms>()
                where ids.Contains(sphereRoom.SphereId)
                select sphereRoom;
            var sphereRooms = await sphereRoomQuery.ToListAsync();
            _context.SphereRooms.RemoveRange(sphereRooms);

            var roomListQuery = from sphereRoom in _context.Set<SphereRooms>()
                join room in _context.Set<Room>()
                on sphereRoom.RoomId equals room.RoomId
                where ids.Contains(sphereRoom.SphereId)
                select room.RoomId;
            List<long> result = await roomListQuery.ToListAsync();

            await _context.SaveChangesAsync();

            return result;
        }
    }
}