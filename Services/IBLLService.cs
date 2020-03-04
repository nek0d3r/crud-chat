using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crud_chat.Models;

namespace crud_chat.Services
{
    public enum ResultType { Ok, NotFound, ContextError };

    public interface IBLLService
    {
        Task<ActionResult<IModel>> Get(long id);
    }
}
