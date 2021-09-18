using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationTimeStamp { get; set; }


        public DateTime UpdateTimeStamp { get; set; }

    }
}
