using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using SPM_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class CourseManagementService:ICourseManagementService
    {

        public IUnitOfWork _unitOfWork;

        public CourseManagementService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
           
        }



        //retrieve courses 
        public async Task<DTResponse<CourseTableData>> GetCoursesDataTable(DTParameterModel dTParameterModel)
        {
            //simulate async code 
            //await Task.Delay(100); 

            var result = await _unitOfWork.CourseRepository.GetCoursesDataTable(dTParameterModel);


            return result; 


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
