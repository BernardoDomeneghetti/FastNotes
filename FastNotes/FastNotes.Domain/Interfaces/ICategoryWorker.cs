using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;

namespace FastNotes.Domain.Interfaces
{
    public interface ICategoryWorker
    {
        Task<GenericResponse<Category>> Register(Category category);
        Task<GenericResponse<Category>> Update(Category category);
        Task Delete(int id);
        Task<GenericResponse<List<Category>>> List(int userId);
    }
}
