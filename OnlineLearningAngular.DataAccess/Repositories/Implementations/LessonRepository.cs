using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class LessonRepository : EfRepository<Lesson, int>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(int moduleId)
        {
            return await FindAll().Where(l => l.ModuleId == moduleId).ToListAsync();
        }
    }
}
