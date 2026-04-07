using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IExamService
    {
        Task<ServiceResult<List<ExamResponse>>> GetExamsByModuleIdAsync(int moduleId);
        Task<ServiceResult<ExamResponse?>> GetExamByIdAsync(int id);
        Task<ServiceResult<ExamResponse?>> CreateExamAsync(CreateExamRequest request);
        Task<ServiceResult<ExamResponse?>> UpdateExamAsync(int id, UpdateExamRequest request);
        Task<ServiceResult<bool>> DeleteExamAsync(int id);
    }
}
