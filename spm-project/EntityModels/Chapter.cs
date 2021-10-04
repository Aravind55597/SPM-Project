using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Chapter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string  Description { get; set; }

        public DateTime CreationTimeStamp { get; set; }

        public DateTime UpdateTimeStamp { get; set; }

        
        public List<Resource> Resources { get; set; }

        public List<ProgressTracker> ProgressTrackers  { get; set; }

        public List<Quiz> Quizzes { get; set; }
    }
}
