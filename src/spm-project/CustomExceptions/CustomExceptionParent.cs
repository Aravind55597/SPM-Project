using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SPM_Project.CustomExceptions
{

    [Serializable]
    public class CustomExceptionParent:Exception
    {

        public Dictionary<string,string> ErrorDict{ get; set; }

        public CustomExceptionParent()

        {
        }



        public CustomExceptionParent(string message)
            : base(message)
        {
        }



        public CustomExceptionParent(string message,Dictionary<string, string> errorDict) : base(message)
        {

            ErrorDict = errorDict; 
        }


        public CustomExceptionParent(string message, Exception innerException)
            : base(message, innerException)
        {
        }


        public CustomExceptionParent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }



    }



}
