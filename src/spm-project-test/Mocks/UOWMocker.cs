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
            mockUnitOfWork = new Mock<IUnitOfWork>(); //
            mockCourseRepository = new Mock<ICourseRepository>();//
            mockLMSUserRepository = new Mock<ILMSUserRepository>(); //
            mockCourseClassRepository = new Mock<ICourseClassRepository>();//
            mockChapterRepository = new Mock<IChapterRepository>(); //
            mockClassEnrollmentRecordRepository = new Mock<IClassEnrollmentRecordRepository>();//
            mockProgressTrackerRepository = new Mock<IProgressTrackerRepository>(); //
            mockQuizQuestionRepository = new Mock<IQuizQuestionRepository>();//
            mockQuizRepository = new Mock<IQuizRepository>(); // 
            mockResourceRepository = new Mock<IResourceRepository>(); //
            mockUserAnswerRepository = new Mock<IUserAnswerRepository>();

            //add mock repo access to unit of work 

            mockUnitOfWork.Setup(l => l.CourseRepository).Returns(mockCourseRepository.Object).Verifiable("Mock Course Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.LMSUserRepository).Returns(mockLMSUserRepository.Object).Verifiable("Mock LMSUser Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.CourseClassRepository).Returns(mockCourseClassRepository.Object).Verifiable("Mock CourseClass Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.ChapterRepository).Returns(mockChapterRepository.Object).Verifiable("Mock Chapter Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.ClassEnrollmentRecordRepository).Returns(mockClassEnrollmentRecordRepository.Object).Verifiable("Mock ClassEnrollmentRecord Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.ProgressTrackerRepository).Returns(mockProgressTrackerRepository.Object).Verifiable("Mock ProgressTracker Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.QuizQuestionRepository).Returns(mockQuizQuestionRepository.Object).Verifiable("Mock QuizQuestion Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.QuizRepository).Returns(mockQuizRepository.Object).Verifiable("Mock QuizQuestion Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.ResourceRepository).Returns(mockResourceRepository.Object).Verifiable("Mock Resource Repository is NOT returned");
            mockUnitOfWork.Setup(l => l.UserAnswerRepository).Returns(mockUserAnswerRepository.Object).Verifiable("Mock UserAnswer Repository is NOT returned");


            //set up save changes savechanges returns an integer 
            mockUnitOfWork.Setup(l => l.CompleteAsync()).ReturnsAsync(1); 

        }












    }
}
