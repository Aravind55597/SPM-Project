using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Chapter: IEntityWithId
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string  Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimeStamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimeStamp { get; set; }

        
        public List<Resource> Resources { get; set; }

        public List<ProgressTracker> ProgressTrackers  { get; set; }

        public List<Quiz> Quizzes { get; set; }



    }
}
