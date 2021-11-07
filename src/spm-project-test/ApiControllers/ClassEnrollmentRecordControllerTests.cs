<<<<<<< Updated upstream
﻿using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
=======
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
>>>>>>> Stashed changes

namespace SPM_Project.ApiControllers.Tests
{
    public class ClassEnrollmentRecordControllerTests:IDisposable
    {

        private ClassEnrollmentRecordController _controller;
        private UOWMocker _uowMocker;


        public  ClassEnrollmentRecordControllerTests()
        {
          //  Assert.True(false, "This test needs an implementation");

            _uowMocker = new UOWMocker();
            _controller = new ClassEnrollmentRecordController(_uowMocker.mockUnitOfWork.Object);
            //_inputDTModel = new DTParameterModel();
            //_outputDTModel = new DTResponse<CourseClassTableData>();

            _uowMocker.mockUnitOfWork.Setup(u => u.CompleteAsync()).Verifiable("CompleteAsync is NOT called"); 

            _uowMocker.mockUnitOfWork.Setup(l => l.CourseRepository).Returns(_uowMocker.mockCourseRepository.Object).Verifiable("Mock Course Repository is NOT returned");
            _uowMocker.mockUnitOfWork.Setup(l => l.CourseClassRepository).Returns(_uowMocker.mockCourseClassRepository.Object).Verifiable("Mock CourseClass Repository is NOT returned");
            _uowMocker.mockUnitOfWork.Setup(l => l.LMSUserRepository).Returns(_uowMocker.mockLMSUserRepository.Object).Verifiable("Mock LMSUser Repository is NOT returned");



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
        public async Task AddEnrollmentRecordTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

<<<<<<< Updated upstream
        [Fact()]
        public void AddEnrollmentRecordTest1()
        {
            Assert.True(false, "This test needs an implementation");
        }
=======
        //APPROVE 



        //DECLINE



       





>>>>>>> Stashed changes
    }
}