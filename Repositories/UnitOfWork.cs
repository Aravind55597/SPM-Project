using SPM_Project.Data;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            // ApplicationUserRepository = new ApplicationUserRepository(context);
            ChapterRepository = new ChapterRepository(context);
            ClassEnrollmentRecordRepository = new ClassEnrollmentRecordRepository(context);
            CourseClassRepository = new CourseClassRepository(context);
            CourseRepository = new CourseRepository(context);
            LMSUserRepository = new LMSUserRepository(context);
            ProgressTrackerRepository = new ProgressTrackerRepository(context);
            QuizRepository = new QuizRepository(context);
            ResourceRepository = new ResourceRepository(context);
            UserAnswerRepository = new UserAnswerRepository(context);
        }

        //all the repositories 
        // public IApplicationUserRepository ApplicationUserRepository { get; }

        public IChapterRepository ChapterRepository { get; }

        public IClassEnrollmentRecordRepository ClassEnrollmentRecordRepository { get; }

        public ICourseClassRepository CourseClassRepository { get; }

        public ICourseRepository CourseRepository { get; }

        public ILMSUserRepository LMSUserRepository { get; }

        public IProgressTrackerRepository ProgressTrackerRepository { get; }

        public IQuizQuestionRepository QuizQuestionRepository { get; }

        public IQuizRepository QuizRepository { get; }

        public IResourceRepository ResourceRepository { get; }

        public IUserAnswerRepository UserAnswerRepository { get; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync(); 

        }

    }
}
