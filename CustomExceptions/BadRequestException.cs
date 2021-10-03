using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SPM_Project.CustomExceptions
{
    [Serializable]

    public class BadRequestException: CustomExceptionParent
    {


        public BadRequestException()

        {
        }



        public BadRequestException(string message)
            : base(message)
        {
        }



        public BadRequestException(string message, Dictionary<string, string> errorDict) : base(message , errorDict)
        {


        }


        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }


        public BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


    }

}
