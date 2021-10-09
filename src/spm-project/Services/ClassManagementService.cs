using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.Repositories.Interfaces;
using SPM_Project.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class ClassManagementService : IClassManagementService
    {
        public IUnitOfWork _unitOfWork;

        public ClassManagementService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }



        //--------------------------------------------------------------------------------------------------------------DATATABLE--------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<DTResponse<CourseClassTableData>> GetCourseClassesDataTable(DTParameterModel dTParameterModel, int? courseId, int? lmsUserId, bool isTrainer, bool isLearner)
        {
            var response = new DTResponse<CourseClassTableData>();

            int userId; 

            //RetrieveCurrentUserId() return the id of the current user if lmsuser is not supplied 
            if (lmsUserId==null)
            {
                userId = await _unitOfWork.LMSUserRepository.RetrieveCurrentUserIdAsync();

            }

            //if lmsUserId is not null , check if user exists 
            else
            {
                var user = await _unitOfWork.LMSUserRepository.GetByIdAsync((int)lmsUserId); 

                if (user ==null)
                {
                    var errorDict = new Dictionary<string, string>()
                    {
                        {"lmsUserId", $"LMS User of Id {lmsUserId} does not exist" }
                    };

                    var notFoundExp = new NotFoundException("lmsUser does not exist", errorDict);

                    throw notFoundExp;
                }
                //use lmsuserId supplied 
                userId = (int)lmsUserId;
            }


            //if courseID is not null
            if (courseId != null)
            {
                //retreive course 
                var course = await _unitOfWork.CourseRepository.GetByIdAsync((int)courseId);

                //course does not exist
                if (course == null)
                {
                    var errorDict = new Dictionary<string, string>()
                    {
                        {"CourseId", $"Course of Id {courseId} does not exist" }
                    };

                    var notFoundExp = new NotFoundException("Course does not exist", errorDict);

                    throw notFoundExp;
                }
            }





            ////retrieve roles of the user
            //List<string> roles = await _unitOfWork.LMSUserRepository.RetreiveUserRolesAsync(userId);

            ////currently a user has one role sowe just take one
            //var role = roles[0];

            //retreive data 
            response = await _unitOfWork.CourseClassRepository.GetCourseClassesDataTable(dTParameterModel, courseId, userId, isTrainer, isLearner);

            return response;
        }
    }
}