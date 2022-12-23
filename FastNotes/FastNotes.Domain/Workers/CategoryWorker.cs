using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using FastNotes.Domain.Interfaces;

namespace FastNotes.Domain.Workers
{
    internal class CategoryWorker : ICategoryWorker
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INoteWorker _noteWorker;

        public CategoryWorker(ICategoryRepository categoryRepository, INoteWorker noteWorker)
        {
            _categoryRepository = categoryRepository;
            _noteWorker = noteWorker;
        }

        public async Task Delete(int id)
        {
            var notes = (await _noteWorker.ListWhere( n => n.CategoryId == id)).Value;
            
            if (notes != null)
                foreach (var note in notes)
                {
                    await _noteWorker.Delete(note.Id);
                }

            await _categoryRepository.Delete(id);
        }

        public async Task<GenericResponse<List<Category>>> List(int userId)
        {
            return new GenericResponse<List<Category>>()
            {
                Success = true,
                Message = "Categories listed successfully!",
                Value = await _categoryRepository.List(userId)
            };
        }

        public async Task<GenericResponse<Category>> Register(Category category)
        {
            await _categoryRepository.Insert(category);
            return new GenericResponse<Category>()
            {
                Success = true,
                Message = "New category registered successfully!"
            };
        }

        public async Task<GenericResponse<Category>> Update(Category category)
        {
            await _categoryRepository.Update(category);
            return new GenericResponse<Category>()
            {
                Success = true,
                Message = "The category was updated successfully"
            };
        }
    }
}
