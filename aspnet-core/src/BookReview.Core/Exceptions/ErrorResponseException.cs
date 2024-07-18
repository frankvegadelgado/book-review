using Abp.UI;
using System;
using System.Net;

namespace BookReview.Exceptions
{
    [SerializableAttribute]

    public class ErrorResponseException : UserFriendlyException
    {

        public HttpStatusCode StatusCode { get; set; }

        
        public ErrorResponseException(HttpStatusCode statusCode, string message, string details) : base(message, details)
        {
            StatusCode = statusCode;
        }

        public ErrorResponseException(string message, string details) : base(message, details)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public ErrorResponseException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public ErrorResponseException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }


    }
}
