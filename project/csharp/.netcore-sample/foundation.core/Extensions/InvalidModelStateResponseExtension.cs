using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Edu.Ntu.Foundation.Core.ErrorResponses;

namespace Edu.Ntu.Foundation.Core.Extensions
{
    /// <summary>
    /// Return common http response for model validation error
    /// https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-2.2#validation-failure-error-response
    /// </summary>
    public static class InvalidModelStateResponseExtension
    {
        public static void ConfigureInvalidModelStateResponseApiBehavior(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState);
                    var errorResponse = new ErrorResponse()
                    {
                        Code = ErrorMessages.INVALID_PARAMETER_ERROR_CODE.ToString(),
                        Message = problemDetails.Title,
                        Parameters = problemDetails.Errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

        }
    }
}