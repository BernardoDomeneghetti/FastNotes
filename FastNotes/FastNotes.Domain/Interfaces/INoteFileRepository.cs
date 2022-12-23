using FastNotes.Common.Models;
using FastNotes.Domain.Data;

namespace FastNotes.Domain.Interfaces
{
    public interface INoteFileRepository
    {
        Task Delete(int fileId);
        Task Insert(NoteFile noteFile);
        Task<IEnumerable<NoteFile>> List(int noteId);
    }
}
