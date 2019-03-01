using System;
using System.Net;

namespace OAuth2.Model
{
    public abstract class Response : IResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
