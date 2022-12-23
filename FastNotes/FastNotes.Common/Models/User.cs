using System.ComponentModel.DataAnnotations.Schema;

namespace FastNotes.Common.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public List<Note>? Notes { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
