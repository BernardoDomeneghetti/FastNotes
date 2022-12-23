using FastNotes.Common.Models;
using System.Linq.Expressions;

namespace FastNotes.Domain.Interfaces
{
    public interface INoteRepository
    {
        Task Delete(int noteId);

        Task<Note?> GetById(int userId, int noteId);

        Task Insert(Note note);

        Task<IEnumerable<Note>> List(int userId, int categoryId);
        Task<IEnumerable<Note>> ListWhere(Expression<Func<Note, bool>> filter);
        Task Update(Note note);
    }
}
