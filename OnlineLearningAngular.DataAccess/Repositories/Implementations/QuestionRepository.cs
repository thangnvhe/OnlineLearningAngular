using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class QuestionRepository : EfRepository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Question>> GetQuestionsByExamIdAsync(int examId)
        {
            return await FindAll().Where(q => q.ExamId == examId).ToListAsync();
        }
    }
}
