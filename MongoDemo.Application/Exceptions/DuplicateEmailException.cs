namespace MongoDemo.Application.Exceptions;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException(string email)
        : base($"Customer with email '{email}' already exists.")
    { }
}