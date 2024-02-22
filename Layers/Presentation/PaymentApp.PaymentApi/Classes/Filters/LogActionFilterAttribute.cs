using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Commons.Classes.Serialization.Json;
using PaymentApp.Logger;
using PaymentApp.Logger.Classes.Enums;
using PaymentApp.PaymentApi.Classes.Configuration.Logging;
using PaymentApp.Commons.Classes.Helpers.CommonObject;

namespace PaymentApp.PaymentApi.Classes.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {       
        public bool IsNeedLogResponseContent { get; }
        private readonly IConfiguration _configuration;
        private readonly LogWriter _logger;

        public LogActionFilterAttribute(IConfiguration configuration, bool isNeedLogResponseContent = true)
        {
            _configuration = configuration;
            IsNeedLogResponseContent = isNeedLogResponseContent;
            _logger = new LogWriter(EnumExtensions.ParseEnum<LogTypeEnum>(_configuration.GetValue<string>(LogConfiguration.LogTypeSection)));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var parameters = context.ActionArguments;

            var message = $"Вызвано действие: {context.HttpContext.Request.Path}" +
                          (parameters == null || parameters.Count == 0 ? string.Empty : $" с параметрами: {GetParameters(parameters)}");

            //_logger.Write(message);
             
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                //_logger.Write($"Получена ошибка: {context.Exception.GetType().Name} Сообщение: {context.Exception.Message}");
            }
            else
            {
                var message = $"Получен ответ: {context.HttpContext.Response.StatusCode}";

                if (IsNeedLogResponseContent && context.Result is JsonResult result)
                {
                    var content = result.Value;

                    if (content != null)
                    {
                        message += $"{Environment.NewLine}Объект ответа: ";
                        message += content.SerializeToJSON();
                    }
                }

                //_logger.Write(message);
            }

            base.OnActionExecuted(context);
        }

        private string GetParameters(IDictionary<string, object?> parameters)
        {
            return parameters.Aggregate(
                "",
                (current, parameter) =>
                    current + $"{Environment.NewLine} {parameter.Key}: {parameter.Value.SerializeToJSON()}");
        }
    }
}
