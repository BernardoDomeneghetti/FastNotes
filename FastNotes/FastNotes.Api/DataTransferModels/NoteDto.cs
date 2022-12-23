using FastNotes.Common.Models;
using FastNotes.Common.Models.Enums;

namespace FastNotes.Api.DataTransferModels
{
    public class NoteDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string FileContent64 { get; set; } = string.Empty;
        public NoteStatus Status { get; set; } = NoteStatus.Anotation;
        public NotePriority Priority { get; set; } = NotePriority.Low;
    }
}
