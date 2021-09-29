using Microsoft.AspNetCore.Http;
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
                        
                        break;
                    case NotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                result.HttpCode = response.StatusCode;
                result.Errors =(Dictionary<string, string>) error.Data; 
                var httpResponse = Newtonsoft.Json.JsonConvert.SerializeObject(

                    result

                    );
                await response.WriteAsync(httpResponse);
            }
        }
    }
}