using Newtonsoft.Json;
using SPM_Project.EntityModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace SPM_Project.DTOs.Tests
{
    public class ChapterDTOTests: IDisposable
    {
        private ChapterDTO _cDTO_1;

        private ChapterDTO _cDTO_2;

        private Chapter _c_1;

        private Chapter _c_2;

        //setup--------------------------------------------------------------------------------------------------------------------------------------------------



        public ChapterDTOTests()
        {
            //CREATE TEST COURSE CLASS
            var testCourseClass = new CourseClass()
            {

            };
            typeof(CourseClass).GetProperty(nameof(testCourseClass.Id)).SetValue(testCourseClass, 1);


            //CREATE LIST IF TEST QUIZZES & RESOURCES 
            List<Quiz> chap1Quizzes = new List<Quiz>();

            for (int i = 1; i < 4; i++)
            {
                var quiz = new Quiz();
                typeof(Quiz).GetProperty(nameof(quiz.Id)).SetValue(quiz, i);
                chap1Quizzes.Add(quiz);
            }


            List<Resource> chap1Resources = new List<Resource>();
            for (int i = 1; i < 4; i++)
            {
                var resource = new Resource();
                typeof(Resource).GetProperty(nameof(resource.Id)).SetValue(resource, i);
                chap1Resources.Add(resource);
            }



            List<Quiz> chap2Quizzes = new List<Quiz>();

            for (int i = 4; i < 7; i++)
            {
                var quiz = new Quiz();
                typeof(Quiz).GetProperty(nameof(quiz.Id)).SetValue(quiz, i);
                chap2Quizzes.Add(quiz);
            }

            List<Resource> chap2Resources = new List<Resource>();
            for (int i = 4; i < 7; i++)
            {
                var resource = new Resource();
                typeof(Resource).GetProperty(nameof(resource.Id)).SetValue(resource, i);
                chap2Resources.Add(resource);
            }



            //TEST CHAPTER DTO --------------------------------------------------------------------
            _cDTO_1 = new ChapterDTO()
            {
                Name = "Chapter1",
                Description = "Chapter1 Description",
                ResourceIds=new List<int>()
                {
                    1,2,3
                },
                QuizIds=new List<int>()
                {
                    1,2,3
                },
                CourseClassId=1

            };
            typeof(ChapterDTO).GetProperty(nameof(_cDTO_1.Id)).SetValue(_cDTO_1, 1);

            _cDTO_2 = new ChapterDTO()
            {
                Name = "Chapter2",
                Description = "Chapter2 Description",
                ResourceIds = new List<int>()
                {
                    4,5,6
                },
                QuizIds = new List<int>()
                {
                    4,5,6
                },
                CourseClassId = 1

            };
            typeof(ChapterDTO).GetProperty(nameof(_cDTO_2.Id)).SetValue(_cDTO_2, 2);


            //TEST CHAPTER-------------------------------------------------------------------------
            _c_1 = new Chapter()
            {
                Name = "Chapter1",
                Description = "Chapter1 Description",
                CourseClass = testCourseClass,
                Quizzes = chap1Quizzes,
                Resources=chap1Resources


            };

            //set id for chapter
            typeof(Chapter).GetProperty(nameof(_c_1.Id)).SetValue(_c_1, 1);



            _c_2 = new Chapter()
            {
                Name = "Chapter2",
                Description = "Chapter2 Description",
                CourseClass = testCourseClass,
                Quizzes = chap2Quizzes,
                Resources = chap2Resources
            };

            //set id for chapter
            typeof(Chapter).GetProperty(nameof(_c_2.Id)).SetValue(_c_2, 2);




        }


        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _cDTO_1 = null;

            _cDTO_2 = null;

            _c_1 = null;
            _c_2 = null;
        }



        [Fact()]
        public void ChapterDTOTest_CheckObject()
        {
            Assert.NotEqual(_cDTO_1, _cDTO_2);
            Assert.NotEqual(_c_1, _c_2);
        }



        [Fact()]
        public void ChapterDTOTest_ConvertDomainToDTO()
        {


            //WHEN RESOURCES QUIZZES COURSECLASS ARE PASSED TO CHAPTER ENTITY------------------------------------------------------------------ 

            //expected dto & generated dto are equal (object converted to json as obejcts are technically different in memory)
            Assert.Equal(JsonConvert.SerializeObject(_cDTO_1), JsonConvert.SerializeObject(new ChapterDTO(_c_1)));
            Assert.Equal(JsonConvert.SerializeObject(_cDTO_2), JsonConvert.SerializeObject(new ChapterDTO(_c_2)));


            //check the resource IDs of the generated DTO is the same as expected
            Assert.Equal(new List<int>() { 1,2,3}, new ChapterDTO(_c_1).ResourceIds);
            Assert.Equal(new List<int>() { 4,5,6 }, new ChapterDTO(_c_2).ResourceIds);


            //check the chapter IDs of the generated DTO is the same as expected
            Assert.Equal(new List<int>() { 1, 2, 3 }, new ChapterDTO(_c_1).QuizIds);
            Assert.Equal(new List<int>() { 4, 5, 6 }, new ChapterDTO(_c_2).QuizIds);



            //WHEN RESOURCES NOT PASSED TO CHAPTER ENTITY------------------------------------------------------------------------------------ 
            _c_1.Resources = null;
            _c_2.Resources = null;

            //chapter dto resource list must be null

            Assert.Null(new ChapterDTO(_c_1).ResourceIds);
            Assert.Null(new ChapterDTO(_c_2).ResourceIds);


            //WHEN QUIZZES ARE NOT PASSED TO CHAPTER ENTITY ----------------------------------------------------------------------------------------
            _c_1.Quizzes = null;
            _c_2.Quizzes = null;

            //chapter dto quiz list must be null
            Assert.Null(new ChapterDTO(_c_1).QuizIds);
            Assert.Null(new ChapterDTO(_c_2).QuizIds);



            //WHEN COURSE CLASS ARE NOT PASSED TO CHAPTER ENTITY 
            _c_1.CourseClass = null;
            _c_2.CourseClass = null;

            //chapter dto course Id must be null
            Assert.Null(new ChapterDTO(_c_1).CourseClassId);
            Assert.Null(new ChapterDTO(_c_2).CourseClassId);


       

        }


    }
}