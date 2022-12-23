using FastNotes.Common.Models;
using FastNotes.Domain.Data;
using FastNotes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastNotes.Domain.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Delete(int Id)
        {
            User? user = await _dataContext.Users.FindAsync(Id);
            if (user == null) throw new Exception("The user id was not found!");
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dataContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task Insert(User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
        }
    }
}
