using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        //entity repositories 

        public IApplicationUserRepository ApplicationUserRepository { get; }

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
        public Task CompleteAsync();

    }
}
