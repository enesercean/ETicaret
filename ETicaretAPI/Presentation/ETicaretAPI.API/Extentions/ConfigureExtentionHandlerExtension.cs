using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace ETicaretAPI.API.Extentions
{
    static public class ConfigureExtentionHandlerExtension
    {
        static public void ConfigureExtentionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    if (exception != null)
                    {
                        logger.LogError(exception.Error, exception.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",
                            Title = "Hata alındı."
                        }));
                    }
                });
            });
        }
    }
}
