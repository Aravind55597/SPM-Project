using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{
    public class CourseDTO
    {
        public CourseDTO(Course domain )
        {
            Id = domain.Id;
            Name = domain.Name;
            Description = domain.Description;
            if (PreRequisites != null ) {
                PreRequisites = domain.PreRequisites.Select(p => p.Name).ToList();
                PreRequisitesIds = domain.PreRequisites.Select(p => p.Id).ToList();
            }
            PassingPercentage = domain.PassingPercentage;
            NumClasses = domain.GetNumCourseClasses(); 

        }


        public int Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public List<string> PreRequisites { get; set; }

        public List<int> PreRequisitesIds { get; set; }


        public decimal PassingPercentage { get; set; }

        public int NumClasses { get; set; }


    }
}
