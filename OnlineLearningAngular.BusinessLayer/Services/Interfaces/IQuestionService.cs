using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<ServiceResult<List<QuestionResponse>>> GetQuestionsByExamIdAsync(int examId);
        Task<ServiceResult<QuestionResponse?>> GetQuestionByIdAsync(int id);
        Task<ServiceResult<QuestionResponse?>> CreateQuestionAsync(CreateQuestionRequest request);
        Task<ServiceResult<QuestionResponse?>> UpdateQuestionAsync(int id, UpdateQuestionRequest request);
        Task<ServiceResult<bool>> DeleteQuestionAsync(int id);
    }
}
