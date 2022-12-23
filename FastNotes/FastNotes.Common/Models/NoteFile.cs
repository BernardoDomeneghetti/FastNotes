using System.ComponentModel.DataAnnotations.Schema;

namespace FastNotes.Common.Models
{
    public class NoteFile
    {
        public int Id { get; set; }
        public int NoteId{ get; set; }
        public Note? Note{ get; set; }
        public string Path { get; set; } = string.Empty;
        public string ContentBase64 { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
