using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Cursus_Data.Models.DTOs;
using Newtonsoft.Json;

namespace Cursus_Business.Exceptions
{
    public sealed class DatabaseTimeOutException : IExceptionHandler
    {
        private readonly ILogger<DatabaseTimeOutException> _logger;

        public DatabaseTimeOutException(ILogger<DatabaseTimeOutException> logger)
        {
            _logger = logger;
        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ExceptionHandlerResponse
            {
                isSuccess = false,
                isFailure = true,
                ErrorDetail = new ErrorResponse
                {
                    code = StatusCodes.Status400BadRequest,
                    message = exception.Message
                }
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errorMessage = JsonConvert.SerializeObject(problemDetails);

            await httpContext.Response.WriteAsync(errorMessage, cancellationToken);

            return true;
        }
    }
}
