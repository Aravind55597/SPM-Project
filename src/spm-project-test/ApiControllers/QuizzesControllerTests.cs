using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels;

namespace SPM_Project.ApiControllers.Tests
{
    public class QuizzesControllerTests
    {





        private QuizzesController _controller;
        private UOWMocker _uowMocker;
        private DTParameterModel _inputDTModel;
        private DTResponse<ClassEnrollmentRecordTableData> _outputDTModel;

        public QuizzesControllerTests()
        {
            //  Assert.True(false, "This test needs an implementation");

            _uowMocker = new UOWMocker();
            _controller = new QuizzesController(_uowMocker.mockUnitOfWork.Object);
            _inputDTModel = new DTParameterModel();
            _outputDTModel = new DTResponse<ClassEnrollmentRecordTableData>();

        }

        public void Dispose()
        {
            _uowMocker = null;
            _controller = null;
        }












        [Fact()]
        public void PostQuizDTOAPIAsyncTest()
        {
            Assert.True(false, "This test needs an implementation");
        }














    }
}