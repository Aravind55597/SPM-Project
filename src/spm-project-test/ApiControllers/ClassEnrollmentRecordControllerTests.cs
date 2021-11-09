using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_ProjectTests.Mocks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SPM_Project.ApiControllers.Tests
{

    //TEST CLASS AUTHOR : THEN EE KI 01329299
    //https://www.meziantou.net/quick-introduction-to-xunitdotnet.htm
    public class ClassEnrollmentRecordControllerTests : IDisposable
    {

        private ClassEnrollmentRecordController _controller;
        private UOWMocker _uowMocker;
        private DTParameterModel _inputDTModel;
        private DTResponse<ClassEnrollmentRecordTableData> _outputDTModel;

        public ClassEnrollmentRecordControllerTests()
        {
            //  Assert.True(false, "This test needs an implementation");

            _uowMocker = new UOWMocker();
            _controller = new ClassEnrollmentRecordController(_uowMocker.mockUnitOfWork.Object);
            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<ClassEnrollmentRecordTableData>();

        }

        //createa test course class with random integer for tests 
        private CourseClass TestCourseClassCreator()
        {

            Random rnd = new Random();
            int id = rnd.Next(1, 50);

            var courseClass = new CourseClass()
            {


                Name = $"Test Course Class {id}",
                StartRegistration = DateTime.Now,
                EndRegistration = DateTime.Now,
                StartClass = DateTime.Now,
                EndClass = DateTime.Now,
                ClassTrainer = new LMSUser()
                {
                    Name = $"Test Trainer {id}",
                    Department = Department.Human_Resource,
                    DOB = DateTime.Now,

                },
                Course = new Course()
                {
                    Name = $"Test Course {1}",
                    Description = "Test Description",
                    PassingPercentage = (decimal)0.85
                },
                Slots = 30

            };

            //set id of the courseClass 
            typeof(CourseClass).GetProperty(nameof(courseClass.Id)).SetValue(courseClass, id);

            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(courseClass.ClassTrainer.Id)).SetValue(courseClass.ClassTrainer, id);

            //set id of course 
            typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, 1);

            return courseClass;

        }


        //sample test course class 
        private CourseClass TestCourseClass()
        {


            var courseClass = new CourseClass()
            {


                Name = $"Test Course Class {1}",
                StartRegistration = DateTime.Now,
                EndRegistration = DateTime.Now,
                StartClass = DateTime.Now,
                EndClass = DateTime.Now,
                ClassTrainer = new LMSUser()
                {
                    Name = $"Test Trainer {1}",
                    Department = Department.Human_Resource,
                    DOB = DateTime.Now,

                },
                Course = new Course
                {
                    Name = $"Test Course {1}",
                    Description = "Test Description",
                    PassingPercentage = (decimal)0.85
                },
                Slots = 30

            };

            //set id of the courseClass 
            typeof(CourseClass).GetProperty(nameof(courseClass.Id)).SetValue(courseClass, 1);

            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(courseClass.ClassTrainer.Id)).SetValue(courseClass.ClassTrainer, 1);

            //set id of course 
            typeof(Course).GetProperty(nameof(courseClass.Course.Id)).SetValue(courseClass.Course, 1);

            return courseClass;

        }



        //returns a list of ClassEnrollmentRecord 
        private List<ClassEnrollmentRecord> TestClassEnrollmentRecordList()
        {
            List<ClassEnrollmentRecord> ClassEnrollmentRecordList = new List<ClassEnrollmentRecord>();

            for (int i = 0; i < 10; i++)
            {
                ClassEnrollmentRecordList.Add(TestClassEnrollmentRecordCreator());
            }

            return ClassEnrollmentRecordList;
        }
        //creates test classenrollmentrecord
        private ClassEnrollmentRecord TestClassEnrollmentRecordCreator()
        {

            Random rnd = new Random();
            int id = rnd.Next(1, 50);

            var classEnrollmentRecord = new ClassEnrollmentRecord()
            {

                IsAssigned = true,
                IsEnrollled = true,
                LMSUser = new LMSUser(),
                CourseClass = TestCourseClass()
            };

            typeof(ClassEnrollmentRecord).GetProperty(nameof(classEnrollmentRecord.Id)).SetValue(classEnrollmentRecord, 1);

            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(classEnrollmentRecord.LMSUser.Id)).SetValue(classEnrollmentRecord.LMSUser, 1);

          

            return classEnrollmentRecord;
        }

        //creates test learner
        private LMSUser TestLearnerCreator()
        {

            Random rnd = new Random();
            int id = rnd.Next(1, 50);

            var testlearner = new LMSUser()
            {
                Name = "TestLearner",
                Enrollments = new List<ClassEnrollmentRecord>(),
    
                
            };


            testlearner.Enrollments.Add(TestClassEnrollmentRecordCreator());
            //set id of classtrainer 
            typeof(LMSUser).GetProperty(nameof(testlearner.Id)).SetValue(testlearner, 1);

          

            return testlearner;
        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }



        [Fact()]
        public async Task GetClassEnrollmentRecordsDataTableTest_EnrollmentRecordsExist_ReturnDTResponseObject()
        {

            //setup 
            _uowMocker.mockClassEnrollmentRecordRepository.Setup(ce => ce.GetClassEnrollmentRecordsDataTable(_inputDTModel)).ReturnsAsync(_outputDTModel).Verifiable("Retreiving enrollment records was not attempted");

            //ACT----------------------------------------------------------------------------------------------------------------------------------------------------

            var result = await _controller.GetClassEnrollmentRecordsDataTable(_inputDTModel) as OkObjectResult;

            //ASSERT---------------------------------------------------------------------------------------------------------------------------------------------------

            //verify that respository is retreived 
            _uowMocker.mockUnitOfWork.Verify(l => l.ClassEnrollmentRecordRepository);

            //verify that repository functionw as called 
            _uowMocker.mockClassEnrollmentRecordRepository.Verify(ce => ce.GetClassEnrollmentRecordsDataTable(_inputDTModel));

            //check if ok is returned 
            Assert.IsType<OkObjectResult>(result);
            //check that a json string is passed to the front end 
            var items = Assert.IsType<string>(result.Value);
            //check if DTResponse object is send to front end 
            var deserializedMessage = JsonConvert.DeserializeObject<DTResponse<ClassEnrollmentRecordTableData>>(items);
            Assert.IsType<DTResponse<ClassEnrollmentRecordTableData>>(deserializedMessage);


        }

        //APPROVE 

        [Fact]
        public async Task ApproveLearnerEnrolment_Test()
        {
            //setup courseclass id 
            var __testcc = TestCourseClass();
            _uowMocker.mockCourseClassRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(__testcc).Verifiable("GetByIdAsync LMSUser was not called");
           

            _uowMocker.mockClassEnrollmentRecordRepository
            .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<ClassEnrollmentRecord, bool>>>(), It.IsAny<Func<IQueryable<ClassEnrollmentRecord>, IOrderedQueryable<ClassEnrollmentRecord>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(TestClassEnrollmentRecordList()).Verifiable("ClassEnrollmentRecord were not retrieved");


            //setup learner id 
         
            _uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(TestLearnerCreator()).Verifiable("GetByIdAsync LMSUser was not called");

            // call function 
            var result = await _controller.ApproveEnrollment(1, __testcc.Id);

            //assert as true if learner's isEnrolled is true
            Debug.WriteLine(result);
            Assert.NotNull(result);

        }


        //DECLINE

        [Fact()]
        public async Task DeclineLearnerEnrolment_Test()
        {


            //setup courseclass id 
            var __testcc = TestCourseClass();
            _uowMocker.mockCourseClassRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(__testcc).Verifiable("GetByIdAsync LMSUser was not called");


            _uowMocker.mockClassEnrollmentRecordRepository
            .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<ClassEnrollmentRecord, bool>>>(), It.IsAny<Func<IQueryable<ClassEnrollmentRecord>, IOrderedQueryable<ClassEnrollmentRecord>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(TestClassEnrollmentRecordList()).Verifiable("ClassEnrollmentRecord were not retrieved");


            //setup learner id 

            _uowMocker.mockLMSUserRepository.Setup(l => l.GetByIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(TestLearnerCreator()).Verifiable("GetByIdAsync LMSUser was not called");

            // call function 
            var result = await _controller.DeclineEnrollment(1, __testcc.Id);

            //assert as true if learner's isEnrolled is true
            Debug.WriteLine(result);
            Assert.NotNull(result);

        }







    }
}
