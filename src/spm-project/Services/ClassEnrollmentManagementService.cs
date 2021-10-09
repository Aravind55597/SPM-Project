using SPM_Project.CustomExceptions;
using SPM_Project.DataTableModels;
using SPM_Project.DataTableModels.DataTableData;
using SPM_Project.DataTableModels.DataTableResponse;
using SPM_Project.EntityModels;
using SPM_Project.Repositories.Interfaces;
using SPM_Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SPM_Project.Services
{
    public class ClassEnrollmentManagementService : IClassEnrollmentRecord
    {
        public IUnitOfWork _unitOfWork;

        public ClassEnrollmentManagementService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public Task AddAsync(EntityModels.ClassEnrollmentRecord entity)
        {
            throw new NotImplementedException();
        }


        //add enrollment record -> all business 





        public async Task<bool> AddClassEnrollmentRecord(LMSUser user, ClassEnrollmentRecord record)
        {
            user.Enrollments.Add(new ClassEnrollmentRecord
            {



            });

            return true;

        }

        public Task AddRangeAsync(IEnumerable<EntityModels.ClassEnrollmentRecord> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EntityModels.ClassEnrollmentRecord>> GetAllAsync(Expression<Func<EntityModels.ClassEnrollmentRecord, bool>> filter, Func<IQueryable<EntityModels.ClassEnrollmentRecord>, IOrderedQueryable<EntityModels.ClassEnrollmentRecord>> orderBy, string includeProperties, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<EntityModels.ClassEnrollmentRecord> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task RemoveByEntityAsync(EntityModels.ClassEnrollmentRecord entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeByEntityAsync(IEnumerable<EntityModels.ClassEnrollmentRecord> entities)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeByIdAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}