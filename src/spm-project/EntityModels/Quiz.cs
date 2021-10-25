using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SPM_Project.EntityModels
{
    public class Quiz : IEntityWithId
    {


        public int Id { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreationTimeStamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateTimeStamp { get; set; }

        //check if it is graded
        public bool IsGraded { get; set; }

        //relate to chapters if it is not a graded quiz
        public Chapter Chapter { get; set; }

        //list of questions present
        public List<QuizQuestion> Questions { get; set; }

        //time limit (in minutes)
        public decimal TimeLimit { get; set; }

        //[NotMapped]
        //public int TotalMarks
        //{
        //    get
        //    {
        //        if (this.IsGraded)
        //        {
        //            if (this.Questions.Count > 0)
        //            {
        //                var marks = this.Questions.Sum(q => q.Marks);

        //                return marks;
        //            }
        //        }

        //        return 0;
        //    }
        //}

    }
}