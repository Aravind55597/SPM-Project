using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.CustomExceptions;
using SPM_Project.DTOs;
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
    public class UserAnswersController : ControllerBase
    {


        public IUnitOfWork _unitOfWork;

        public CourseClassesController _courseClassesCon;

        public UsersController _usersController;



        public UserAnswersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_courseClassesCon = new CourseClassesController(unitOfWork);
            _courseClassesCon = new CourseClassesController(unitOfWork);
            _usersController = new UsersController(unitOfWork); 

        }




        //post




        //get







        //put








        //get user Answer---------------------------------------------------------------------------------------------------------------


        [NonAction]
        public async Task<UserAnswer> GetUserAnswerAsync(int id, string properties = "")
        {
            var userAns = await _unitOfWork.UserAnswerRepository.GetByIdAsync(id, properties);

            if (userAns == null)
            {
                throw new NotFoundException($"User Answer of id {id} is not found");
            }
            return userAns;
        }

        [NonAction]
        public async Task<UserAnswerDTO> GetUserAnswerDTOAsync(int id, string properties = "")
        {
            var chap = await GetUserAnswerAsync(id, properties);

            return new UserAnswerDTO(chap);
        }


        //get chaps------------------------------------------------------------------------------------------------------------


        //[NonAction]
        //public async Task<List<UserAnswer>> GetUserAnswersAsync(int? courseClassId, string properties = "")
        //{

        //    if (courseClassId != null)
        //    {
        //        var cc = await _courseClassesCon.GetCourseClassAsync((int)courseClassId, "");

        //        return await _unitOfWork.UserAnswerRepository.GetAllAsync(filter: f => f.QuizQuestion.Id == cc.Id, includeProperties: properties);
        //    }
        //    else
        //    {
        //        return await _unitOfWork.ChapterRepository.GetAllAsync(includeProperties: properties);
        //    }

        //}



        //[NonAction]
        //public async Task<List<ChapterDTO>> GetChapterDTOsAsync(int? courseClassId, string properties = "")
        //{
        //    var chaps = await GetChaptersAsync(courseClassId, properties);

        //    var result = new List<ChapterDTO>();

        //    foreach (var item in chaps)
        //    {
        //        result.Add(new ChapterDTO(item));
        //    }

        //    return result;

        //}












    }
}
