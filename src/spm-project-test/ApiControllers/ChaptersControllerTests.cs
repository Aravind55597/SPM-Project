using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;

namespace SPM_Project.ApiControllers.Tests
{
    public class ChaptersControllerTests:IDisposable
    {

        private ChaptersController _controller;
        private UOWMocker _uowMocker;





        public ChaptersControllerTests()
        {
            _uowMocker = new UOWMocker();
            _controller = new ChaptersController(_uowMocker.mockUnitOfWork.Object);

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
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        //Get single chapter DTO
        public async Task GetChapterDTOAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        //get multiple chapters 
        public async Task GetChaptersAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        //get multiple chapters  async
        [Fact()]
        public async Task GetChapterDTOsAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }





    }
}