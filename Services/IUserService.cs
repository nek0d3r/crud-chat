using System.Collections.Generic;
using System.Threading.Tasks;
using crud_chat.Models;

namespace crud_chat.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        
        Task<IEnumerable<User>> GetAll();
        
        Task<User> Get(long id);

        Task<User> Add(User user, string password);

        Task<ResultType> Change(User user, string password = null);

        Task<ResultType> Delete(long id);
    }
}