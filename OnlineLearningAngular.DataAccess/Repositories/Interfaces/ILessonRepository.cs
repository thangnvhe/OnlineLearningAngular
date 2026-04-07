using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface ILessonRepository : IRepository<Lesson, int>
    {
        Task<IEnumerable<Lesson>> GetLessonsByModuleIdAsync(int moduleId);
    }
}
