using Xunit;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPM_Project.EntityModels.Tests
{
    public class CourseTests
    {
        private Course _testCoursePrequisities;
        private Course _testUserCourse;


        private Course CreateTestCourse()
        {
            var course = new Course()
            {
                Name = "Course 1",
                Description = "I love SPM",
                PreRequisites = null,
                PassingPercentage = (decimal)0.85,
            };

            return course;

        }

        private Course CreateTestCourse2()
        {
            var course = new Course()
            {
                Name = "Course 2",
                Description = "I love SPM",
                PreRequisites = null,
                PassingPercentage = (decimal)0.85,
            };

            return course;

        }

        //setup--------------------------------------------------
        public CourseTests()
        {
            _testCoursePrequisities = CreateTestCourse();
            _testUserCourse = CreateTestCourse2();

        }

        //setup--------------------------------------------------


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            //_controller = null;
            _testCoursePrequisities = null;
            _testUserCourse = null;

        }

        [Fact()]
        public void CourseTest()
        {
            //set course 2 into course 1 prerequisites
            _testCoursePrequisities.PreRequisites = new List<Course>(){ _testUserCourse };
            //User has the prequisites of course 2
            Assert.Equal(_testCoursePrequisities.PreRequisites, new List<Course>() { _testUserCourse });

            //User does not have the prequisites of course 2
            Assert.NotEqual(_testCoursePrequisities.PreRequisites, new List<Course>() { _testCoursePrequisities });

        }

    }
}