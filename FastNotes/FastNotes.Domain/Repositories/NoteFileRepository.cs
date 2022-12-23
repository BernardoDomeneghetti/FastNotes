using FastNotes.Common.Models;
using FastNotes.Domain.Data;
using FastNotes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastNotes.Domain.Repositories
{
    internal class NoteFileRepository : INoteFileRepository
    {
        private DataContext _dataContext;

        public NoteFileRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Delete(int fileId)
        {
            var noteFile = _dataContext.NoteFiles.Find(fileId);

            if (noteFile == null) throw new Exception("File Id not found");

            _dataContext.NoteFiles.Remove(noteFile);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Insert(NoteFile noteFile)
        {
            _dataContext.NoteFiles.Add(noteFile);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<NoteFile>> List(int noteId)
        {
            return await _dataContext.NoteFiles.Where(n => noteId == 0 || n.Id == noteId).ToListAsync();
        }
    }
}
