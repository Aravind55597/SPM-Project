using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
using SPM_Project.EntityModels;
using Moq;
using SPM_Project.CustomExceptions;
using System.Linq.Expressions;
using SPM_Project.DTOs;
using System.Text.Json;

namespace SPM_Project.ApiControllers.Tests
{

    //TEST CLASS AUTHOR : ESTHER TEO 01400174
    //https://www.meziantou.net/quick-introduction-to-xunitdotnet.htm
    public class ChaptersControllerTests:IDisposable
    {

        private ChaptersController _controller;
        private UOWMocker _uowMocker;





        //returns a list of chapters
        private List<Chapter> TestChaptersList()
        {
            List<Chapter> courseClassesList = new List<Chapter>();

            for (int i = 1; i < 4; i++)
            {
                if (1!=2)
                {
                    courseClassesList.Add(TestChapterCreator(i));
                }
      
            }

            return courseClassesList;
        }


        //returns a list of chapterdtos
        private List<ChapterDTO> TestChapterDTOsList()
        {
            List<ChapterDTO> courseClassesList = new List<ChapterDTO>();

            for (int i = 1; i <4; i++)
            {
                if (1 != 2)
                {
                    courseClassesList.Add(TestChapterDTOCreator(i));
                }
            }

            return courseClassesList;
        }



        //createa test course class with random integer for tests 
        private Chapter TestChapterCreator(int chapterId)
        {

            var chapter = new Chapter()
            {
                Name = $"Chapter {chapterId}",
                Description = "Chapter Description",
                CourseClass= TestCourseClass()
            };

            //set id of course 
            typeof(Chapter).GetProperty(nameof(chapter.Id)).SetValue(chapter, chapterId);

            return chapter;

        }


        private ChapterDTO TestChapterDTOCreator(int chapterId)
        {
            return new ChapterDTO()
            {
                Id= chapterId,
                Name= $"Chapter {chapterId}",
                Description = "Chapter Description",
                CourseClassId=1
            }; 
        }


        //sample TestCourseClass 1
        private CourseClass TestCourseClass()
        {
            var courseClass = new CourseClass()
            {

                Name = $"Test Course Class {1}",
                StartRegistration = DateTime.Parse("2021-10-28 12:29:06.900"),
                EndRegistration = DateTime.Parse("2021-10-28 12:29:06.900"),
                StartClass = DateTime.Parse("2021-10-28 12:29:06.900"),
                EndClass = DateTime.Parse("2021-10-28 12:29:06.900"),
                ClassTrainer = new LMSUser()
                {
                    Name = $"Test Trainer {1}",
                    Department = Department.Human_Resource,
                    DOB = DateTime.Parse("2021-10-28 12:29:06.900"),

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




        public ChaptersControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new ChaptersController(_uowMocker.mockUnitOfWork.Object);


            //SET UP RETRIEVAL OF CHAPTER

            //chapter exists -> return chapter
            _uowMocker.mockChapterRepository
               .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
               .ReturnsAsync(TestChapterCreator(1)).Verifiable("When chapter exists : GetIdByAsync is not called");

            //chapter does not exists -> return null
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetByIdAsync(2, It.IsAny<string>()))
                .ReturnsAsync((Chapter)null).Verifiable("When chapter does not exist : GetIdByAsync is not called");


            //SET UP RETRIEVAL OF COURSE CLASS

            //course class exists -> reuturn course class 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync(TestCourseClass()).Verifiable("When course class exists : GetIdByAsync is not called");

            //course class does not exists -> return null 
            _uowMocker.mockCourseClassRepository
                .Setup(l => l.GetByIdAsync(2, It.IsAny<string>()))
                .ReturnsAsync((CourseClass)null).Verifiable("When course class does not exist : GetIdByAsync is not called");


        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }





        [Fact()]
        //Get single chapter 
        public async Task GetChapterAsyncTest()
        {


            //WHEN CHAPTER EXISTS---------------------------------------------------------------------------------------------------------------------------------------- 

            var result = await _controller.GetChapterAsync(1,"");
            //check getidbyasync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u=>u.ChapterRepository);
            //check that the type of the result is Chapter
            Assert.IsType<Chapter>(result); 
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(TestChapterCreator(1)), JsonSerializer.Serialize(result));

            //WHEN CHAPTER DOES NOT EXIST---------------------------------------------------------------------------------------------------------------------------------------- 

            Func<Task> action = (async () => await _controller.GetChapterAsync(2, ""));
            //check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);
            //check getidbyasync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetByIdAsync(2, It.IsAny<string>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);

       

        }

        [Fact()]
        //blackbox
        public async Task GetChapterDTOAsyncTest()
        {
            //WHEN CHAPTER EXISTS---------------------------------------------------------------------------------------------------------------------------------------- 
            var result = await _controller.GetChapterDTOAsync(1, "");
            Assert.Equal(JsonSerializer.Serialize(TestChapterDTOCreator(1)), JsonSerializer.Serialize(result));


            //WHEN CHAPTER DOES NOT EXIST---------------------------------------------------------------------------------------------------------------------------------------- 

            Func<Task> action = (async () => await _controller.GetChapterDTOAsync(2, ""));
            //check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);


        }



