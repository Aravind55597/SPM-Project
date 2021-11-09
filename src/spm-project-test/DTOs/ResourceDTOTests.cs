using Xunit;
using SPM_Project.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPM_Project.EntityModels;
using Newtonsoft.Json;

namespace SPM_Project.DTOs.Tests
{
    //TEST CLASS AUTHOR : SHUM CHIN NING 01355819
    public class ResourceDTOTests:IDisposable
    {



        private ResourceDTO _rDTO_1;

        private ResourceDTO _rDTO_2;


        private Resource _r_1;

        private Resource _r_2;

        //setup--------------------------------------------------------------------------------------------------------------------------------------------------
        public ResourceDTOTests()
        {

            //TEST RESOURCE DTO 
            _rDTO_1 = new ResourceDTO()
            {
                ContentUrl="Resource1.com",
                DownloadableContentUrl= "Resource1Download.com",
                Content="PDF",
                ChapterId=1

            };
            typeof(ResourceDTO).GetProperty(nameof(_rDTO_1.Id)).SetValue(_rDTO_1, 1);

            _rDTO_2 = new ResourceDTO()
            {
                ContentUrl = "Resource2.com",
                DownloadableContentUrl = "Resource2Download.com",
                Content = "Excel",
                ChapterId = 2

            };
            typeof(ResourceDTO).GetProperty(nameof(_rDTO_2.Id)).SetValue(_rDTO_2, 2);


            //TEST RESOURCE 
            _r_1 = new Resource()
            {
                ContentUrl = "Resource1.com",
                DownloadableContentUrl = "Resource1Download.com",
                Content = ContentType.PDF,
                Chapter = new Chapter()

            };
            typeof(Chapter).GetProperty(nameof(_r_1.Chapter.Id)).SetValue(_r_1.Chapter, 1);
            typeof(Resource).GetProperty(nameof(_r_1.Id)).SetValue(_r_1, 1);

            _r_2 = new Resource()
            {
                ContentUrl = "Resource2.com",
                DownloadableContentUrl = "Resource2Download.com",
                Content = ContentType.Excel,
                Chapter = new Chapter()

            };
            typeof(Chapter).GetProperty(nameof(_r_2.Chapter.Id)).SetValue(_r_2.Chapter, 2);
            typeof(Resource).GetProperty(nameof(_r_2.Id)).SetValue(_r_2, 2);

        }
        
       
        
        //tear down-----------------------------------------------------------------------------
        public void Dispose()
        {
            _rDTO_1 = null;

            _rDTO_2 = null;

            _r_1 = null;
            _r_2 = null;
        }



        [Fact()]
        public void ResourceDTOTest_CheckObject()
        {
            Assert.NotEqual(_rDTO_1, _rDTO_2);
            Assert.NotEqual(_r_1, _r_2);

        }


        [Fact()]
        public void ResourceDTOTest_ConvertDomainToDTO()
        {


            //WHEN CHAPTER IS PROVIDED---------------------------------------------------------------------------------------------------------------------------------------------------------
            //chapter id is provided

            //expected dto & generated dto are equal (object converted to json as obejcts are technically different in memory)
            Assert.Equal(JsonConvert.SerializeObject(_rDTO_1), JsonConvert.SerializeObject(new ResourceDTO(_r_1)));
            Assert.Equal(JsonConvert.SerializeObject(_rDTO_2), JsonConvert.SerializeObject(new ResourceDTO(_r_2)));

            //chapter id is equal between expected DTO & generated DTO
            Assert.Equal(_rDTO_1.ChapterId, new ResourceDTO(_r_1).ChapterId);
            Assert.Equal(_rDTO_2.ChapterId, new ResourceDTO(_r_2).ChapterId);

            //check the chapter id of the generated DTO 

            Assert.Equal(1, new ResourceDTO(_r_1).ChapterId);
            Assert.Equal(2, new ResourceDTO(_r_2).ChapterId);


            //WHEN CHAPTER IS NOT PROVIDED-=--------------------------------------------------------------------------------------------------------------------------------------------------------
            _r_1.Chapter = null;
            _r_2.Chapter = null;

            //expected dto & generated dto are NOT equal as Chapter ID in generated DTO is 0 (as chapter is null in Resource [NOT RESOURCE DTO])
            Assert.NotEqual(JsonConvert.SerializeObject(_rDTO_1), JsonConvert.SerializeObject(new ResourceDTO(_r_1)));
            Assert.NotEqual(JsonConvert.SerializeObject(_rDTO_2), JsonConvert.SerializeObject(new ResourceDTO(_r_2)));

            //generate DTO's chapter Id is 0 

            Assert.Equal(0, new ResourceDTO(_r_1).ChapterId);
            Assert.Equal(0, new ResourceDTO(_r_2).ChapterId);
        }

       




    }




}