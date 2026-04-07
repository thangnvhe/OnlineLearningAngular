using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class ModuleRepository : EfRepository<Module, int>, IModuleRepository
    {
        public ModuleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await FindAll().Where(m => m.CourseId == courseId).ToListAsync();
        }
    }
}
