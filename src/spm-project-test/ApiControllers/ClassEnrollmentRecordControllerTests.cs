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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SPM_Project.ApiControllers.Tests
{


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



        //DECLINE



       





    }
}
