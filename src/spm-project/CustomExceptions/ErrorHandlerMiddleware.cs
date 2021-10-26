using Microsoft.AspNetCore.Http;
using SPM_Project;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs.RRModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomExceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public HttpResponse ReturnResponseObject(HttpContext context)
        {
            return context.Response;

        }

        public Response<object> CreateResultObject(HttpContext context, HttpResponse response)
        {

            response.ContentType = "application/json";
            var result = new Response<object>();
            return result;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }


            catch (Exception error)
            {

                var response = context.Response;
                response.ContentType = "application/json";
                var result = new Response<object>();
                switch (error)
                {
                    case BadRequestException e:
                        // bad request due to faulty input
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        result.Errors = e.ErrorDict;
                        break;
                    case NotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        result.Errors = e.ErrorDict;
                        break;
                    case NotImplementedException e:
                        response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        break; 
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                //add the httpcode
                result.HttpCode = response.StatusCode;

                //add the custom message 
                result.Message = error.Message;

                //serialised response 
                var httpResponse = Newtonsoft.Json.JsonConvert.SerializeObject(

                    result

                    );
                await response.WriteAsync(httpResponse);

        


            }


        }
    }
}
