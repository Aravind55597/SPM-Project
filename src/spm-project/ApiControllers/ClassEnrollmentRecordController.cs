using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPM_Project.EntityModels;
using SPM_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassEnrollmentRecordController : ControllerBase
    {

        public IServiceManager _serviceManager;

        public ClassEnrollmentRecordController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }



        [HttpPost, Route("Add",Name = "GetClassEnrollmentRecord")]
        public Task<bool> AddEnrollmentRecord()
        {
            //firstly retreive class from classservice (check if class exists)

            //Secondly use classenrollmentrecordservice to check eligibility 

            //Create classenrollment record for the user

            //pass in user & enrollment record 

            return true;
        }



        public async Task<bool> EnrollUserIntoClass(LMSUser user, CourseClass courseclass)
        {

            if (_serviceManager.CourseManagementService.GetCourseEligiblity(user, courseclass.Course))
            {
                //user is eligible


                //enroll user
                //_serviceManager.ClassManagementService
                //call classenrollment add

                return true;
            }
            else
            {
                //do smth since user is not eligible

                return false;
            }



        }

        public async Task<bool> AddEnrollmentRecord(LMSUser user, CourseClass courseclass)
        {

            //- Send user id and class id for send an enrollment request



            //- I can't enroll in the same class again if I already sent a request 



            //- I can't enrolled in other classes IF they teach the same course as the class I am already enrolled into




            //- i can't enrol into classes outside of the course registration period "

           

            return true;
        }







    }
}
