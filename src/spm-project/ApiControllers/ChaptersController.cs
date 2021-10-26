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
    public class ChaptersController : ControllerBase
    {



        public IUnitOfWork _unitOfWork;

        public CourseClassesController _courseClassesCon; 

        public ChaptersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _courseClassesCon = new CourseClassesController(unitOfWork); 
        }



        [HttpGet, Route("{id:int?}", Name = "GetChapters")]
        public async Task<IActionResult> GetChapterDTOs(int? id , [FromQuery] int? courseClassId)
        {

            if (id!=null)
            {
                return Ok( new Response<ChapterDTO>(await GetChapterDTOAsync((int)id, "Resources,Quizzes,CourseClass"))); 
            }

            else
            {

    

            return Ok(new Response<List<ChapterDTO>>(await GetChapterDTOsAsync(courseClassId, "Resources,Quizzes,CourseClass")));
             

            }

        }

        //get chap---------------------------------------------------------------------------------------------------------------
        [NonAction]
        public async Task<Chapter> GetChapterAsync(int id, string properties = "")
        {
            var chap = await _unitOfWork.ChapterRepository.GetByIdAsync(id, properties);

            if (chap == null)
            {
                throw new NotFoundException($"Chapter of id {id} is not found");
            }
            return chap;
        }

        [NonAction]
        public async Task<ChapterDTO> GetChapterDTOAsync(int id, string properties = "")
        {
            var chap = await GetChapterAsync(id, properties);

            return new ChapterDTO(chap);
        }


        //get chaps------------------------------------------------------------------------------------------------------------
        [NonAction]
        public async Task<List<Chapter>> GetChaptersAsync(int? courseClassId, string properties = "")
        {
            var chaps = new List<Chapter>(); 
            if (courseClassId!=null)
            {
                var cc = await _courseClassesCon.GetCourseClass((int)courseClassId);
                chaps = await _unitOfWork.ChapterRepository.GetAllAsync(filter: f => f.CourseClass.Id == cc.Id, includeProperties: properties);
            }
            else
            {
                chaps = await _unitOfWork.ChapterRepository.GetAllAsync(includeProperties: properties);
            }

            if (chaps == null)
            {
                throw new NotFoundException($"Chapters are not found");
            }
            return chaps;
        }



        [NonAction]
        public async Task<List<ChapterDTO>> GetChapterDTOsAsync(int? courseClassId, string properties = "")
        {
            var chaps =await GetChaptersAsync(courseClassId, properties); 

            var result = new List<ChapterDTO>();

            foreach (var item in chaps)
            {
                result.Add(new ChapterDTO(item)); 
            }

            return result; 

        }







    }
}
