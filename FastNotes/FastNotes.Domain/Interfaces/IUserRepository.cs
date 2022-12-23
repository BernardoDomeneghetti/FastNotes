using FastNotes.Common.Models;
using FastNotes.Domain.Data;

namespace FastNotes.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Delete(int Id);

        Task<User?> GetById(int id);

        Task<User?> GetByEmail(string email);

        Task Insert(User user);
    }
}
