using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;

namespace FastNotes.Domain.Interfaces
{
    public interface IUserWorker
    {
        Task<GenericResponse<User>> Register(User user);
        Task<GenericResponse<User>> Unregister(User user);
        Task<User?> GetByEmail(string email);
    }
}
