namespace ProjectManagementSystem.Domain.Exceptions;
/// <summary>
/// Represents an exception that is thrown when an operation fails to complete successfully.
/// </summary>
/// <remarks>Use this exception to indicate that a specific operation could not be performed as expected. The
/// exception message typically includes the name of the failed operation and, optionally, additional details about the
/// failure. This exception is intended for scenarios where a more specific exception type is not available or
/// applicable.</remarks>
public class OperationFailedException : Exception
{
    public OperationFailedException(string operation)
        : base($"Failed to {operation}.")
    {
    }

    public OperationFailedException(string operation, string details)
        : base($"Failed to {operation}. {details}")
    {
    }

    public OperationFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
