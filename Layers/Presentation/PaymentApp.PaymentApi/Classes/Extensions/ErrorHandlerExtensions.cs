using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
                        OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                        _ => (int)HttpStatusCode.ServiceUnavailable,
                    };

                    var responseError = new ValidationResponse
                    {
                        Message = contextFeature.Error.GetBaseException().Message
                    };

                    await context.Response.WriteAsJsonAsync(responseError);
                });
            });
        }

        public static void ConfigureModelBindingExceptionHandling(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    ValidationProblemDetails error = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ValidationProblemDetails(actionContext.ModelState)).FirstOrDefault();

                    var jsonResult = new JsonResult(error)
                    {
                        Value = new ValidationResponse()
                        {
                            Message = error.Errors.FirstOrDefault(x => x.Key != "request").Value.FirstOrDefault()
                        },
                        StatusCode = error.Status ?? (int)HttpStatusCode.BadRequest
                    };

                    return jsonResult;
                };
            });
        }
    }
}
