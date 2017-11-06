using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogExampleApi.Models
{
    public class Response
    {
        public readonly static int STATUS_OK = 200;
        public readonly static int STATUS_CREATED = 202;
        public readonly static int STATUS_NOT_FOUND = 404;
        public readonly static string MESSAGE_OK = "ok";
        public readonly static string MESSAGE_ERROR="error";


        public string Status { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; } // put object data here
    }
}