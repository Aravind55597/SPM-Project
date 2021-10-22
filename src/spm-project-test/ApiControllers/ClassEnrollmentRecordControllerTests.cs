using Xunit;
using SPM_Project.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_ProjectTests.Mocks;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SPM_Project.ApiControllers.Tests
{
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
            ////check that the data inside the response object is a list of ClassEnrollmentRecordsTabelData 
            //Assert.IsType<List<ClassEnrollmentRecordTableData>>(deserializedMessage.Data);

        }







    }
}
