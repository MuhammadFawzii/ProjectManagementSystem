
namespace ProjectManagementSystem.Domain.Exceptions;
/// <summary>
/// Represents an exception that is thrown when a specified resource cannot be found.
/// </summary>
/// <remarks>Use this exception to indicate that an operation failed because the requested resource does not
/// exist. This exception typically includes information about the resource type and identifier to assist with error
/// handling and diagnostics.</remarks>
/// <param name="resourceType">The type or category of the resource that was not found. This value is used to identify the kind of resource
/// involved in the exception.</param>
/// <param name="resourceIdentifier">The unique identifier of the resource that could not be located. This value helps specify which resource was
/// missing.</param>
public class NotFoundException(string resourceType, string resourceIdentifier)
    :Exception ($"{resourceType} with id: {resourceIdentifier} doesn't exist" )
{
}
