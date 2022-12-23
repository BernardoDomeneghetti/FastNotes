namespace FastNotes.Common.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound() : base("The user was not found"){}
    }
}
