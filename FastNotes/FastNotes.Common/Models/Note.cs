using FastNotes.Common.Models.Enums;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace FastNotes.Common.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public NoteStatus Status { get; set; } = NoteStatus.Anotation;
        public NotePriority Priority { get; set; } = NotePriority.Low;
        public string FileBase64 { get; set; } = string.Empty;
        public List<NoteFile>? NoteFiles { get; set; }
    }
}
