using FastNotes.Common.Models;

namespace FastNotes.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task Delete(int Id);
        Task<Category> Find(int categoryId);
        Task Insert(Category category);
        Task<List<Category>> List(int userId);
        Task Update(Category category);
    }
}
