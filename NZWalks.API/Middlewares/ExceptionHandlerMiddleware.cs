using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.next = next;
            this.logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try{
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                string message = $"{errorId} : {ex.Message}";
                logger.LogError(ex, message);


                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong."
                };

                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
