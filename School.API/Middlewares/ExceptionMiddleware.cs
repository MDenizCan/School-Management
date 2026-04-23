using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace School.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            string errorMessage;

            switch (ex)
            {
                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorMessage = ex.Message;
                    break;
                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorMessage = ex.Message;
                    break;
                case DbUpdateException dbEx:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    errorMessage = dbEx.InnerException?.Message ?? "A database conflict occurred. Possibly a duplicate record.";
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = ex.Message;
                    break;
            }

            var response = new
            {
                error = errorMessage
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
