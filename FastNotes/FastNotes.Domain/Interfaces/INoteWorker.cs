using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using System.Linq.Expressions;

namespace FastNotes.Domain.Interfaces
{
    public interface INoteWorker
    {
        Task<GenericResponse<Note>> Write(Note note);
        Task<GenericResponse<Note>> Update(Note note);
        Task<GenericResponse<Note>> Delete(int id);
        Task<GenericResponse<IEnumerable<Note>>> List(int userId, int categoryId);
        Task<GenericResponse<IEnumerable<Note>>> ListWhere(Expression<Func<Note, bool>> filter);
    }
}
