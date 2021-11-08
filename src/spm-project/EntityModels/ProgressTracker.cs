using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class ProgressTracker: IEntityWithId
    {
        public int Id { get; private set; }


        public DateTime CreationTimestamp { get; set; }

        public DateTime UpdateTimestamp { get; set; }

        public Course Course { get; set; }
        public Chapter Chapter { get; set; }
        public bool CompletedUngradedQuiz { get; set; }

        public bool HaveViewedResources { get; set; }
        public LMSUser LMSUser { get; set; }



    }
}
