using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        //entity repositories 

        //applciation user is removed everything is done via lms user 
        //If any function require stuff via application user entity , we still will deal with lms user 
        //this simplifies & prevents any form of confusion 

        public IChapterRepository ChapterRepository { get; }

        public IClassEnrollmentRecordRepository ClassEnrollmentRecordRepository { get; }

        public ICourseClassRepository CourseClassRepository { get; }

        public ICourseRepository CourseRepository { get; }

        public ILMSUserRepository LMSUserRepository { get;  }

        public IProgressTrackerRepository ProgressTrackerRepository { get; }

        public IQuizQuestionRepository QuizQuestionRepository { get; }

        public IQuizRepository QuizRepository { get; }

        public IResourceRepository ResourceRepository { get; }

        public IUserAnswerRepository UserAnswerRepository { get;  }

        //function to end database transaction 
        public Task<int> CompleteAsync();

    }
}
