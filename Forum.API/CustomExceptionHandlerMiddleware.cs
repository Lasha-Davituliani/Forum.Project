using Forum.Service.Exceptions;
using System.Net;

namespace Forum.API
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            ApiResponse apiResponse = new();

            switch (exception)
            {
                case
                    TopicNotFoundException topicNotFoundException:
                    apiResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = topicNotFoundException.Message;
                    apiResponse.Result = null;
                    break;
                case
                    CommentNotFoundException commentNotFoundException:
                    apiResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = commentNotFoundException.Message;
                    apiResponse.Result = null;
                    break;
                case          
                    UserNotFoundException userNotFoundException:
                    apiResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = userNotFoundException.Message;
                    apiResponse.Result = null;
                    break;

                case
                    ArgumentException argumentException:
                    apiResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = argumentException.Message;
                    apiResponse.Result = null;
                    break;

                case
                    UnauthorizedAccessException unauthorizedAccessException:
                    apiResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = unauthorizedAccessException.Message;
                    apiResponse.Result = null;
                    break;

                case
                    Exception ex:
                    apiResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    apiResponse.IsSuccess = false;
                    apiResponse.Message = ex.Message;
                    apiResponse.Result = null;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = apiResponse.StatusCode;

            return context.Response.WriteAsJsonAsync(apiResponse);

        }
    }
}
