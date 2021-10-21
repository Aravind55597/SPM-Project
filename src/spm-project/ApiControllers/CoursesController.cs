using Microsoft.AspNetCore.Mvc;
using SPM_Project.DataTableModels;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SPM_Project.DTOs;
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


        [HttpGet, Route("GetEligibleCourses", Name = "GetEligibleCourses")]
        public async Task<IActionResult> GetEligibleCourses()
        {

            //get current user 
            var userId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();
            var user = await _unitOfWork.LMSUserRepository.GetByIdAsync(userId, "Enrollments");
            var response = await GetUserEligibleCourses(user);
            var dto = new List<CourseDTO>();
            foreach (var course in response) {
                
                dto.Add(new CourseDTO(course));


            }

            var responseJson = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            return Ok(responseJson);

        }


        //get list of course for eligible person

        [NonAction]
        public async Task<List<Course>> GetUserEligibleCourses(LMSUser user)
        {
            List<Course> eligiblecourses = new List<Course>();

            //for loop enroolment and check for completionstatus 

            var currentEnrollments = user.Enrollments.Where(e=>e.CompletionStatus == true);
            //for loop the completed ones to include course class
            var currentUserEnrollments = new List<ClassEnrollmentRecord>() ;
            foreach (var enrollment in currentEnrollments) {
                currentUserEnrollments.Add(await _unitOfWork.ClassEnrollmentRecordRepository.GetByIdAsync(enrollment.Id, "Course"));
            }

            var currentUserCourses = new List<Course>();

            foreach (var enrollment in currentUserEnrollments)
            {
                currentUserCourses.Add(enrollment.Course);
            }

            //get all coursess();
            var courses =await  _unitOfWork.CourseRepository.GetAllAsync(null,null, "PreRequisites CourseClass");
            //foreach course, check if user is eligible and push to 
            if (courses.Count >0)
            {

                foreach (var course in courses)
                {
                    //var isEligible = await GetCourseEligiblity(user, course);
                    if (course.PreRequisites == null || course.PreRequisites.Count == 0 ) {
                        eligiblecourses.Add(course);
                    }

                    else if (await GetCourseEligiblity(course, currentUserCourses))
                    {
                        eligiblecourses.Add(course);
                    }
                }



            }
            //return array
            return eligiblecourses;


        }

        [NonAction]
        public async Task<bool> GetCourseEligiblity(Course course,List<Course> courseprereq)
        {
            //sort arrays then check if equal
            course.PreRequisites = course.PreRequisites.OrderBy(c => c.Id).ToList();
            courseprereq = courseprereq.OrderBy(c => c.Id).ToList();
            //if the count is 0 means got no prereq
            if (course.PreRequisites.Count == 0 && courseprereq.Count == 0)
            {
                return true;
            }

            if (course.Equals(courseprereq))
            {
                return true;
            }
            return false;
        }


    }
}
