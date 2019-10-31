using System;
using Microsoft.AspNetCore.Builder;

namespace Store.ApiStore.Infrastructure.Middleware.Extensions
{
    public static class GlobalExceptionsExtensions
    {
        public static IApplicationBuilder EnableGlobalExceptions(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<GlobalExceptionsMiddleware>();
        }
    }
}
