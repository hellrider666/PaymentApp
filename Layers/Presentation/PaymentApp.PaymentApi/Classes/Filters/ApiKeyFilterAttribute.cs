using Microsoft.AspNetCore.Mvc.Filters;
using PaymentApp.Logger;
using PaymentApp.PaymentApi.Classes.Configuration.Logging;
using PaymentApp.PaymentApi.Classes.Configuration.Security;
using PaymentApp.Commons.Classes.Helpers.CommonObject;
using PaymentApp.Logger.Classes.Enums;

namespace PaymentApp.PaymentApi.Classes.Filters
{
    public class ApiKeyFilterAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        private readonly LogWriter _logger;

        public ApiKeyFilterAttribute(IConfiguration configuration)
        {
            _configuration = configuration;

            _logger = new LogWriter(EnumExtensions.ParseEnum<LogTypeEnum>(_configuration.GetValue<string>(LogConfiguration.LogTypeSection)));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string currentApiKey = _configuration.GetValue<string>(ApiKeyConfiguration.ApiKeySection);

            if (string.IsNullOrEmpty(currentApiKey))
            {
                _logger.Write($"Значение ключа API не указано в конфиг файле");

                throw new ArgumentException("The server is unavailable due to a secure connection");
            }

            var inputKey = context.HttpContext.Request.Headers[ApiKeyConfiguration.ApiKeyHeaderName];

            if (string.IsNullOrEmpty(inputKey))
            {
                throw new ArgumentException("ApiKey cannot be empty");
            }

            if (inputKey != currentApiKey)
            {
                throw new ArgumentException("Wrong api key");
            }
        }
    }
}
