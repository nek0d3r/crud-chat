using Microsoft.EntityFrameworkCore;

namespace crud_chat.Models
{
    public class CrudChatContext : DbContext
    {
        public DbSet<Sphere> Spheres { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Message> Messages { get; set; }

        public CrudChatContext(DbContextOptions<CrudChatContext> options)
            : base(options)
        {
        }
    }
}