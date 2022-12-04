using System.Net;

namespace Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }

        public HttpStatusCodeException(int statusCode) : base(GetDefaultMessage(statusCode))
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode) : base(GetDefaultMessage((int)statusCode))
        {
            StatusCode = (int)statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = (int)statusCode;
        }

        private static string GetDefaultMessage(int statusCode)
        {
            switch (statusCode)
            {
                case 404: return "Not Found";
                default: return "";
            }
        }
    }
}
