using System.Net;

namespace JobPlatformUIService.Web
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
