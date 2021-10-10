using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels
{
    public class LMSUser
    {
        //this is to form relationships with other tables , we leave the ApplicationUser just for Authntication & Authorization
        //also for data that rarely changes 

        public int Id { get; private set; }

        //Quiz answers 
        public List<UserAnswer> UserAnswers { get; set; }

        public List<ProgressTracker> ProgressTrackers { get; set; }

        //list of classes the user Trains 
        public List<CourseClass> ClassesTrained { get; set; }

        public List<ClassEnrollmentRecord> Enrollments { get; set; }

        
    }
}
