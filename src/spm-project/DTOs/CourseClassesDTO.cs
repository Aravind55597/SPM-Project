using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.DTOs
{
    public class CourseClassesDTO
    {

        public CourseClassesDTO(CourseClass domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            StartRegistration = domain.StartRegistration;
            EndRegistration = domain.EndRegistration;
            StartClass = domain.StartClass;
            EndClass = domain.EndClass;

            if (domain.ClassTrainer!=null)
            {
                TrainerName = domain.ClassTrainer.Name;
                TrainerId = domain.ClassTrainer.Id; 
            }
    
            CourseName = domain.Course.Name;
            CourseId = domain.Course.Id;
            Slots = domain.Slots; 
        }



        public int Id { get; private set; }

        public string Name { get; set; }


        public DateTime StartRegistration { get; set; }

        public DateTime EndRegistration { get; set; }

        public DateTime StartClass { get; set; }

        public DateTime EndClass { get; set; }


        //trainer of the class (trainer ID & trainer name)

        public string TrainerName { get; set; }

        public int TrainerId { get; set; }

        //course id & course name 

        public string CourseName { get; set; }

        public int CourseId { get; set; }

        //cnumbert o fchapters 

        public int Slots { get; set; }



    }
}
