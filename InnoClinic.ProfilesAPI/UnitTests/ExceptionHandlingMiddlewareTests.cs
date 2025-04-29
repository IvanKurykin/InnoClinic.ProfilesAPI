using System.Net;
using System.Text.Json;
using API.Middleware;
using Application.Exceptions.NotFoundExceptions;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Xunit;
using UnitTests.TestCases;

namespace UnitTests;

public class ExceptionHandlingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsyncShouldReturn400ForValidationException()
    {
        var httpContext = new DefaultHttpContext();
        await using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        var validationFailures = new List<FluentValidation.Results.ValidationFailure>
        {
            new(MiddlewareTestCases.NameField, MiddlewareTestCases.NameError),
            new(MiddlewareTestCases.EmailField, MiddlewareTestCases.EmailError)
        };
        var validationException = new ValidationException(validationFailures);

        var middleware = new ExceptionHandlingMiddleware(_ => throw validationException);

        await middleware.InvokeAsync(httpContext);

        httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var response = await new StreamReader(memoryStream).ReadToEndAsync();

        var json = JsonSerializer.Deserialize<JsonElement>(response);
        json.GetProperty(MiddlewareTestCases.Error).GetString().Should().Be(MiddlewareTestCases.ValidationErrorMessage);

        var details = json.GetProperty(MiddlewareTestCases.Details).EnumerateArray().ToList();
        details.Should().HaveCount(2);
    }

    [Fact]
    public async Task InvokeAsyncShouldReturnCustomStatusForProfileException()
    {
        var httpContext = new DefaultHttpContext();
        await using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        var profileException = new DoctorNotFoundException();

        var middleware = new ExceptionHandlingMiddleware(_ => throw profileException);

        await middleware.InvokeAsync(httpContext);

        httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var response = await new StreamReader(memoryStream).ReadToEndAsync();

        var json = JsonSerializer.Deserialize<JsonElement>(response);
        json.GetProperty(MiddlewareTestCases.Error).GetString().Should().Be(MiddlewareTestCases.DoctorNotFoundMessage);
        json.GetProperty(MiddlewareTestCases.Details).ValueKind.Should().Be(JsonValueKind.Null);
    }

    [Fact]
    public async Task InvokeAsyncShouldReturn500ForUnhandledException()
    {
        var httpContext = new DefaultHttpContext();
        await using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        var exception = new Exception(MiddlewareTestCases.GenericExceptionMessage);

        var middleware = new ExceptionHandlingMiddleware(_ => throw exception);

        await middleware.InvokeAsync(httpContext);

        httpContext.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var response = await new StreamReader(memoryStream).ReadToEndAsync();

        var json = JsonSerializer.Deserialize<JsonElement>(response);
        json.GetProperty(MiddlewareTestCases.Error).GetString().Should().Be(MiddlewareTestCases.GenericExceptionMessage);
        json.GetProperty(MiddlewareTestCases.Details).ValueKind.Should().Be(JsonValueKind.Null);
    }
}
