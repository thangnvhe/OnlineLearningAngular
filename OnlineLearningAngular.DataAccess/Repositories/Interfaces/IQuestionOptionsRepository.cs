using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IQuestionOptionsRepository : IRepository<QuestionOptions, int>
    {
        Task<IEnumerable<QuestionOptions>> GetQuestionOptionsByQuestionIdAsync(int questionId);
    }
}
