using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Store.ApiStore.Infrastructure.Exceptions;

namespace Store.ApiStore.Infrastructure.Middleware
{
    public sealed class GlobalExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IHostingEnvironment environment)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                try
                {
                    HttpStatusCode status;
                    string exceptionKey;

                    if (ex is AggregateException && ex.InnerException != null)
                        ex = ex.InnerException;

                    switch (ex)
                    {
                        case InvalidArgumentException _:
                            {
                                exceptionKey = ex.Message;
                                status = HttpStatusCode.BadRequest;
                            }
                            break;
                        case NotFoundException _:
                            {
                                exceptionKey = ex.Message;
                                status = HttpStatusCode.NotFound;
                            }
                            break;
                        case LogicalException _:
                            {
                                exceptionKey = ex.Message;
                                status = HttpStatusCode.Conflict;
                            }
                            break;
                        default:
                            {
                                exceptionKey = "InternalServerError (report to a program administrator)";
                                status = HttpStatusCode.InternalServerError;
                            }
                            break;
                    }
                    var response = httpContext.Response;

                    if (response.HasStarted)
                    {
                        //Log.Error(ex, "Error: exception outside scoupe!");
                    }
                    else
                    {
                        response.StatusCode = (int)status;
                        response.ContentType = "application/json";
                    }

                    object error;
                    var exceptionTypeName = ex.GetType().Name;

                    if (environment.IsDevelopment())
                    {
                        error = new
                        {
                            type = exceptionTypeName,
                            key = exceptionKey,
                            stack = ex.StackTrace.Split("\r\n")

                        };
                    }
                    else
                    {
                        error = new
                        {
                            type = (status == HttpStatusCode.InternalServerError)
                                ? "InternalServerError"
                                : exceptionTypeName,
                            key = exceptionKey

                        };

                       // Log.Error("ErrorType: " + exceptionTypeName + "\r\n" + "ErrorString: " + ex);
                    }

                    var result = JsonConvert.SerializeObject(error);
                   // Log.Error(result);

                    await response.WriteAsync(result);
                }
                catch (Exception exc)
                {
                    //Log.Error(exc, "Error in global exception filter!");
                }
            }
        }
    }
}
