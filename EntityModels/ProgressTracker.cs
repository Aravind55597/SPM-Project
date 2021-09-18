using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class ProgressTracker
    {
        public int Id { get; set; }

        public DateTime CreateTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        public Chapter Chapter { get; set; }

        public bool Completed { get; set; }

        public LMSUser LMSUser { get; set; }
    }
}
