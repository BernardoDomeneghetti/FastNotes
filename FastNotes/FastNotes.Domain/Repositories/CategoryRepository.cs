using FastNotes.Common.Models;
using FastNotes.Domain.Data;
using FastNotes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastNotes.Domain.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Delete(int Id)
        {
            var category = _dataContext.Categories.Find(Id);

            if (category == null) throw new Exception("Category id not found");

            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Category> Find(int categoryId)
        {
            return await _dataContext.Categories.FindAsync(categoryId);
        }

        public async Task Insert(Category category)
        {
            _dataContext.Categories.Add(category);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Category>> List(int userId)
        {
            return await _dataContext.Categories.Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task Update(Category category)
        {
            _dataContext.Entry(category).State = EntityState.Modified;
            _dataContext.Update(category);
            await _dataContext.SaveChangesAsync();
        }
    }
}
