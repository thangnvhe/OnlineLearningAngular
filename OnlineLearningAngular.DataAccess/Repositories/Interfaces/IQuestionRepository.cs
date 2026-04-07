using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IQuestionRepository : IRepository<Question, int>
    {
        Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId);
    }
}
