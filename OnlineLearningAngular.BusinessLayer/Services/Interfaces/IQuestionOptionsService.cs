using OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IQuestionOptionsService
    {
        Task<ServiceResult<List<QuestionOptionsResponse>>> GetQuestionOptionsByQuestionIdAsync(int questionId);
        Task<ServiceResult<QuestionOptionsResponse?>> GetQuestionOptionsByIdAsync(int id);
        Task<ServiceResult<QuestionOptionsResponse?>> CreateQuestionOptionsAsync(CreateQuestionOptionsRequest request);
        Task<ServiceResult<QuestionOptionsResponse?>> UpdateQuestionOptionsAsync(int id, UpdateQuestionOptionsRequest request);
        Task<ServiceResult<bool>> DeleteQuestionOptionsAsync(int id);
    }
}
