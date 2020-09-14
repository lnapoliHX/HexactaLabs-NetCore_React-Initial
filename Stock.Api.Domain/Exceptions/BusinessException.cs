using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Stock.Api.Domain.Exceptions
{
    public class BusinessException : System.Exception
    {
        public HttpStatusCode HttpErrorCode { get; set; }

        public BusinessException(string message, HttpStatusCode code) : base(message)
        {
            HttpErrorCode = code;
        }
    }
}