using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IModuleRepository : IRepository<Module, int>
    {
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
    }
}
