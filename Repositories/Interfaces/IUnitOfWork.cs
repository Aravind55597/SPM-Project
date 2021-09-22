using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories.Interfaces
{
    interface IUnitOfWork
    {

        //entity repositories 

        IApplicationUserRepository ApplicationUserRepository { get; }

        IChapterRepository ChapterRepository { get; }

        IClassEnrollmentRecordRepository ClassEnrollmentRecordRepository { get; }

        ICourseClassRepository CourseClassRepository { get; }

        ICourseRepository CourseRepository { get; }

        ILMSUserRepository LMSUserRepository { get;  }

        IProgressTrackerRepository ProgressTrackerRepository { get; }

        IQuizQuestionRepository QuizQuestionRepository { get; }

        IQuizRepository QuizRepository { get; }

        IResourceRepository ResourceRepository { get; }

        IUserAnswerRepository UserAnswerRepository { get;  }

        //function to end database transaction 
        Task Complete();

    }
}
