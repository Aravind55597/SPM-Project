using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class ProgressTracking
    {
        public int Id { get; set; }

        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        public int MyProperty { get; set; }

    }
}
