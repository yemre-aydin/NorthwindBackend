using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public class ExceptionMİddlewareExtensions
    {
        public ExceptionMİddlewareExtensions()
        {
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }


    }
}
