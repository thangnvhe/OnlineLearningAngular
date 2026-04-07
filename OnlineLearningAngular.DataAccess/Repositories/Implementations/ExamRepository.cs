using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class ExamRepository : EfRepository<Exam, int>, IExamRepository
    {
        public ExamRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Exam>> GetExamsByModuleIdAsync(int moduleId)
        {
            return await FindAll().Where(e => e.ModuleId == moduleId).ToListAsync();
        }
    }
}
