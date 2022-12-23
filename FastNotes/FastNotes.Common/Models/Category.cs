using System.ComponentModel.DataAnnotations.Schema;

namespace FastNotes.Common.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Note>? Notes { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
