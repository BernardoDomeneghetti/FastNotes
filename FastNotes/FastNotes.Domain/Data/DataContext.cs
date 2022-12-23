using FastNotes.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace FastNotes.Domain.Data
{
    public class DataContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteFile> NoteFiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}