using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels;
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

        public async Task<bool> GetCourseEligiblity(LMSUser user, Course course)
        {

            //get courses that user has completed
            var completed_progresstrackers = (List<ProgressTracker>)_unitOfWork.LMSUserRepository.GetCompletedProgressTracker(user);
            var completed_courses = new List<Course>();
            foreach (var tracker in completed_progresstrackers)
            {
                completed_courses.Add(tracker.Course);
            }
            //get the course prereq for current course
            var course_prereq = _unitOfWork.CourseRepository.GetCoursePreReq(course);

            //check if the prereq are fufilled

            if (completed_courses.Equals(course_prereq))
            {
                return true;
            }
            return false;
        }


    }
}
