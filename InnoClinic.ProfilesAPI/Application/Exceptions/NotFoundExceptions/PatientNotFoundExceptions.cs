using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions.NotFoundExceptions;

[ExcludeFromCodeCoverage]
public class PatientNotFoundException : ProfileException
{
    private const string DefaultMessage = "The patient was not found.";
    private const int PatientNotFoundExceptionStatusCode = StatusCodes.Status404NotFound;

    public PatientNotFoundException() : base(DefaultMessage, PatientNotFoundExceptionStatusCode) { }
    public PatientNotFoundException(string message) : base(message, PatientNotFoundExceptionStatusCode) { }
}