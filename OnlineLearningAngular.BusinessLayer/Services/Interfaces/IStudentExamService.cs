using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IStudentExamService
    {
        Task<ServiceResult<PagedResult<StudentExamResponse>>> GetPagedStudentExamByExamIdAsync(int examId, PagingFilterBase filters);
        Task<ServiceResult<StudentExamResponse?>> GetStudentExamByIdAsync(int id);
        Task<ServiceResult<StudentExamResponse?>> CreateStudentExamAsync(CreateStudentExamRequest request);
        Task<ServiceResult<StudentExamResponse?>> UpdateStudentExamAsync(int id, UpdateStudentExamRequest request);
        Task<ServiceResult<bool>> DeleteStudentExamAsync(int id);
    }
}
