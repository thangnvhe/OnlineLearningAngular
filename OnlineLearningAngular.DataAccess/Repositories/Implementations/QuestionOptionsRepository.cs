using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class QuestionOptionsRepository : EfRepository<QuestionOptions, int>, IQuestionOptionsRepository
    {
        public QuestionOptionsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<QuestionOptions>> GetQuestionOptionsByQuestionIdAsync(int questionId)
        {
            return await FindAll().Where(qo => qo.QuestionId == questionId).ToListAsync();
        }
    }
}
