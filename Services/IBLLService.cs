using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crud_chat.Models;

namespace crud_chat.Services
{
    public enum ResultType { Ok, NotFound, ContextError };

    public interface IBLLService
    {
        Task<ActionResult<IEnumerable<IModel>>> GetAll();

        Task<ActionResult<IModel>> Get(long id);

        Task<bool> Add(IModel model);

        Task<bool> Change(IModel model);

        Task<List<long>> Delete(IEnumerable<long> id);
    }
}
