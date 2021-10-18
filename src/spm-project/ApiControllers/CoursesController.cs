using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        public IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }








        [HttpPost, Route("CoursesDataTable", Name = "GetCoursesDataTable")]
        public async Task<IActionResult> GetCoursesDataTable([FromBody] DTParameterModel dTParameterModel)
        {


            var response = await _unitOfWork.CourseRepository.GetCoursesDataTable(dTParameterModel);


            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            return Ok(responseJson);

        }


        //[HttpGet, Route("GetEligibleCourses", Name = "GetEligibleCourses")]
        //public async Task<IActionResult> GetEligibleCourses()
        //{

        //    //get current user 
        //    var userId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();
        //    var user = await _unitOfWork.LMSUserRepository.GetByIdAsync(userId);
        //    var response = await GetUserEligibleCourses(user);


        //    var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(response);
        //    return Ok(responseJson);

        //}



        ////get list of course for eligible person




        //[NonAction]
        //public async Task<List<Course>> GetUserEligibleCourses(LMSUser user)
        //{
        //    List<Course> eligiblecourses = new List<Course>();
        //    //get all coursess();
        //    var courses = _unitOfWork.CourseRepository.GetAllCourses();
        //    //foreach course, check if user is eligible and push to 
        //    if (courses.Count > 0)
        //    {

        //        foreach (var course in courses)
        //        {

        //            var isEligible = await GetCourseEligiblity(user, course);
        //            if (isEligible)
        //            {
        //                eligiblecourses.Add(course);
        //            }
        //        }



        //    }
        //    //return array
        //    return eligiblecourses;





        //}

        //[NonAction]
        //public async Task<bool> GetCourseEligiblity(LMSUser user, Course course)
        //{
        //    //get all the courses including prerequisites
        //    //get all completed courses fo a user
        //    //
        //    //get courses that user has completed
        //    var completed_progresstrackers = await _unitOfWork.CourseRepository.GetByIdAsync(course.Id, "PreRequisites");  //find en
        //    var completed_courses = new List<Course>();
        //    foreach (var tracker in completed_progresstrackers)
        //    {
        //        completed_courses.Add(tracker.Course);
        //    }

        //    //get the course prereq for current course
        //    var courseprereq = await _unitOfWork.CourseRepository.GetByIdAsync(course.Id, "PreRequisites");

        //    //check if the prereq are fufilled
        //    Debug.WriteLine(courseprereq.PreRequisites);
        //    if (completed_courses.Equals(courseprereq.PreRequisites))
        //    {
        //        return true;
        //    }
        //    return false;
        //}













    }
}
