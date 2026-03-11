using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Tasks_Management_System.Application.Common;

public class ApiResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ApiResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);

            // Handle 401 / 403 automatically
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid token",
                    Data = null
                };
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<string>
            {
                Success = false,
                Message = ex.Message,
                Data = null
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}