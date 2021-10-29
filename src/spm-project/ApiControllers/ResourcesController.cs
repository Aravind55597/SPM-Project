using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs;
using SPM_Project.DTOs.RRModels;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {

        public IUnitOfWork _unitOfWork;
        public ChaptersController _chaptersCon;

        public ResourcesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _chaptersCon = new ChaptersController(unitOfWork); 
        }



        [HttpGet, Route("{id:int?}", Name = "GetResources")]
        public async Task<IActionResult> GetResourceDTOs(int? id, [FromQuery] int? chapterId)
        {

            if (id != null)
            {
                return Ok(new Response<ResourceDTO>(await GetResourceDTOAsync((int)id, "Chapter")));
            }

            else
            {


                return Ok(new Response<List<ResourceDTO>>(await GetResourceDTOsAsync(chapterId, "Resources,Quizzes,CourseClass")));


            }

        }


        //get chap---------------------------------------------------------------------------------------------------------------


        [NonAction]
        public async Task<Resource> GetResourceAsync(int id, string properties = "")
        {
            var resource = await _unitOfWork.ResourceRepository.GetByIdAsync(id, properties);

            if (resource == null)
            {
                throw new NotFoundException($"Resource of id {id} is not found");
            }
            return resource;
        }

        [NonAction]
        public async Task<ResourceDTO> GetResourceDTOAsync(int id, string properties = "")
        {
            var chap = await GetResourceAsync(id, properties);

            return new ResourceDTO(chap);
        }


        //get chaps------------------------------------------------------------------------------------------------------------
        [NonAction]
        public async Task<List<Resource>> GetResourcesAsync(int? chapterId, string properties = "")
        {
            var resources = new List<Resource>();
            if (chapterId != null)
            {
                var cc = await _chaptersCon.GetChapterAsync((int)chapterId);
                resources = await _unitOfWork.ResourceRepository.GetAllAsync(filter: f => f.Chapter.Id == cc.Id, includeProperties: properties);
            }
            else
            {
                resources = await _unitOfWork.ResourceRepository.GetAllAsync(includeProperties: properties);
            }
            return resources;
        }



        [NonAction]
        public async Task<List<ResourceDTO>> GetResourceDTOsAsync(int? chapterId, string properties = "")
        {
            var resources = await GetResourcesAsync(chapterId, properties);

            var result = new List<ResourceDTO>();

            foreach (var item in resources)
            {
                result.Add(new ResourceDTO(item));
            }

            return result;

        }







    }
}
