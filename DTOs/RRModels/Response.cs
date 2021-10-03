using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SPM_Project.DTOs.RRModels
{
    public class Response<T>  
    {


        public Response()
        {
        }

        //Success response
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            HttpCode = (int)HttpStatusCode.OK;
        }

        //failure response
        public Response(int errorCode , Dictionary<string,string> errors)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = errors;
            Data = default(T);
            HttpCode = errorCode;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public IDictionary<string,string> Errors { get; set; }
        public string Message { get; set; }

        public int HttpCode { get; set; }

    }
}
