using KoboPay.Logic.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace KoboPay.Api
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object? errors = null;
            var message = string.Empty;
            var errorDesscription = string.Empty;
            HttpStatusCode status = HttpStatusCode.BadRequest;
            switch (ex)
            {
                case ArgumentNullException re:
                    message = re.Message;
                    errors = re;
                    status = HttpStatusCode.BadRequest;
                    errorDesscription = ex.StackTrace;
                    break;
                case AccessViolationException re:
                    message = re.Message;
                    errors = re;
                    errorDesscription = re.StackTrace;
                    status = HttpStatusCode.BadRequest;
                    break;
                case Exception e:
                    message = e.Message;
                    errors = e;
                    errorDesscription = e.StackTrace;
                    status = HttpStatusCode.InternalServerError;
                    break;
            };

            var obj = JsonConvert.SerializeObject(errors);
            var response = new BaseResponseModel<string>
            {
                Message = message,
                StatusCode = status,
                Object = null,
                IsSuccessful = false
            };

            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            });
            await context.Response.WriteAsync(result);
        }
    }
}