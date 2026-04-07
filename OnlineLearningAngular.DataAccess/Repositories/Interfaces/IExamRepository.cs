using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IExamRepository : IRepository<Exam, int>
    {
        Task<IEnumerable<Exam>> GetExamsByModuleIdAsync(int moduleId);
    }
}
