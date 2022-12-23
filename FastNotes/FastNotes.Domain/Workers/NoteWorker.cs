using FastNotes.Common.Exceptions;
using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using FastNotes.Domain.Data;
using FastNotes.Domain.Interfaces;
using System.Linq.Expressions;

namespace FastNotes.Domain.Workers
{
    internal class NoteWorker : INoteWorker
    {
        private readonly INoteFileRepository _noteFileRepository;
        private readonly INoteRepository _noteRepository;
        private readonly ICategoryRepository _categoryRepository;

        public NoteWorker(INoteFileRepository noteFileRepository, INoteRepository noteRepository, ICategoryRepository categoryRepository)
        {
            _noteFileRepository = noteFileRepository;
            _noteRepository = noteRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<GenericResponse<Note>> Delete(int id)
        {
            await _noteRepository.Delete(id);
            return new GenericResponse<Note>()
            {
                Success = true,
                Message = "Note deleted successfully!"
            };
        }

        public async Task<GenericResponse<IEnumerable<Note>>> List(int userId, int categoryId)
        {
            var result = await _noteRepository.List(userId, categoryId);
            
            return new()
            {
                Success = true,
                Message = "Notes listed successflly!",
                Value = result,
            };
        }

        public async Task<GenericResponse<IEnumerable<Note>>> ListWhere(Expression<Func<Note, bool>> filter)
        {
            return new GenericResponse<IEnumerable<Note>>
            {
                Success = true,
                Message = "The notes were listed successfully",
                Value = await _noteRepository.ListWhere(filter)
            };
        }

        public async Task<GenericResponse<Note>> Update(Note note)
        {
            await _noteRepository.Update(note);

            return new()
            {
                Success = true,
                Message = "Note updated successfully!"
            };
        }

        public async Task<GenericResponse<Note>> Write(Note note)
        {
            await _noteRepository.Insert(note);
            if (note.NoteFiles != null)
            {
                List<Task> tasks = new List<Task>();
            
                foreach (var file in note.NoteFiles)
                {
                    tasks.Add(_noteFileRepository.Insert(file));
                }
                await Task.WhenAll(tasks);
            }

            return new GenericResponse<Note>()
            {
                Success = true,
                Message = "Note inserted successfully!"
            };
        }
    }
}
