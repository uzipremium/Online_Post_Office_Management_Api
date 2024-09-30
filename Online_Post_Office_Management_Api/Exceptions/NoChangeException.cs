namespace Online_Post_Office_Management_Api.Exceptions
{
    public class NoChangeException : Exception
    {
        public NoChangeException() : base("No changes were made.")
        {
        }
    }
}

