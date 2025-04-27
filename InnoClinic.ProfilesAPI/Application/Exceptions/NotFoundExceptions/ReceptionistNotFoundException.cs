using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions.NotFoundExceptions;

[ExcludeFromCodeCoverage]
public class ReceptionistNotFoundException : ProfileException
{
    private const string DefaultMessage = "The receptionist was not found.";
    private const int ReceptionistNotFoundExceptionStatusCode = StatusCodes.Status404NotFound;

    public ReceptionistNotFoundException() : base(DefaultMessage, ReceptionistNotFoundExceptionStatusCode) { }
    public ReceptionistNotFoundException(string message) : base(message, ReceptionistNotFoundExceptionStatusCode) { }
}