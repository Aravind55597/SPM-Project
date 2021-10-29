using Moq;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs;
using SPM_Project.EntityModels;
using SPM_ProjectTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SPM_Project.ApiControllers.Tests
{
    public class ResourcesControllerTests
    {
        private ResourcesController _controller;
        private UOWMocker _uowMocker;

        //returns a list of chapters
        private List<Resource> TestResourcesList()
        {
            List<Resource> resourcesList = new List<Resource>();

            for (int i = 1; i < 4; i++)
            {
                if (1 != 2)
                {
                    resourcesList.Add(TestResourceCreator(i));
                }
            }

            return resourcesList;
        }

        //returns a list of chapterdtos
        private List<ResourceDTO> TestResourceDTOsList()
        {
            List<ResourceDTO> resourcesList = new List<ResourceDTO>();

            for (int i = 1; i < 4; i++)
            {
                if (1 != 2)
                {
                    resourcesList.Add(TestResourceDTOCreator(i));
                }
            }

            return resourcesList;
        }

        //createa test resource with random integer for tests
        private Resource TestResourceCreator(int resourceId)
        {
            var resource = new Resource()
            {
                ContentUrl = $"Resource {resourceId}",
                DownloadableContentUrl = "Resource Description",
                Chapter = TestChapter(),
                Content = ContentType.PDF
            };

            //set id of course
            typeof(Resource).GetProperty(nameof(resource.Id)).SetValue(resource, resourceId);

            return resource;
        }

        private ResourceDTO TestResourceDTOCreator(int resourceId)
        {
            var resourceDTO = new ResourceDTO()
            {

                ContentUrl = $"Resource {resourceId}",
                DownloadableContentUrl = "Resource Description",
                ChapterId = 1,
                Content = ContentType.PDF.ToString()
            };

            //set id of course
            typeof(ResourceDTO).GetProperty(nameof(resourceDTO.Id)).SetValue(resourceDTO, resourceId);


            return resourceDTO;
        }

        //sample TestChapter 1
        private Chapter TestChapter()
        {

            var chapter = new Chapter()
            {
                Name = $"Chapter {1}",
                Description = "Chapter Description"
            };

            //set id of course 
            typeof(Chapter).GetProperty(nameof(chapter.Id)).SetValue(chapter, 1);

            return chapter;
        }

        public ResourcesControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new ResourcesController(_uowMocker.mockUnitOfWork.Object);

            //SET UP RETRIEVAL OF CHAPTER

            //resource exists -> return resource
            _uowMocker.mockResourceRepository
               .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
               .ReturnsAsync(TestResourceCreator(1)).Verifiable("When resource exists : GetIdByAsync is not called");

            //resource does not exists -> return null
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetByIdAsync(2, It.IsAny<string>()))
                .ReturnsAsync((Resource)null).Verifiable("When resource does not exist : GetIdByAsync is not called");

            //SET UP RETRIEVAL OF COURSE CLASS

            //resource exists -> reuturn resource
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetByIdAsync(1, It.IsAny<string>()))
                .ReturnsAsync(TestChapter()).Verifiable("When resource exists : GetIdByAsync is not called");

            //resource does not exists -> return null
            _uowMocker.mockChapterRepository
                .Setup(l => l.GetByIdAsync(2, It.IsAny<string>()))
                .ReturnsAsync((Chapter)null).Verifiable("When resource does not exist : GetIdByAsync is not called");
        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }

        [Fact()]
        //Get single resource
        public async Task GetResourceAsyncTest()
        {
            //WHEN CHAPTER EXISTS----------------------------------------------------------------------------------------------------------------------------------------

            var result = await _controller.GetResourceAsync(1, "");
            //check getidbyasync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetByIdAsync(1, It.IsAny<string>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ResourceRepository);
            //check that the type of the result is Resource
            Assert.IsType<Resource>(result);
            //check that the result returned is the same as the test Resource
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestResourceCreator(1)), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //WHEN CHAPTER DOES NOT EXIST----------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetResourceAsync(2, ""));
            //check that not found is returned
            await Assert.ThrowsAsync<NotFoundException>(action);
            //check getidbyasync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetByIdAsync(2, It.IsAny<string>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ResourceRepository);
        }

        [Fact()]
        //blackbox
        public async Task GetResourceDTOAsyncTest()
        {
            //WHEN RESOURCE EXISTS----------------------------------------------------------------------------------------------------------------------------------------
            var result = await _controller.GetResourceDTOAsync(1, "");
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestResourceDTOCreator(1)), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //WHEN RESOURCE DOES NOT EXIST----------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetResourceDTOAsync(2, ""));
            //check that not found is returned
            await Assert.ThrowsAsync<NotFoundException>(action);
        }

        [Fact()]
        //get multiple chapters
        public async Task GetResourcesAsyncTest_ChapterIdPassed()
        {
            //CHAPTER EXISTS -> LIST OF RESOURCES RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //List of resources returned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestResourcesList()).Verifiable("Resources exist : GetAllAsync() is not called to retreive a list of chapters");

            var result = await _controller.GetResourcesAsync(1);

            //check GetAllAsync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
            //check that the type of the result is Resource list
            Assert.IsType<List<Resource>>(result);
            //check that the result returned is the same as the test Resource
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestResourcesList()), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //CHAPTER EXISTS -> EMPTY LIST OF RESOURCES RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //Empty resource list is retunrned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Resource>()).Verifiable("Resources don't exist : GetAllAsync() is not called to retreive empty resource list");

            result = await _controller.GetResourcesAsync(1);

            //check GetAllAsync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
            //check that the type of the result is Resource list
            Assert.IsType<List<Resource>>(result);
            //check that the result returned is the same as the test Resource
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(new List<Resource>()), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //CHAPTER DOES NOT EXIST----------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetResourcesAsync(2));
            //check that not found is returned
            await Assert.ThrowsAsync<NotFoundException>(action);
            //check getidbyasync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ChapterRepository);
        }

        [Fact()]
        //get multiple chapters
        public async Task GetResourcesAsyncTest_ChapterIdNotPassed()
        {
            //LIST OF CHAPTERS RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //List of chapters returned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestResourcesList()).Verifiable("Resources exist : GetAllAsync() is not called to retreive a list of resources");

            var result = await _controller.GetResourcesAsync(null);

            //check GetAllAsync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ResourceRepository);
            //check that the type of the result is Resource list
            Assert.IsType<List<Resource>>(result);
            //check that the result returned is the same as the test Resource
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestResourcesList()), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //EMPTY LIST OF CHAPTERS RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //Empty resource list is retunrned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Resource>()).Verifiable("Resources don't exist : GetAllAsync() is not called to retreive empty resource list");

            result = await _controller.GetResourcesAsync(null);

            //check GetAllAsync is called
            _uowMocker.mockResourceRepository.Verify(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            //check that resource repository is accessed
            _uowMocker.mockUnitOfWork.Verify(u => u.ResourceRepository);
            //check that the type of the result is Resource list
            Assert.IsType<List<Resource>>(result);
            //check that the result returned is the same as the test Resource
            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(new List<Resource>()), Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        //get multiple chapters  async
        [Fact()]
        public async Task GetResourceDTOsAsyncTest_ChapterIdPassed()
        {
            //COURSECLASE EXISTS -> LIST OF CHAPTERS RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //List of chapters returned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestResourcesList()).Verifiable("Resources exist : GetAllAsync() is not called to retreive a list of chapters");

            var result = await _controller.GetResourceDTOsAsync(1);

            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestResourceDTOsList()), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //COURSECLASE EXISTS -> EMPTY LIST OF CHAPTERS RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //Empty resource list is retunrned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Resource>()).Verifiable("Resources don't exist : GetAllAsync() is not called to retreive empty resource list");

            result = await _controller.GetResourceDTOsAsync(1);

            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(new List<ResourceDTO>()), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //COURSECLASS DOES NOT EXIST----------------------------------------------------------------------------------------------------------------------------------------

            Func<Task> action = (async () => await _controller.GetResourceDTOsAsync(2));
            //check that not found is returned
            await Assert.ThrowsAsync<NotFoundException>(action);
        }

        //get multiple chapters  async
        [Fact()]
        public async Task GetResourceDTOsAsyncTest_ChapterIdNotPassed()
        {
            //LIST OF CHAPTERS RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //List of chapters returned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(TestResourcesList()).Verifiable("Resources exist : GetAllAsync() is not called to retreive a list of chapters");

            var result = await _controller.GetResourceDTOsAsync(1);

            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(TestResourceDTOsList()), Newtonsoft.Json.JsonConvert.SerializeObject(result));

            //EMPTY LIST OF CHAPTERS RETURNED----------------------------------------------------------------------------------------------------------------------------------------

            //Empty resource list is retunrned
            _uowMocker.mockResourceRepository
                .Setup(l => l.GetAllAsync(It.IsAny<Expression<Func<Resource, bool>>>(), It.IsAny<Func<IQueryable<Resource>, IOrderedQueryable<Resource>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Resource>()).Verifiable("Resources don't exist : GetAllAsync() is not called to retreive empty resource list");

            result = await _controller.GetResourceDTOsAsync(1);

            Assert.Equal(Newtonsoft.Json.JsonConvert.SerializeObject(new List<ResourceDTO>()), Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }
    }
}