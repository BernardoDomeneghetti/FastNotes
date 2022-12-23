namespace FastNotes.Common.Models.Responses
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Value { get; set; }
    }
}
