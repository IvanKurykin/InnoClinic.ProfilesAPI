using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions;

[ExcludeFromCodeCoverage]
public class DtoIsNullException : ProfileException
{
    private const string DefaultMessage = "Incoming data cannot be null.";
    private const int DtoIsNullExceptionStatusCode = StatusCodes.Status400BadRequest;

    public DtoIsNullException() : base(DefaultMessage, DtoIsNullExceptionStatusCode) { }
    public DtoIsNullException(string message) : base(message, DtoIsNullExceptionStatusCode) { }
}