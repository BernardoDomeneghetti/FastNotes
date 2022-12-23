using FastNotes.Common.Models;
using FastNotes.Domain.Data;
using FastNotes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FastNotes.Domain.Repositories
{
    internal class NoteRepository : INoteRepository
    {
        private DataContext _dataContext;

        public NoteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Delete(int noteId)
        {
            var note = _dataContext.Notes.Find(noteId);
            
            if (note == null) throw new Exception("The note id was not found to be deleted!");
            
            _dataContext.Notes.Remove(note);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Note?> GetById(int userId, int noteId)
        {
            return await _dataContext.Notes.Where(n => n.Category.UserId == userId && n.Id == noteId).FirstOrDefaultAsync();
        }

        public async Task Insert(Note note)
        {
            _dataContext.Notes.Add(note);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Note>> List(int userId, int categoryId)
        {
            return await _dataContext.Notes.Where(n => (n.Category.UserId == userId && n.CategoryId == categoryId )).ToListAsync();
        }

        public async Task<IEnumerable<Note>> ListWhere(Expression<Func<Note, bool>> filter)
        {
            return await _dataContext.Notes.Where(filter).ToListAsync();
        }

        public async Task Update(Note note)
        {
            _dataContext.Entry(note).State = EntityState.Modified;
            _dataContext.Update(note);
            await _dataContext.SaveChangesAsync();
        }
    }
}
