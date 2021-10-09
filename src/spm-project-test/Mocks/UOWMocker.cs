using Moq;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPM_ProjectTests.Mocks
{
    class UOWMocker
    {
        public Mock<IUnitOfWork> mockUnitOfWork;

        public Mock<ICourseClassRepository> mockCourseClassRepository;

        public Mock<ILMSUserRepository> mockLMSUserRepository;

        public Mock<ICourseRepository> mockCourseRepository;

        public Mock<IChapterRepository> mockChapterRepository;

        public Mock<IClassEnrollmentRecordRepository> mockClassEnrollmentRecordRepository;

        public Mock<IProgressTrackerRepository> mockProgressTrackerRepository;

        public Mock<IQuizQuestionRepository> mockQuizQuestionRepository;

        public Mock<IQuizRepository> mockQuizRepository;

        public Mock<IResourceRepository> mockResourceRepository;

        public Mock<IUserAnswerRepository> mockUserAnswerRepository;



        public UOWMocker()
        {
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockCourseRepository = new Mock<ICourseRepository>();
            mockLMSUserRepository = new Mock<ILMSUserRepository>();
            mockCourseClassRepository = new Mock<ICourseClassRepository>();
            mockChapterRepository = new Mock<IChapterRepository>();
            mockClassEnrollmentRecordRepository = new Mock<IClassEnrollmentRecordRepository>();
            mockProgressTrackerRepository = new Mock<IProgressTrackerRepository>();
            mockQuizQuestionRepository = new Mock<IQuizQuestionRepository>();
            mockQuizRepository = new Mock<IQuizRepository>();
            mockResourceRepository = new Mock<IResourceRepository>();
            mockUserAnswerRepository = new Mock<IUserAnswerRepository>();


        }












    }
}
