using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions;


[ExcludeFromCodeCoverage]
public abstract class ProfileException : Exception
{
    public int HttpStatusCode { get; }

    protected ProfileException(string message, int statusCode)
        : base(message)
    {
        HttpStatusCode = statusCode;
    }
}