using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{
    public class ChapterDTO
    {



        public ChapterDTO()
        {

        }


        public ChapterDTO(Chapter domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            Description = domain.Description;
            if (domain.Resources!=null)
            {
                ResourceIds = domain.Resources.Select(r=>r.Id).ToList(); 
            };
            if (domain.Quizzes != null)
            {
                QuizIds = domain.Quizzes.Select(r => r.Id).ToList();
            };
            if (domain.CourseClass!=null)
            {
                CourseClassId = domain.CourseClass.Id;
            }
            
        }



        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        //list of resources id 

        public List<int> ResourceIds { get; set; }

        //list of quizzes id 
        public List<int> QuizIds { get; set; }


        public int CourseClassId { get; set; }



    }
}
