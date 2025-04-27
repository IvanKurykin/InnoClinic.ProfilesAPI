using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions.NotFoundExceptions;

[ExcludeFromCodeCoverage]
public class DoctorNotFoundException : ProfileException
{
    private const string DefaultMessage = "The doctor was not found.";
    private const int DoctorNotFoundExceptionStatusCode = StatusCodes.Status404NotFound;

    public DoctorNotFoundException() : base(DefaultMessage, DoctorNotFoundExceptionStatusCode) { }
    public DoctorNotFoundException(string message) : base(message, DoctorNotFoundExceptionStatusCode) { }
}