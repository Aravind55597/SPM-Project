using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class Chapter: IEntityWithId
    {



        public Chapter()
        {

        }

        public int Id { get; private set; }

        public string Name { get; set; }

        public string  Description { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimestamp { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimestamp { get; set; }

        
        public List<Resource> Resources { get; set; }

        public List<ProgressTracker> ProgressTrackers  { get; set; }

        public List<Quiz> Quizzes { get; set; }

        public CourseClass CourseClass { get; set; }


        public int NumberOfQuizzes()
        {
            if (Quizzes==null)
            {
                return 0; 
            }

            return Quizzes.Count; 
        }

        public int NumberOfResources()
        {
            if (Resources == null)
            {
                return 0;
            }

            return Resources.Count;
        }



    }
}
