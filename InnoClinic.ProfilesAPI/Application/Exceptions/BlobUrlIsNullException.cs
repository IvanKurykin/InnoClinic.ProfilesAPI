using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

[ExcludeFromCodeCoverage]
public class BlobUrlIsNullException : ProfileException
{
    private const string DefaultMessage = "Blob Url cannot be null.";
    private const int BlobUrlIsNullExceptionStatusCode = StatusCodes.Status400BadRequest;

    public BlobUrlIsNullException() : base(DefaultMessage, BlobUrlIsNullExceptionStatusCode) { }
    public BlobUrlIsNullException(string message) : base(message, BlobUrlIsNullExceptionStatusCode) { }
}