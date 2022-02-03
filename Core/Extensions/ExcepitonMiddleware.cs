using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExcepitonMiddleware
    {
        private RequestDelegate _next;

        public  ExceptionMiddleware(RequestDelegate next)
        {

        }


    }
}
