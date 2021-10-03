using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SPM_Project.CustomExceptions
{

    [Serializable]
    public class NotFoundException:Exception
    {





        public NotFoundException()
        {
        }
        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, IDictionary<string, string> errorDict) : base(message)
        {

            foreach (KeyValuePair<string, string> err in errorDict)
            {


                Data.Add(err.Key.ToString(), err.Key.ToString());

            }

        }




        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


    }

}
