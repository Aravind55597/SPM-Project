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
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
            HttpCode = HttpStatusCode.OK;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public Dictionary<string,string> Errors { get; set; }
        public string Message { get; set; }

        public HttpStatusCode HttpCode { get; set; }




    }
}
