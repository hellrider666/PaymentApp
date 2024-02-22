using Microsoft.AspNetCore.Diagnostics;
using PaymentApp.PaymentApi.Classes.Responses;
using System.Net;

namespace PaymentApp.PaymentApi.Classes.Extensions
{
    public static class ErrorHandlerExtensions
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        ArgumentException => (int)HttpStatusCode.BadRequest,
                        OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable
                    };

                    var responseError = new ValidationResponse
                    {
                        Message = contextFeature.Error.GetBaseException().Message
                    };

                    await context.Response.WriteAsJsonAsync(responseError);
                });
            });
        }
    }
}
