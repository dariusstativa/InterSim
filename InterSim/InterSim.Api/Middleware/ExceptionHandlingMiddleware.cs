using System.Text.Json;

namespace InterSim.Api.Middleware;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InvalidOperationException ex)
        {
           
            await WriteProblem(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception)
        {
            
            await WriteProblem(context, StatusCodes.Status500InternalServerError, "Unexpected error.");
        }
    }

    private static async Task WriteProblem(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var payload = new
        {
            status = statusCode,
            error = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}