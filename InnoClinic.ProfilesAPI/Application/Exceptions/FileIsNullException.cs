using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions;

[ExcludeFromCodeCoverage]
public class FileIsNullException : ProfileException
{
    private const string DefaultMessage = "File cannot be null.";
    private const int FileIsNullExceptionStatusCode = StatusCodes.Status400BadRequest;

    public FileIsNullException() : base(DefaultMessage, FileIsNullExceptionStatusCode) { }
    public FileIsNullException(string message) : base(message, FileIsNullExceptionStatusCode) { }
}