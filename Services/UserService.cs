using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using crud_chat.Models;

namespace crud_chat.Services
{
    public class UserService : IUserService
    {
        private CrudChatContext _context { get; set; }

        public UserService(CrudChatContext context) => _context = context;

        public async Task<User> Authenticate(string username, string password)
        {
            if(_context == null)
                return null;
            
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            
            var userQuery = from user in _context.Set<User>()
                where user.Username == username
                select user;
            var result = await userQuery.ToListAsync();

            if(result.Count != 1)
                return null;
            
            if(!VerifyPasswordHash(password, result.First().PasswordHash, result.First().PasswordSalt))
                return null;

            return result.First();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            if(_context == null)
                return null;
            
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Get(long id)
        {
            if(_context == null)
                return null;
            
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Add(User user, string password)
        {
            if(_context == null)
                return null;
            
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if(await _context.Users.AnyAsync(u => u.Username == user.Username))
                throw new ArgumentException("Username \"" + user.Username + "\" is already taken");
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ResultType> Change(User user, string password = null)
        {
            if(_context == null)
                return ResultType.ContextError;
            
            User result = _context.Users.Find(user.UserId);

            if(result == null)
                throw new ArgumentException("User not found");
            
            if(!string.IsNullOrWhiteSpace(user.Username) && user.Username != result.Username)
            {
                if(await _context.Users.AnyAsync(u => u.Username == user.Username))
                    throw new ArgumentException("Username \"" + user.Username + "\" is already taken");
                
                result.Username = user.Username;
            }

            if(!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                result.PasswordHash = passwordHash;
                result.PasswordSalt = passwordSalt;
            }

            _context.Entry(result).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ResultType.Ok;
        }

        public async Task<ResultType> Delete(long id)
        {
            if(_context == null)
                return ResultType.ContextError;

            User user = await _context.Users.FindAsync(id);

            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return ResultType.Ok;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if(password == null)
                throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if(password == null)
                throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace", "password");
            if(storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if(storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                    if(computedHash[i] != storedHash[i])
                        return false;
                return true;
            }
        }
    }
}