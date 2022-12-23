namespace FastNotes.Api.DataTransferModels
{
    public class NoteFileDto
    {
        public string contentBase64 { get; set; } = string.Empty;
        public int NoteId { get; set; }
    }
}
