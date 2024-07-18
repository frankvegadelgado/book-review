using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Exceptions
{
    [Serializable]

    public class ErrorInfoResponse
    {
        public string Message { get; set; }

        public string Description { get; set; }

        public ErrorInfoResponse(ErrorInfo errorInfo)
        {
            Message = errorInfo?.Message;
            Description = errorInfo?.Details;
        }

    }
}
