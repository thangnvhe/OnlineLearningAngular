using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IStudentExamDetailService
    {
        Task<ServiceResult<List<StudentExamDetailResponse>>> GetStudentExamDetailsByStudentExamIdAsync(int studentExamId);
        Task<ServiceResult<StudentExamDetailResponse?>> GetStudentExamDetailByIdAsync(int id);
        Task<ServiceResult<StudentExamDetailResponse?>> CreateStudentExamDetailAsync(CreateStudentExamDetailRequest request);
        Task<ServiceResult<StudentExamDetailResponse?>> UpdateStudentExamDetailAsync(int id, UpdateStudentExamDetailRequest request);
        Task<ServiceResult<bool>> DeleteStudentExamDetailAsync(int id);
    }
}
