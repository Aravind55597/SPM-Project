using System;
using System.Collections.Generic;
using Xunit;

namespace SPM_Project.EntityModels.Tests
{
    public class ChapterTests : IDisposable
    {
        //PREFACE : NAVIGATION PROPERTIES ARE MINIMALLY TESTED DUE TO DIFFICULTY OF CREATING LARGE OBJECTS
        //TEST CLASS AUTHOR : ESTHER TEO 01400174


        private Chapter _chap1;

        private Chapter _chap2;

        //setup------------------------------------------------------------------

        public ChapterTests()
        {
            _chap1 = new Chapter()
            {
                Name = $"Chapter 1",
                Description = "Chapter 1 Description",
                Quizzes = new List<Quiz>()
                {
                    new Quiz(),
                    new Quiz(),
                    new Quiz(),
                },
                Resources = new List<Resource>()
                {
                    new Resource(),
                    new Resource(),
                    new Resource(),
                }
            };

            typeof(Chapter).GetProperty(nameof(_chap1.Id)).SetValue(_chap1, 1);

            _chap2 = new Chapter()
            {
                Name = $"Chapter 2",
                Description = "Chapter 2 Description",
                Quizzes = new List<Quiz>()
                {
                    new Quiz(),
                    new Quiz(),
                },
                Resources = new List<Resource>()
                {
                    new Resource(),
                    new Resource(),
                }
            };

            typeof(Chapter).GetProperty(nameof(_chap2.Id)).SetValue(_chap2, 2);
        }

        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _chap1 = null;

            _chap2 = null;

        }

        [Fact()]
        public void ChapterTest()
        {
            Assert.NotEqual(_chap1,_chap2);

            Assert.Equal(1, _chap1.Id);
            Assert.Equal("Chapter 1", _chap1.Name);
            Assert.Equal("Chapter 1 Description", _chap1.Description);

            Assert.Equal(2, _chap2.Id);
            Assert.Equal("Chapter 2", _chap2.Name);
            Assert.Equal("Chapter 2 Description", _chap2.Description);
        }

        [Fact()]
        public void NumberOfQuizzesTest()
        {


            //QUIZZES EXIST
            Assert.Equal(3, _chap1.NumberOfQuizzes());
            Assert.Equal(2, _chap2.NumberOfQuizzes());


            //QUIZZES DON'T EXIST 
            _chap1.Quizzes = null;
            _chap2.Quizzes = null;
            Assert.Equal(0, _chap1.NumberOfQuizzes());
            Assert.Equal(0, _chap2.NumberOfQuizzes());


        }

        [Fact()]
        public void NumberOfResourcesTest()
        {
            //RESOURCES EXIST
            Assert.Equal(3, _chap1.NumberOfResources());
            Assert.Equal(2, _chap2.NumberOfResources());

            //RESOURCES DON'T EXIST
            _chap1.Resources = null;
            _chap2.Resources = null;
            Assert.Equal(0, _chap1.NumberOfResources());
            Assert.Equal(0, _chap2.NumberOfResources());

        }
    }
}