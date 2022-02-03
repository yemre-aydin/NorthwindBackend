using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;


        public ExceptionMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext);
                
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(new ErrorDetail
            {

                StatusCode = httpContext.Response.StatusCode,
                Message = "Internaş Server Error"


            }.ToString());

        }
    }
}
