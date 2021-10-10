﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class ProgressTracker
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTimestamp { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimestamp { get; set; }

        public Course Course { get; set; }
        public Chapter Chapter { get; set; }
        public bool Completed { get; set; }

        public bool HaveViewedResources { get; set; }
        public LMSUser LMSUser { get; set; }
    }
}
