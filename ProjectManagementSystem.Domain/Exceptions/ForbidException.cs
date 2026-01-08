namespace ProjectManagementSystem.Domain.Exceptions;
/// <summary>
/// Represents an exception that is thrown when an operation is forbidden due to insufficient permissions or access
/// rights.
/// </summary>
/// <remarks>Use this exception to indicate that the current user or context is not authorized to perform the
/// requested action. This exception is typically used in scenarios involving authorization checks, such as enforcing
/// security policies or access control. The default message provides a generic description, but a custom message can be
/// supplied for more specific error details.</remarks>
public class ForbidException : Exception
{
    public ForbidException()
        : base("You are not allowed to perform this action.")
    {
    }

    public ForbidException(string message)
        : base(message)
    {
    }

    public ForbidException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