        [Fact()]
        //get multiple chapters 
        public async Task GetChaptersAsyncTest_CourseClassIdPassed()
        {



            //COURSECLASE EXISTS -> LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 


            //List of chapters returned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestChaptersList()).Verifiable("Chapters exist : GetAllAsync() is not called to retreive a list of chapters");

            var result = await _controller.GetChaptersAsync(1);

            //check GetAllAsync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
            //check that the type of the result is Chapter list
            Assert.IsType<List<Chapter>>(result);
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(TestChaptersList()), JsonSerializer.Serialize(result));


            //COURSECLASE EXISTS -> EMPTY LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 

            //Empty chapter list is retunrned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Chapter>()).Verifiable("Chapters don't exist : GetAllAsync() is not called to retreive empty chapter list");


            result = await _controller.GetChaptersAsync(1); 

            //check GetAllAsync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
            //check that the type of the result is Chapter list
            Assert.IsType<List<Chapter>>(result);
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(new List<Chapter>()), JsonSerializer.Serialize(result));


            //COURSECLASS DOES NOT EXIST---------------------------------------------------------------------------------------------------------------------------------------- 


            
            Func<Task> action = (async () => await _controller.GetChaptersAsync(2));
            //check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);
            //check getidbyasync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);


        }




        [Fact()]
        //get multiple chapters 
        public async Task GetChaptersAsyncTest_CourseClassIdNotPassed()
        {

            //LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 


            //List of chapters returned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestChaptersList()).Verifiable("Chapters exist : GetAllAsync() is not called to retreive a list of chapters");

            var result = await _controller.GetChaptersAsync(null);

            //check GetAllAsync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
            //check that the type of the result is Chapter list
            Assert.IsType<List<Chapter>>(result);
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(TestChaptersList()), JsonSerializer.Serialize(result));


            //EMPTY LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 


            //Empty chapter list is retunrned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Chapter>()).Verifiable("Chapters don't exist : GetAllAsync() is not called to retreive empty chapter list");


            result = await _controller.GetChaptersAsync(null);

            //check GetAllAsync is called 
            _uowMocker.mockChapterRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that chapter repository is accessed 
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
            //check that the type of the result is Chapter list
            Assert.IsType<List<Chapter>>(result);
            //check that the result returned is the same as the test Chapter
            Assert.Equal(JsonSerializer.Serialize(new List<Chapter>()), JsonSerializer.Serialize(result));


        }



        //get multiple chapters  async
        [Fact()]
        public async Task GetChapterDTOsAsyncTest_CourseClassIdPassed()
        {

            //COURSECLASE EXISTS -> LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 


            //List of chapters returned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestChaptersList()).Verifiable("Chapters exist : GetAllAsync() is not called to retreive a list of chapters");

            var result = await _controller.GetChapterDTOsAsync(1);


            Assert.Equal(JsonSerializer.Serialize(TestChapterDTOsList()), JsonSerializer.Serialize(result));


            //COURSECLASE EXISTS -> EMPTY LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 

            //Empty chapter list is retunrned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Chapter>()).Verifiable("Chapters don't exist : GetAllAsync() is not called to retreive empty chapter list");

            result = await _controller.GetChapterDTOsAsync(1);

            Assert.Equal(JsonSerializer.Serialize(new List<ChapterDTO>()), JsonSerializer.Serialize(result));

            //COURSECLASS DOES NOT EXIST---------------------------------------------------------------------------------------------------------------------------------------- 

            Func<Task> action = (async () => await _controller.GetChapterDTOsAsync(2));
            //check that not found is returned 
            await Assert.ThrowsAsync<NotFoundException>(action);



        }


        //get multiple chapters  async
        [Fact()]
        public async Task GetChapterDTOsAsyncTest_CourseClassIdNotPassed()
        {
            //LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 


            //List of chapters returned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestChaptersList()).Verifiable("Chapters exist : GetAllAsync() is not called to retreive a list of chapters");


            var result = await _controller.GetChapterDTOsAsync(1);


            Assert.Equal(JsonSerializer.Serialize(TestChapterDTOsList()), JsonSerializer.Serialize(result));


            //EMPTY LIST OF CHAPTERS RETURNED---------------------------------------------------------------------------------------------------------------------------------------- 


            //Empty chapter list is retunrned 
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Chapter, bool>>>(), It.IsAny<Func<IQueryable<Chapter>, IOrderedQueryable<Chapter>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Chapter>()).Verifiable("Chapters don't exist : GetAllAsync() is not called to retreive empty chapter list");


            result = await _controller.GetChapterDTOsAsync(1);

            Assert.Equal(JsonSerializer.Serialize(new List<ChapterDTO>()), JsonSerializer.Serialize(result));

        }





    }
}