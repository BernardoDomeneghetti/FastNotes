using FastNotes.Common.Exceptions;
using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using FastNotes.Domain.Interfaces;

namespace FastNotes.Domain.Workers
{
    internal class UserWorker : IUserWorker
    {
        private readonly IUserRepository _userRepository;

        public UserWorker(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByEmail(string email)
        {
            return  await _userRepository.GetByEmail(email);
        }

        public async Task<GenericResponse<User>> Register(User user)
        {
            await _userRepository.Insert(user);
            return new GenericResponse<User>()
            {
                Success = true,
                Message = "User created sucessfully!",
            };
                
        }

        public async Task<GenericResponse<User>> Unregister(User user)
        {
            var u = _userRepository.GetById(user.Id);
            if (u == null)
                throw new UserNotFound();
            
            await _userRepository.Delete(user.Id);

            return new GenericResponse<User>()
            {
                Message = "User deleted successfully!",
            };   
        }
    }
}
